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
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);

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

