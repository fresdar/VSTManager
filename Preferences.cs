using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace VSTManager
{
    public class XmlPreferences
    {
        public XmlPreferences()
        {
            VST3FolderList = new List<string>();
            VST2FolderList = new List<string>();
            ExcludedFolders = new List<string>();
        }
        public List<string> ExcludedFolders;
        public string SteinbergConfigPath;
        public string SearchEngineBaseUrl;

        public string VST2Extension;
        public List<string> VST2FolderList;

        public string VST3Extension;
        public List<string> VST3FolderList;
        public string DownloadFolder;
    }

    [DefaultPropertyAttribute("Name")]
    public class Preferences
    {
        public Preferences()
        {
            _pref = new XmlPreferences();
            _xmlConfigFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "//VSTManager//preferences.xml";
            Load();
        }

        private string _xmlConfigFile;
        private XmlPreferences _pref;

        [CategoryAttribute("Steinberg"), DescriptionAttribute("Path of steinberg xml files")]
        public string SteinbergConfigPath
        {
            get
            {
                return _pref.SteinbergConfigPath;
            }
            set
            {
                _pref.SteinbergConfigPath = value;
            }
        }
        [CategoryAttribute("Web search"), DescriptionAttribute("Path of search engine")]
        public string SearchEngineBaseUrl
        {
            get
            {
                return _pref.SearchEngineBaseUrl;
            }
            set
            {
                _pref.SearchEngineBaseUrl = value;
            }
        }
        [CategoryAttribute("Web search"), DescriptionAttribute("Download folder")]
        public string DownloadFolder
        {
            get
            {
                return _pref.DownloadFolder;
            }
            set
            {
                _pref.DownloadFolder = value;
            }
        }

        [CategoryAttribute("Folder scan"), DescriptionAttribute("Excluded folders")]
        public List<string> ExcludedFolders
        {
            get
            {
                return _pref.ExcludedFolders;
            }
            set
            {
                _pref.ExcludedFolders = value;
            }
        }
        [CategoryAttribute("File scan"), DescriptionAttribute("VST2 filename extension")]
        public string VST2Extension
        {
            get
            {
                return _pref.VST2Extension;
            }
            set
            {
                _pref.VST2Extension = value;
            }
        }
        [CategoryAttribute("File scan"), DescriptionAttribute("VST3 filename extension")]
        public string VST3Extension
        {
            get
            {
                return _pref.VST3Extension;
            }
            set
            {
                _pref.VST3Extension = value;
            }
        }

        [CategoryAttribute("File scan"), DescriptionAttribute("VST2 folder list")]
        public List<string> VST2FolderList
        {
            get
            {
                return _pref.VST2FolderList;
            }
            set
            {
                _pref.VST2FolderList = value;
            }
        }
        [CategoryAttribute("File scan"), DescriptionAttribute("VST3 folder list")]
        public List<string> VST3FolderList
        {
            get
            {
                return _pref.VST3FolderList;
            }
            set
            {
                _pref.VST3FolderList = value;
            }
        }

        private enum VSTVersion { vst2, vst3};
        private void EnumerateFiles(ref List<string> folders, VSTVersion version, DirectoryInfo dir)
        {
            try
            {
                //dirs
                DirectoryInfo[] dirs = dir.GetDirectories("*", SearchOption.TopDirectoryOnly);
                foreach (DirectoryInfo di in dirs)
                {
                    if (IsValid(di.Attributes, di.FullName))
                    {
                        if (version == VSTVersion.vst2 
                            && (di.FullName.EndsWith("\\vst2", StringComparison.InvariantCultureIgnoreCase)
                            || di.FullName.EndsWith("\\vstplugins", StringComparison.InvariantCultureIgnoreCase)))
                        {
                            folders.Add(di.FullName);
                        }
                        else if (version == VSTVersion.vst3 && di.FullName.EndsWith("\\vst3", StringComparison.InvariantCultureIgnoreCase))
                        {
                            folders.Add(di.FullName);
                        }
                        else
                                {
                            EnumerateFiles(ref folders, version, di);
                        }
                    }
                }
            }
            catch(UnauthorizedAccessException)
            {
                _pref.ExcludedFolders.Add(dir.FullName.ToLower());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool IsValid(FileAttributes attributes, string fullpath)
        {
            bool retval = true;
            fullpath = fullpath.ToLower();

            if (fullpath.Contains("microsoft"))
            {
                return false;
            }

            if (_pref.ExcludedFolders.Contains(fullpath))
            {
                return false;
            }

            try
            {
                // Attempt to get a list of security permissions from the folder. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(fullpath);
            }
            catch (UnauthorizedAccessException)
            {
                _pref.ExcludedFolders.Add(fullpath);
                return false;
            }

            if (((attributes & FileAttributes.System) == FileAttributes.System) ||
                ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden))
            {
                return false;
            }

            return retval;
        }
        private void DirSearch(ref List<string> folders, List<DirectoryInfo> roots, VSTVersion version)
        {
            foreach (DirectoryInfo di in roots)
            {
                EnumerateFiles(ref folders, version, di);
            }
        }
        public void resetVST2FolderList()
        {
            _pref.VST2FolderList.Clear();
            _pref.VST2FolderList.Add(@"C:\Program Files\VSTPlugins");
            _pref.VST2FolderList.Add(@"C:\Program Files\Steinberg\VSTPlugins");
            _pref.VST2FolderList.Add(@"C:\Program Files\Common Files\VST2");
            _pref.VST2FolderList.Add(@"C:\Program Files\Common Files\Steinberg\VST2");
        }
        public void searchVST2Folders()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                VST2FolderList.Clear();

                DriveInfo[] allDrives = DriveInfo.GetDrives();
                List<DirectoryInfo> roots = new List<DirectoryInfo>();
                foreach (DriveInfo drive in allDrives)
                {
                    if ((drive.DriveType == DriveType.Fixed) || (drive.DriveType == DriveType.Removable) && drive.IsReady)
                    {
                        roots.Add(drive.RootDirectory);
                    }
                }
                DirSearch(ref _pref.VST2FolderList, roots, VSTVersion.vst2);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        public void resetVST3FolderList()
        {
            _pref.VST3FolderList.Clear();
            _pref.VST3FolderList.Add(@"C:\Program Files\Common Files\VST3");
        }

        public void searchVST3Folders()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                VST3FolderList.Clear();


                DriveInfo[] allDrives = DriveInfo.GetDrives();
                List<DirectoryInfo> roots = new List<DirectoryInfo>();
                foreach (DriveInfo drive in allDrives)
                {
                    if ((drive.DriveType == DriveType.Fixed) || (drive.DriveType == DriveType.Removable) && drive.IsReady)
                    {
                        roots.Add(drive.RootDirectory);
                    }
                }
                DirSearch(ref _pref.VST3FolderList, roots, VSTVersion.vst3);
            } 
            catch (Exception e) 
            {
                MessageBox.Show(e.Message);
            }
            Cursor.Current = Cursors.Default;
        }
        public void Save()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer writer =
                    new System.Xml.Serialization.XmlSerializer(typeof(XmlPreferences));
                Directory.CreateDirectory(Path.GetDirectoryName(_xmlConfigFile));
                
                System.IO.FileStream file = System.IO.File.Create(_xmlConfigFile);

                writer.Serialize(file, _pref);
                file.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void Load()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(XmlPreferences));
                System.IO.StreamReader file = new System.IO.StreamReader(_xmlConfigFile);
                _pref = (XmlPreferences)reader.Deserialize(file);
                file.Close();
            }
            catch(FileNotFoundException)
            {
                // Set default values
                _pref.VST2Extension = "*.dll";
                _pref.VST3Extension = "*.vst3";

                resetVST2FolderList();
                resetVST3FolderList();

                _pref.SteinbergConfigPath = @"%AppData%\Roaming\Steinberg";
                _pref.SearchEngineBaseUrl = "https://duckduckgo.com";
                _pref.DownloadFolder = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();
                _pref.ExcludedFolders.Add(Environment.GetEnvironmentVariable("windir").ToLower());
                _pref.ExcludedFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86).ToLower());
                _pref.ExcludedFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache).ToLower());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
