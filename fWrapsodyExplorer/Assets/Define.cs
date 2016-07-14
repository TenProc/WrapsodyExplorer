using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace fWrapsodyExplorer
{
	public static class Define
	{
		public const int SYNC_USER_ID_SIZE = 65;
		public const int SYNC_USER_NAME_SIZE = 129;
		public const int SYNC_SYNC_ID_SIZE = 47;
		public const int SYNC_REVISION_MEMO_SIZE = 513;
		public const int MAX_PATH = 260;
	}
	
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct SYNC_USER_INFO
	{
		/// char[15]
		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_USER_ID_SIZE)]
		public string userId;
		/// int[15]
		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_USER_NAME_SIZE)]
		public string userName;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct SYNC_REVISION_INFO
	{
		/// char[15]
		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_SYNC_ID_SIZE)]
		public string userId;

		public int revisionN;
		public int regTime;

		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_USER_ID_SIZE)]
		public string checkinUserID;

		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_USER_NAME_SIZE)]
		public string checkInUserName;

		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_REVISION_MEMO_SIZE)]
		public string memo;

		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.MAX_PATH)]
		public string fileName;

		uint attribute;
		uint signCount;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct SYNC_NOTIFY_INFO
	{
		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_SYNC_ID_SIZE)]
		public string syncID;

		int nRevisionN;
		int nNotifyType;

		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_USER_ID_SIZE)]
		public string userID;

		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_USER_NAME_SIZE)]
		public string userName;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct SYNC_SIGN_INFO
	{
		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_USER_ID_SIZE)]
		public string userID;

		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_USER_NAME_SIZE)]
		public string userName;

		public int regTime;

		[MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = Define.SYNC_REVISION_MEMO_SIZE)]
		public string memo;
	}
	







}