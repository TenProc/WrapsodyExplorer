using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;

namespace fWrapsodyExplorer
{
	public class WrapDocumentInfo : BaseWrapper
	{
        public WrapDocumentInfo()
        {
            //if (true != this.Initialize())
            //{
            //    MessageBox.Show("f_documentinfo initialization failed");
            //}
        }
        enum DocumentInfoApi
		{
			Init = 0x2000,
			UpdataSyncInfos = 0x2001,
			GetSyncInfo = 0x2002,
			ClearAllSyncInfos = 0x2003,
			CheckIn = 0x2004,
			UndoCheckOut = 0x2005,
			UnWrap = 0x2006,
			GetSyncList = 0x2007
		}	

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void _Init([MarshalAs(UnmanagedType.LPWStr)] string FilePath);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int _UpdateAllSyncInfos(int nCnt);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate void _ClearAllSyncInfos();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int _GetSyncInfo([MarshalAs(UnmanagedType.LPWStr)] string FilePath,
										  ref int LocalVer,
										  ref int LatestVer,
										  ref SYNC_USER_INFO CheckOutUserInfo
										  );

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int _CheckIn([MarshalAs(UnmanagedType.LPWStr)] string FilePath);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int _UndoCheckOut([MarshalAs(UnmanagedType.LPWStr)] string FilePath);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int _UnWrap([MarshalAs(UnmanagedType.LPWStr)] string FilePath);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int _GetSyncList(ref List<String> synclist);

		#region Export Pol Function
		public Dictionary<uint, System.Delegate> APIs;
		public _Dllmain Dllmain;
		public _Init Init;
		public _UpdateAllSyncInfos UpdateAllSyncInfos;
		public _GetSyncInfo GetSyncInfo;
		public _ClearAllSyncInfos ClearAllSyncInfos;
		public _CheckIn CheckIn;
		public _UndoCheckOut UndoCheckOut;
		public _UnWrap UnWrap;
		public _GetSyncList GetSyncList;
		#endregion

		/// <summary>
		/// Wrapsody 관련 모듈을 로딩하고 함수 주소를 가져온다.
		/// </summary>
		public bool Initialize()
		{
			if (!_initialized)
			{
				if (!LoadDll(@"f_documentinfo.dll"))
				{
					return false;
				}

				if (!GetAPIs())
				{
					return false;
				}

			}

			return true;
		}

		private bool GetAPIs()
		{
			var reader = new PeHeaderReader(_modulePath);

			if (!reader.Is32BitHeader)
			{
				return false;
			}

			PeHeaderReader.IMAGE_OPTIONAL_HEADER32 header32 = reader.OptionalHeader32;
			if (header32.AddressOfEntryPoint != 0)
			{
				UInt32 entrypoint = (UInt32)_module + header32.AddressOfEntryPoint;
				Dllmain = (_Dllmain)Marshal.GetDelegateForFunctionPointer(new IntPtr(entrypoint), typeof(_Dllmain));

			}

			uint fhmApiID = _dllProcKey;
			uint fhmApiAddress = new uint();
			if (Dllmain == null)
			{
				return false;
			}


			fhmApiAddress = Convert.ToUInt32(DocumentInfoApi.Init);
			Dllmain(_module, fhmApiID, ref fhmApiAddress);
			Init = (_Init)Marshal.GetDelegateForFunctionPointer(new IntPtr(fhmApiAddress), typeof(_Init));

			fhmApiAddress = Convert.ToUInt32(DocumentInfoApi.UpdataSyncInfos);
			Dllmain(_module, fhmApiID, ref fhmApiAddress);
			UpdateAllSyncInfos = (_UpdateAllSyncInfos)Marshal.GetDelegateForFunctionPointer(new IntPtr(fhmApiAddress), typeof(_UpdateAllSyncInfos));

			fhmApiAddress = Convert.ToUInt32(DocumentInfoApi.GetSyncInfo);
			Dllmain(_module, fhmApiID, ref fhmApiAddress);
			GetSyncInfo = (_GetSyncInfo)Marshal.GetDelegateForFunctionPointer(new IntPtr(fhmApiAddress), typeof(_GetSyncInfo));

			fhmApiAddress = Convert.ToUInt32(DocumentInfoApi.ClearAllSyncInfos);
			Dllmain(_module, fhmApiID, ref fhmApiAddress);
			ClearAllSyncInfos = (_ClearAllSyncInfos)Marshal.GetDelegateForFunctionPointer(new IntPtr(fhmApiAddress), typeof(_ClearAllSyncInfos));

			fhmApiAddress = Convert.ToUInt32(DocumentInfoApi.ClearAllSyncInfos);
			Dllmain(_module, fhmApiID, ref fhmApiAddress);
			CheckIn = (_CheckIn)Marshal.GetDelegateForFunctionPointer(new IntPtr(fhmApiAddress), typeof(_CheckIn));

			fhmApiAddress = Convert.ToUInt32(DocumentInfoApi.ClearAllSyncInfos);
			Dllmain(_module, fhmApiID, ref fhmApiAddress);
			UndoCheckOut = (_UndoCheckOut)Marshal.GetDelegateForFunctionPointer(new IntPtr(fhmApiAddress), typeof(_UndoCheckOut));

			fhmApiAddress = Convert.ToUInt32(DocumentInfoApi.ClearAllSyncInfos);
			Dllmain(_module, fhmApiID, ref fhmApiAddress);
			UnWrap = (_UnWrap)Marshal.GetDelegateForFunctionPointer(new IntPtr(fhmApiAddress), typeof(_UnWrap));

			fhmApiAddress = Convert.ToUInt32(DocumentInfoApi.GetSyncList);
			Dllmain(_module, fhmApiID, ref fhmApiAddress);
			GetSyncList = (_GetSyncList)Marshal.GetDelegateForFunctionPointer(new IntPtr(fhmApiAddress), typeof(_GetSyncList));

			if (Init == null
				|| UpdateAllSyncInfos == null
				|| GetSyncInfo == null
				|| ClearAllSyncInfos == null
				|| CheckIn == null
				|| UndoCheckOut == null
				|| UnWrap == null
				|| GetSyncList == null)
			{
				return false;
			}

			return true;
		}
	}
}
