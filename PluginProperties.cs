using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace VSTManager
{
    [DefaultPropertyAttribute("ProductName")]
    public class PluginProperties
    {
        public PluginProperties(string pluginPath)
        {
            FullPath = pluginPath;

            FileInfo fi = new FileInfo(pluginPath);
            Size = fi.Length;
            if (!fi.Attributes.HasFlag(FileAttributes.Directory))
            {
                FileVersionInfo version = FileVersionInfo.GetVersionInfo(pluginPath);
                ProductVersion = version.ProductVersion;
                FileVersion = version.FileVersion;
                CompanyName = version.CompanyName;
                ProductName = version.ProductName;
                FileDescription = version.FileDescription;
                Query = GetPluginQuery(pluginPath);
            }
            else
            {
                string[] words = pluginPath.Split('\\');
                if (words.Length > 1)
                {
                    Query = words[words.Length - 1].Replace(' ', '+') + "+vst";
                }
            }
            GetDllInfo(pluginPath);

        }

        private static void AddKeywords(string data, ref List<string> keywords)
        {
            string[] words = data.Split(' ');
            foreach (string w in words)
            {
                if (!string.IsNullOrEmpty(w))
                {
                    if (!keywords.Exists(e => e == w))
                        keywords.Add(w);
                }
            }
        }

        private string GetPluginQuery(string path)
        {
            string query = string.Empty;
            List<string> keywords = new List<string>();

            if (!string.IsNullOrEmpty(CompanyName))
            {
                AddKeywords(CompanyName, ref keywords);
            }
            else
            {
                string[] words = path.Split('\\');
                if (words.Length > 1 && !words[words.Length - 2].Equals("VST3", StringComparison.InvariantCultureIgnoreCase))
                {
                    AddKeywords(words[words.Length - 2], ref keywords);
                }
            }
            if (!string.IsNullOrEmpty(ProductName))
            {
                AddKeywords(ProductName, ref keywords);
            }
            else
            {
                string[] words = path.Split('\\');
                if (words.Length > 1)
                {
                    AddKeywords(words[words.Length - 1].Replace(".dll", "").Replace(".vst3", ""), ref keywords);
                }
            }
            AddKeywords("vst", ref keywords);
            query = string.Join("+", keywords);

            return query;
        }

        public void GetDllInfo(string pluginPath)
        {
            bool? is64Bits = IsDll64Bits(pluginPath);
            if (is64Bits != null)
            {
                Plateform = (bool)is64Bits ? "64 bits" : "32 bits";
            }

            IntPtr modulePtr = NativeMethods.LoadLibraryEx(pluginPath, IntPtr.Zero, NativeMethods.LoadLibraryFlags.DONT_RESOLVE_DLL_REFERENCES);
            IntPtr vst3PluginFactory = NativeMethods.GetProcAddress(modulePtr, "GetPluginFactory");
            IntPtr mainEntryPoint = NativeMethods.GetProcAddress(modulePtr, "VSTPluginMain");
            NativeMethods.FreeLibrary(modulePtr);
            bool found = false;
            if (mainEntryPoint != IntPtr.Zero)
            {
                VSTVersion += "2";
                found = true;
            }
            if (vst3PluginFactory != IntPtr.Zero)
            {
                VSTVersion += found ? " & 3" : "3";
            }
            
        }
        private enum MachineType : ushort
        {
            IMAGE_FILE_MACHINE_UNKNOWN = 0x0,
            IMAGE_FILE_MACHINE_AM33 = 0x1d3,
            IMAGE_FILE_MACHINE_AMD64 = 0x8664,
            IMAGE_FILE_MACHINE_ARM = 0x1c0,
            IMAGE_FILE_MACHINE_EBC = 0xebc,
            IMAGE_FILE_MACHINE_I386 = 0x14c,
            IMAGE_FILE_MACHINE_IA64 = 0x200,
            IMAGE_FILE_MACHINE_M32R = 0x9041,
            IMAGE_FILE_MACHINE_MIPS16 = 0x266,
            IMAGE_FILE_MACHINE_MIPSFPU = 0x366,
            IMAGE_FILE_MACHINE_MIPSFPU16 = 0x466,
            IMAGE_FILE_MACHINE_POWERPC = 0x1f0,
            IMAGE_FILE_MACHINE_POWERPCFP = 0x1f1,
            IMAGE_FILE_MACHINE_R4000 = 0x166,
            IMAGE_FILE_MACHINE_SH3 = 0x1a2,
            IMAGE_FILE_MACHINE_SH3DSP = 0x1a3,
            IMAGE_FILE_MACHINE_SH4 = 0x1a6,
            IMAGE_FILE_MACHINE_SH5 = 0x1a8,
            IMAGE_FILE_MACHINE_THUMB = 0x1c2,
            IMAGE_FILE_MACHINE_WCEMIPSV2 = 0x169,
            IMAGE_FILE_MACHINE_ARM64 = 0xaa64
        }
        private static bool? IsDll64Bits(string dllPath)
        {
            // See http://www.microsoft.com/whdc/system/platform/firmware/PECOFF.mspx
            // Offset to PE header is always at 0x3C.
            // The PE header starts with "PE\0\0" =  0x50 0x45 0x00 0x00,
            // followed by a 2-byte machine type field (see the document above for the enum).
            //
            using (var fs = new FileStream(dllPath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                fs.Seek(0x3c, SeekOrigin.Begin);
                Int32 peOffset = br.ReadInt32();

                fs.Seek(peOffset, SeekOrigin.Begin);
                UInt32 peHead = br.ReadUInt32();

                if (peHead != 0x00004550) // "PE\0\0", little-endian
                    throw new Exception("Can't find PE header");

                switch ((MachineType)br.ReadUInt16())
                {
                    case MachineType.IMAGE_FILE_MACHINE_AMD64:
                    case MachineType.IMAGE_FILE_MACHINE_IA64:
                        return true;
                    case MachineType.IMAGE_FILE_MACHINE_I386:
                        return false;
                    default:
                        return null;
                }
            }
        }
        
        [Browsable(false)]
        public string Query { get; set; }
        public string Plateform { get; set; }
        [DisplayName("VST Version")]
        public string VSTVersion { get; set; }
        [DisplayName("Full Path")]
        public string FullPath { get; set; }
        public long Size { get; set; }
        [DisplayName("Product Version")]
        public string ProductVersion { get; set; }
        [DisplayName("File Version")]
        public string FileVersion { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("File Description")]
        public string FileDescription { get; set; }
    }
}
