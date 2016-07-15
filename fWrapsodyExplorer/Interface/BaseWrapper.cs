using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace fWrapsodyExplorer
{
	public class BaseWrapper
	{
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate bool _Dllmain(IntPtr hmodule, UInt32 dword, ref UInt32 lpvoid);

		#region Mem Variable
		protected bool _initialized { get; set; }
		protected IntPtr _module { get; set; }
		protected string _modulePath { get; set; }
		protected uint _dllProcKey { get; set; }
		private static string _installPath { get; set; }
		private static string _keyName;
		#endregion

		#region Win32api
		[DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
		protected static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

		[DllImport("kernel32.dll", EntryPoint = "GetModuleHandle")]
		protected static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPStr)] string lpModuleName);
		#endregion
		
		public BaseWrapper()
		{
			_initialized = false;
			_module = IntPtr.Zero;
			_dllProcKey = 114;
			_keyName = "HKEY_LOCAL_MACHINE\\Software\\Wrapsody";
		}

		public static bool GetDRMInstallPath()
		{
			bool bRet = false;
			object o = (string)Registry.GetValue(_keyName, "InstallPath", null);

			if (!o.Equals(null))
			{
				bRet = true;
				_installPath = o as string;
			}

			return bRet;
		}

		protected bool LoadDll(string dllName)
		{
			_module = GetModuleHandle(dllName);
			if (IntPtr.Equals(_module, IntPtr.Zero))
			{
				if (!GetDRMInstallPath())
				{
					return false;
				}

				_modulePath = _installPath + dllName;
				_module = LoadLibrary(_modulePath);
				if (IntPtr.Equals(_module, IntPtr.Zero))
				{
					return false;
				}
			}

			return true;
		}

		
	}
}
