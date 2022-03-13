using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VSTManager
{
	static class NativeMethods
	{
		[System.Flags]
		public enum LoadLibraryFlags : uint
		{
			None = 0,
			DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
			LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
			LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
			LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
			LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
			LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
			LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
			LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
			LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
			LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
			LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
		}

		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

		[DllImport("kernel32.dll")]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		[DllImport("kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hModule);
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public class PFactoryInfo
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string vendor;               ///< e.g. "Steinberg Media Technologies"
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string url;                  ///< e.g. "http://www.steinberg.de"
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string email;                ///< e.g. "info@steinberg.de"
		public Int32 flags;                 ///< (see above)
	}
	public interface IPluginFactory
	{
		int getFactoryInfo(ref PFactoryInfo info);
	}

	public class VST3Plugin
	{
		public VST3Plugin()
		{

		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int InitVST3Dll();
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr GetVST3PluginFactory();

		public bool Load(string dllToLoad, ref string errorDescription)
		{
			bool result = false;
			IntPtr pDll = NativeMethods.LoadLibrary(dllToLoad);
			if (pDll == IntPtr.Zero)
			{
				errorDescription = "LoadLibrary failed.";
				return false;
			}
			else
			{
				IntPtr pGetFactoryProc = NativeMethods.GetProcAddress(pDll, "GetPluginFactory");
				IntPtr pInitDllProc = NativeMethods.GetProcAddress(pDll, "InitDll");

				if (pGetFactoryProc == IntPtr.Zero)
				{
					errorDescription = "GetPluginFactory not found";
				}
				else if (pInitDllProc == IntPtr.Zero)
				{
					errorDescription = "initDll not found";
				}
				else
				{
					InitVST3Dll initDll = (InitVST3Dll)Marshal.GetDelegateForFunctionPointer(pInitDllProc, typeof(InitVST3Dll));

					if (initDll() == 0)
					{
					}
					else
					{
						GetVST3PluginFactory getPluginFactory = (GetVST3PluginFactory)Marshal.GetDelegateForFunctionPointer(pGetFactoryProc, typeof(GetVST3PluginFactory));
						IntPtr factory = getPluginFactory();
						var comObject = Marshal.GetObjectForIUnknown(factory);
						PFactoryInfo info = new PFactoryInfo();
						IntPtr infoPtr = IntPtr.Zero;
						//comObject.invokeMe getFactoryInfo(infoPtr);
					}

					//auto f = Steinberg::FUnknownPtr<Steinberg::IPluginFactory>(owned(factoryProc()));
					//if (!f)
					//{
					//	errorDescription = "Calling 'GetPluginFactory' returned nullptr";
					//	return false;
					//}
					//factory = PluginFactory(f);
					//return true;

				}

				NativeMethods.FreeLibrary(pDll);
			}
			return result;
		}
	}
}

