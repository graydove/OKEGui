﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace OKEGui
{
    public class WindowUtil
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        public static string GetText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        private enum EXECUTION_STATE : uint
        {
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_DISPLAY_REQUIRED = 0x00000002,

            // Legacy flag, should not be used.
            // ES_USER_PRESENT   = 0x00000004,
            ES_CONTINUOUS = 0x80000000,
        }

        public static void PreventSystemPowerdown()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                EXECUTION_STATE.ES_CONTINUOUS);
        }

        public static void AllowSystemPowerdown()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        public static string GetErrorText(int iErrorValue)
        {
            if (iErrorValue >= 0)
                return iErrorValue.ToString();
            string strErrorHex = iErrorValue.ToString("X");
            string strErrorText = String.Empty;

            // http://msdn.microsoft.com/en-us/library/cc704588%28v=prot.10%29.aspx
            // http://nologs.com/ntstatus.html
            switch (strErrorHex) {
                case "00000000": strErrorText = "STATUS_SUCCESS"; break;
                case "00000001": strErrorText = "STATUS_WAIT_1"; break;
                case "00000002": strErrorText = "STATUS_WAIT_2"; break;
                case "00000003": strErrorText = "STATUS_WAIT_3"; break;
                case "0000003F": strErrorText = "STATUS_WAIT_63"; break;
                case "00000080": strErrorText = "STATUS_ABANDONED"; break;
                case "000000BF": strErrorText = "STATUS_ABANDONED_WAIT_63"; break;
                case "000000C0": strErrorText = "STATUS_USER_APC"; break;
                case "00000100": strErrorText = "STATUS_KERNEL_APC"; break;
                case "00000101": strErrorText = "STATUS_ALERTED"; break;
                case "00000102": strErrorText = "STATUS_TIMEOUT"; break;
                case "00000103": strErrorText = "STATUS_PENDING"; break;
                case "00000104": strErrorText = "STATUS_REPARSE"; break;
                case "00000105": strErrorText = "STATUS_MORE_ENTRIES"; break;
                case "00000106": strErrorText = "STATUS_NOT_ALL_ASSIGNED"; break;
                case "00000107": strErrorText = "STATUS_SOME_NOT_MAPPED"; break;
                case "00000108": strErrorText = "STATUS_OPLOCK_BREAK_IN_PROGRESS"; break;
                case "00000109": strErrorText = "STATUS_VOLUME_MOUNTED"; break;
                case "0000010A": strErrorText = "STATUS_RXACT_COMMITTED"; break;
                case "0000010B": strErrorText = "STATUS_NOTIFY_CLEANUP"; break;
                case "0000010C": strErrorText = "STATUS_NOTIFY_ENUM_DIR"; break;
                case "0000010D": strErrorText = "STATUS_NO_QUOTAS_FOR_ACCOUNT"; break;
                case "0000010E": strErrorText = "STATUS_PRIMARY_TRANSPORT_CONNECT_FAILED"; break;
                case "00000110": strErrorText = "STATUS_PAGE_FAULT_TRANSITION"; break;
                case "00000111": strErrorText = "STATUS_PAGE_FAULT_DEMAND_ZERO"; break;
                case "00000112": strErrorText = "STATUS_PAGE_FAULT_COPY_ON_WRITE"; break;
                case "00000113": strErrorText = "STATUS_PAGE_FAULT_GUARD_PAGE"; break;
                case "00000114": strErrorText = "STATUS_PAGE_FAULT_PAGING_FILE"; break;
                case "00000115": strErrorText = "STATUS_CACHE_PAGE_LOCKED"; break;
                case "00000116": strErrorText = "STATUS_CRASH_DUMP"; break;
                case "00000117": strErrorText = "STATUS_BUFFER_ALL_ZEROS"; break;
                case "00000118": strErrorText = "STATUS_REPARSE_OBJECT"; break;
                case "00000119": strErrorText = "STATUS_RESOURCE_REQUIREMENTS_CHANGED"; break;
                case "00000120": strErrorText = "STATUS_TRANSLATION_COMPLETE"; break;
                case "00000121": strErrorText = "STATUS_DS_MEMBERSHIP_EVALUATED_LOCALLY"; break;
                case "00000122": strErrorText = "STATUS_NOTHING_TO_TERMINATE"; break;
                case "00000123": strErrorText = "STATUS_PROCESS_NOT_IN_JOB"; break;
                case "00000124": strErrorText = "STATUS_PROCESS_IN_JOB"; break;
                case "00000125": strErrorText = "STATUS_VOLSNAP_HIBERNATE_READY"; break;
                case "00000126": strErrorText = "STATUS_FSFILTER_OP_COMPLETED_SUCCESSFULLY"; break;
                case "00010001": strErrorText = "DBG_EXCEPTION_HANDLED"; break;
                case "00010002": strErrorText = "DBG_CONTINUE"; break;
                case "40000000": strErrorText = "STATUS_OBJECT_NAME_EXISTS"; break;
                case "40000001": strErrorText = "STATUS_THREAD_WAS_SUSPENDED"; break;
                case "40000002": strErrorText = "STATUS_WORKING_SET_LIMIT_RANGE"; break;
                case "40000003": strErrorText = "STATUS_IMAGE_NOT_AT_BASE"; break;
                case "40000004": strErrorText = "STATUS_RXACT_STATE_CREATED"; break;
                case "40000005": strErrorText = "STATUS_SEGMENT_NOTIFICATION"; break;
                case "40000006": strErrorText = "STATUS_LOCAL_USER_SESSION_KEY"; break;
                case "40000007": strErrorText = "STATUS_BAD_CURRENT_DIRECTORY"; break;
                case "40000008": strErrorText = "STATUS_SERIAL_MORE_WRITES"; break;
                case "40000009": strErrorText = "STATUS_REGISTRY_RECOVERED"; break;
                case "4000000A": strErrorText = "STATUS_FT_READ_RECOVERY_FROM_BACKUP"; break;
                case "4000000B": strErrorText = "STATUS_FT_WRITE_RECOVERY"; break;
                case "4000000C": strErrorText = "STATUS_SERIAL_COUNTER_TIMEOUT"; break;
                case "4000000D": strErrorText = "STATUS_NULL_LM_PASSWORD"; break;
                case "4000000E": strErrorText = "STATUS_IMAGE_MACHINE_TYPE_MISMATCH"; break;
                case "4000000F": strErrorText = "STATUS_RECEIVE_PARTIAL"; break;
                case "40000010": strErrorText = "STATUS_RECEIVE_EXPEDITED"; break;
                case "40000011": strErrorText = "STATUS_RECEIVE_PARTIAL_EXPEDITED"; break;
                case "40000012": strErrorText = "STATUS_EVENT_DONE"; break;
                case "40000013": strErrorText = "STATUS_EVENT_PENDING"; break;
                case "40000014": strErrorText = "STATUS_CHECKING_FILE_SYSTEM"; break;
                case "40000015": strErrorText = "STATUS_FATAL_APP_EXIT"; break;
                case "40000016": strErrorText = "STATUS_PREDEFINED_HANDLE"; break;
                case "40000017": strErrorText = "STATUS_WAS_UNLOCKED"; break;
                case "40000018": strErrorText = "STATUS_SERVICE_NOTIFICATION"; break;
                case "40000019": strErrorText = "STATUS_WAS_LOCKED"; break;
                case "4000001A": strErrorText = "STATUS_LOG_HARD_ERROR"; break;
                case "4000001B": strErrorText = "STATUS_ALREADY_WIN32"; break;
                case "4000001C": strErrorText = "STATUS_WX86_UNSIMULATE"; break;
                case "4000001D": strErrorText = "STATUS_WX86_CONTINUE"; break;
                case "4000001E": strErrorText = "STATUS_WX86_SINGLE_STEP"; break;
                case "4000001F": strErrorText = "STATUS_WX86_BREAKPOINT"; break;
                case "40000020": strErrorText = "STATUS_WX86_EXCEPTION_CONTINUE"; break;
                case "40000021": strErrorText = "STATUS_WX86_EXCEPTION_LASTCHANCE"; break;
                case "40000022": strErrorText = "STATUS_WX86_EXCEPTION_CHAIN"; break;
                case "40000023": strErrorText = "STATUS_IMAGE_MACHINE_TYPE_MISMATCH_EXE"; break;
                case "40000024": strErrorText = "STATUS_NO_YIELD_PERFORMED"; break;
                case "40000025": strErrorText = "STATUS_TIMER_RESUME_IGNORED"; break;
                case "40000026": strErrorText = "STATUS_ARBITRATION_UNHANDLED"; break;
                case "40000027": strErrorText = "STATUS_CARDBUS_NOT_SUPPORTED"; break;
                case "40000028": strErrorText = "STATUS_WX86_CREATEWX86TIB"; break;
                case "40000029": strErrorText = "STATUS_MP_PROCESSOR_MISMATCH"; break;
                case "4000002A": strErrorText = "STATUS_HIBERNATED"; break;
                case "4000002B": strErrorText = "STATUS_RESUME_HIBERNATION"; break;
                case "4000002C": strErrorText = "STATUS_FIRMWARE_UPDATED"; break;
                case "4000002D": strErrorText = "STATUS_DRIVERS_LEAKING_LOCKED_PAGES"; break;
                case "40010001": strErrorText = "DBG_REPLY_LATER"; break;
                case "40010002": strErrorText = "DBG_UNABLE_TO_PROVIDE_HANDLE"; break;
                case "40010003": strErrorText = "DBG_TERMINATE_THREAD"; break;
                case "40010004": strErrorText = "DBG_TERMINATE_PROCESS"; break;
                case "40010005": strErrorText = "DBG_CONTROL_C"; break;
                case "40010006": strErrorText = "DBG_PRINTEXCEPTION_C"; break;
                case "40010007": strErrorText = "DBG_RIPEXCEPTION"; break;
                case "40010008": strErrorText = "DBG_CONTROL_BREAK"; break;
                case "40010009": strErrorText = "DBG_COMMAND_EXCEPTION"; break;
                case "80000001": strErrorText = "STATUS_GUARD_PAGE_VIOLATION"; break;
                case "80000002": strErrorText = "STATUS_DATATYPE_MISALIGNMENT"; break;
                case "80000003": strErrorText = "STATUS_BREAKPOINT"; break;
                case "80000004": strErrorText = "STATUS_SINGLE_STEP"; break;
                case "80000005": strErrorText = "STATUS_BUFFER_OVERFLOW"; break;
                case "80000006": strErrorText = "STATUS_NO_MORE_FILES"; break;
                case "80000007": strErrorText = "STATUS_WAKE_SYSTEM_DEBUGGER"; break;
                case "8000000A": strErrorText = "STATUS_HANDLES_CLOSED"; break;
                case "8000000B": strErrorText = "STATUS_NO_INHERITANCE"; break;
                case "8000000C": strErrorText = "STATUS_GUID_SUBSTITUTION_MADE"; break;
                case "8000000D": strErrorText = "STATUS_PARTIAL_COPY"; break;
                case "8000000E": strErrorText = "STATUS_DEVICE_PAPER_EMPTY"; break;
                case "8000000F": strErrorText = "STATUS_DEVICE_POWERED_OFF"; break;
                case "80000010": strErrorText = "STATUS_DEVICE_OFF_LINE"; break;
                case "80000011": strErrorText = "STATUS_DEVICE_BUSY"; break;
                case "80000012": strErrorText = "STATUS_NO_MORE_EAS"; break;
                case "80000013": strErrorText = "STATUS_INVALID_EA_NAME"; break;
                case "80000014": strErrorText = "STATUS_EA_LIST_INCONSISTENT"; break;
                case "80000015": strErrorText = "STATUS_INVALID_EA_FLAG"; break;
                case "80000016": strErrorText = "STATUS_VERIFY_REQUIRED"; break;
                case "80000017": strErrorText = "STATUS_EXTRANEOUS_INFORMATION"; break;
                case "80000018": strErrorText = "STATUS_RXACT_COMMIT_NECESSARY"; break;
                case "8000001A": strErrorText = "STATUS_NO_MORE_ENTRIES"; break;
                case "8000001B": strErrorText = "STATUS_FILEMARK_DETECTED"; break;
                case "8000001C": strErrorText = "STATUS_MEDIA_CHANGED"; break;
                case "8000001D": strErrorText = "STATUS_BUS_RESET"; break;
                case "8000001E": strErrorText = "STATUS_END_OF_MEDIA"; break;
                case "8000001F": strErrorText = "STATUS_BEGINNING_OF_MEDIA"; break;
                case "80000020": strErrorText = "STATUS_MEDIA_CHECK"; break;
                case "80000021": strErrorText = "STATUS_SETMARK_DETECTED"; break;
                case "80000022": strErrorText = "STATUS_NO_DATA_DETECTED"; break;
                case "80000023": strErrorText = "STATUS_REDIRECTOR_HAS_OPEN_HANDLES"; break;
                case "80000024": strErrorText = "STATUS_SERVER_HAS_OPEN_HANDLES"; break;
                case "80000025": strErrorText = "STATUS_ALREADY_DISCONNECTED"; break;
                case "80000026": strErrorText = "STATUS_LONGJUMP"; break;
                case "80000027": strErrorText = "STATUS_CLEANER_CARTRIDGE_INSTALLED"; break;
                case "80000028": strErrorText = "STATUS_PLUGPLAY_QUERY_VETOED"; break;
                case "80000029": strErrorText = "STATUS_UNWIND_CONSOLIDATE"; break;
                case "8000002A": strErrorText = "STATUS_REGISTRY_HIVE_RECOVERED"; break;
                case "8000002B": strErrorText = "STATUS_DLL_MIGHT_BE_INSECURE"; break;
                case "8000002C": strErrorText = "STATUS_DLL_MIGHT_BE_INCOMPATIBLE"; break;
                case "80010001": strErrorText = "DBG_EXCEPTION_NOT_HANDLED"; break;
                case "80130001": strErrorText = "STATUS_CLUSTER_NODE_ALREADY_UP"; break;
                case "80130002": strErrorText = "STATUS_CLUSTER_NODE_ALREADY_DOWN"; break;
                case "80130003": strErrorText = "STATUS_CLUSTER_NETWORK_ALREADY_ONLINE"; break;
                case "80130004": strErrorText = "STATUS_CLUSTER_NETWORK_ALREADY_OFFLINE"; break;
                case "80130005": strErrorText = "STATUS_CLUSTER_NODE_ALREADY_MEMBER"; break;
                case "C0000001": strErrorText = "STATUS_UNSUCCESSFUL"; break;
                case "C0000002": strErrorText = "STATUS_NOT_IMPLEMENTED"; break;
                case "C0000003": strErrorText = "STATUS_INVALID_INFO_CLASS"; break;
                case "C0000004": strErrorText = "STATUS_INFO_LENGTH_MISMATCH"; break;
                case "C0000005": strErrorText = "STATUS_ACCESS_VIOLATION"; break;
                case "C0000006": strErrorText = "STATUS_IN_PAGE_ERROR"; break;
                case "C0000007": strErrorText = "STATUS_PAGEFILE_QUOTA"; break;
                case "C0000008": strErrorText = "STATUS_INVALID_HANDLE"; break;
                case "C0000009": strErrorText = "STATUS_BAD_INITIAL_STACK"; break;
                case "C000000A": strErrorText = "STATUS_BAD_INITIAL_PC"; break;
                case "C000000B": strErrorText = "STATUS_INVALID_CID"; break;
                case "C000000C": strErrorText = "STATUS_TIMER_NOT_CANCELED"; break;
                case "C000000D": strErrorText = "STATUS_INVALID_PARAMETER"; break;
                case "C000000E": strErrorText = "STATUS_NO_SUCH_DEVICE"; break;
                case "C000000F": strErrorText = "STATUS_NO_SUCH_FILE"; break;
                case "C0000010": strErrorText = "STATUS_INVALID_DEVICE_REQUEST"; break;
                case "C0000011": strErrorText = "STATUS_END_OF_FILE"; break;
                case "C0000012": strErrorText = "STATUS_WRONG_VOLUME"; break;
                case "C0000013": strErrorText = "STATUS_NO_MEDIA_IN_DEVICE"; break;
                case "C0000014": strErrorText = "STATUS_UNRECOGNIZED_MEDIA"; break;
                case "C0000015": strErrorText = "STATUS_NONEXISTENT_SECTOR"; break;
                case "C0000016": strErrorText = "STATUS_MORE_PROCESSING_REQUIRED"; break;
                case "C0000017": strErrorText = "STATUS_NO_MEMORY"; break;
                case "C0000018": strErrorText = "STATUS_CONFLICTING_ADDRESSES"; break;
                case "C0000019": strErrorText = "STATUS_NOT_MAPPED_VIEW"; break;
                case "C000001A": strErrorText = "STATUS_UNABLE_TO_FREE_VM"; break;
                case "C000001B": strErrorText = "STATUS_UNABLE_TO_DELETE_SECTION"; break;
                case "C000001C": strErrorText = "STATUS_INVALID_SYSTEM_SERVICE"; break;
                case "C000001D": strErrorText = "STATUS_ILLEGAL_INSTRUCTION"; break;
                case "C000001E": strErrorText = "STATUS_INVALID_LOCK_SEQUENCE"; break;
                case "C000001F": strErrorText = "STATUS_INVALID_VIEW_SIZE"; break;
                case "C0000020": strErrorText = "STATUS_INVALID_FILE_FOR_SECTION"; break;
                case "C0000021": strErrorText = "STATUS_ALREADY_COMMITTED"; break;
                case "C0000022": strErrorText = "STATUS_ACCESS_DENIED"; break;
                case "C0000023": strErrorText = "STATUS_BUFFER_TOO_SMALL"; break;
                case "C0000024": strErrorText = "STATUS_OBJECT_TYPE_MISMATCH"; break;
                case "C0000025": strErrorText = "STATUS_NONCONTINUABLE_EXCEPTION"; break;
                case "C0000026": strErrorText = "STATUS_INVALID_DISPOSITION"; break;
                case "C0000027": strErrorText = "STATUS_UNWIND"; break;
                case "C0000028": strErrorText = "STATUS_BAD_STACK"; break;
                case "C0000029": strErrorText = "STATUS_INVALID_UNWIND_TARGET"; break;
                case "C000002A": strErrorText = "STATUS_NOT_LOCKED"; break;
                case "C000002B": strErrorText = "STATUS_PARITY_ERROR"; break;
                case "C000002C": strErrorText = "STATUS_UNABLE_TO_DECOMMIT_VM"; break;
                case "C000002D": strErrorText = "STATUS_NOT_COMMITTED"; break;
                case "C000002E": strErrorText = "STATUS_INVALID_PORT_ATTRIBUTES"; break;
                case "C000002F": strErrorText = "STATUS_PORT_MESSAGE_TOO_LONG"; break;
                case "C0000030": strErrorText = "STATUS_INVALID_PARAMETER_MIX"; break;
                case "C0000031": strErrorText = "STATUS_INVALID_QUOTA_LOWER"; break;
                case "C0000032": strErrorText = "STATUS_DISK_CORRUPT_ERROR"; break;
                case "C0000033": strErrorText = "STATUS_OBJECT_NAME_INVALID"; break;
                case "C0000034": strErrorText = "STATUS_OBJECT_NAME_NOT_FOUND"; break;
                case "C0000035": strErrorText = "STATUS_OBJECT_NAME_COLLISION"; break;
                case "C0000037": strErrorText = "STATUS_PORT_DISCONNECTED"; break;
                case "C0000038": strErrorText = "STATUS_DEVICE_ALREADY_ATTACHED"; break;
                case "C0000039": strErrorText = "STATUS_OBJECT_PATH_INVALID"; break;
                case "C000003A": strErrorText = "STATUS_OBJECT_PATH_NOT_FOUND"; break;
                case "C000003B": strErrorText = "STATUS_OBJECT_PATH_SYNTAX_BAD"; break;
                case "C000003C": strErrorText = "STATUS_DATA_OVERRUN"; break;
                case "C000003D": strErrorText = "STATUS_DATA_LATE_ERROR"; break;
                case "C000003E": strErrorText = "STATUS_DATA_ERROR"; break;
                case "C000003F": strErrorText = "STATUS_CRC_ERROR"; break;
                case "C0000040": strErrorText = "STATUS_SECTION_TOO_BIG"; break;
                case "C0000041": strErrorText = "STATUS_PORT_CONNECTION_REFUSED"; break;
                case "C0000042": strErrorText = "STATUS_INVALID_PORT_HANDLE"; break;
                case "C0000043": strErrorText = "STATUS_SHARING_VIOLATION"; break;
                case "C0000044": strErrorText = "STATUS_QUOTA_EXCEEDED"; break;
                case "C0000045": strErrorText = "STATUS_INVALID_PAGE_PROTECTION"; break;
                case "C0000046": strErrorText = "STATUS_MUTANT_NOT_OWNED"; break;
                case "C0000047": strErrorText = "STATUS_SEMAPHORE_LIMIT_EXCEEDED"; break;
                case "C0000048": strErrorText = "STATUS_PORT_ALREADY_SET"; break;
                case "C0000049": strErrorText = "STATUS_SECTION_NOT_IMAGE"; break;
                case "C000004A": strErrorText = "STATUS_SUSPEND_COUNT_EXCEEDED"; break;
                case "C000004B": strErrorText = "STATUS_THREAD_IS_TERMINATING"; break;
                case "C000004C": strErrorText = "STATUS_BAD_WORKING_SET_LIMIT"; break;
                case "C000004D": strErrorText = "STATUS_INCOMPATIBLE_FILE_MAP"; break;
                case "C000004E": strErrorText = "STATUS_SECTION_PROTECTION"; break;
                case "C000004F": strErrorText = "STATUS_EAS_NOT_SUPPORTED"; break;
                case "C0000050": strErrorText = "STATUS_EA_TOO_LARGE"; break;
                case "C0000051": strErrorText = "STATUS_NONEXISTENT_EA_ENTRY"; break;
                case "C0000052": strErrorText = "STATUS_NO_EAS_ON_FILE"; break;
                case "C0000053": strErrorText = "STATUS_EA_CORRUPT_ERROR"; break;
                case "C0000054": strErrorText = "STATUS_FILE_LOCK_CONFLICT"; break;
                case "C0000055": strErrorText = "STATUS_LOCK_NOT_GRANTED"; break;
                case "C0000056": strErrorText = "STATUS_DELETE_PENDING"; break;
                case "C0000057": strErrorText = "STATUS_CTL_FILE_NOT_SUPPORTED"; break;
                case "C0000058": strErrorText = "STATUS_UNKNOWN_REVISION"; break;
                case "C0000059": strErrorText = "STATUS_REVISION_MISMATCH"; break;
                case "C000005A": strErrorText = "STATUS_INVALID_OWNER"; break;
                case "C000005B": strErrorText = "STATUS_INVALID_PRIMARY_GROUP"; break;
                case "C000005C": strErrorText = "STATUS_NO_IMPERSONATION_TOKEN"; break;
                case "C000005D": strErrorText = "STATUS_CANT_DISABLE_MANDATORY"; break;
                case "C000005E": strErrorText = "STATUS_NO_LOGON_SERVERS"; break;
                case "C000005F": strErrorText = "STATUS_NO_SUCH_LOGON_SESSION"; break;
                case "C0000060": strErrorText = "STATUS_NO_SUCH_PRIVILEGE"; break;
                case "C0000061": strErrorText = "STATUS_PRIVILEGE_NOT_HELD"; break;
                case "C0000062": strErrorText = "STATUS_INVALID_ACCOUNT_NAME"; break;
                case "C0000063": strErrorText = "STATUS_USER_EXISTS"; break;
                case "C0000064": strErrorText = "STATUS_NO_SUCH_USER"; break;
                case "C0000065": strErrorText = "STATUS_GROUP_EXISTS"; break;
                case "C0000066": strErrorText = "STATUS_NO_SUCH_GROUP"; break;
                case "C0000067": strErrorText = "STATUS_MEMBER_IN_GROUP"; break;
                case "C0000068": strErrorText = "STATUS_MEMBER_NOT_IN_GROUP"; break;
                case "C0000069": strErrorText = "STATUS_LAST_ADMIN"; break;
                case "C000006A": strErrorText = "STATUS_WRONG_PASSWORD"; break;
                case "C000006B": strErrorText = "STATUS_ILL_FORMED_PASSWORD"; break;
                case "C000006C": strErrorText = "STATUS_PASSWORD_RESTRICTION"; break;
                case "C000006D": strErrorText = "STATUS_LOGON_FAILURE"; break;
                case "C000006E": strErrorText = "STATUS_ACCOUNT_RESTRICTION"; break;
                case "C000006F": strErrorText = "STATUS_INVALID_LOGON_HOURS"; break;
                case "C0000070": strErrorText = "STATUS_INVALID_WORKSTATION"; break;
                case "C0000071": strErrorText = "STATUS_PASSWORD_EXPIRED"; break;
                case "C0000072": strErrorText = "STATUS_ACCOUNT_DISABLED"; break;
                case "C0000073": strErrorText = "STATUS_NONE_MAPPED"; break;
                case "C0000074": strErrorText = "STATUS_TOO_MANY_LUIDS_REQUESTED"; break;
                case "C0000075": strErrorText = "STATUS_LUIDS_EXHAUSTED"; break;
                case "C0000076": strErrorText = "STATUS_INVALID_SUB_AUTHORITY"; break;
                case "C0000077": strErrorText = "STATUS_INVALID_ACL"; break;
                case "C0000078": strErrorText = "STATUS_INVALID_SID"; break;
                case "C0000079": strErrorText = "STATUS_INVALID_SECURITY_DESCR"; break;
                case "C000007A": strErrorText = "STATUS_PROCEDURE_NOT_FOUND"; break;
                case "C000007B": strErrorText = "STATUS_INVALID_IMAGE_FORMAT"; break;
                case "C000007C": strErrorText = "STATUS_NO_TOKEN"; break;
                case "C000007D": strErrorText = "STATUS_BAD_INHERITANCE_ACL"; break;
                case "C000007E": strErrorText = "STATUS_RANGE_NOT_LOCKED"; break;
                case "C000007F": strErrorText = "STATUS_DISK_FULL"; break;
                case "C0000080": strErrorText = "STATUS_SERVER_DISABLED"; break;
                case "C0000081": strErrorText = "STATUS_SERVER_NOT_DISABLED"; break;
                case "C0000082": strErrorText = "STATUS_TOO_MANY_GUIDS_REQUESTED"; break;
                case "C0000083": strErrorText = "STATUS_GUIDS_EXHAUSTED"; break;
                case "C0000084": strErrorText = "STATUS_INVALID_ID_AUTHORITY"; break;
                case "C0000085": strErrorText = "STATUS_AGENTS_EXHAUSTED"; break;
                case "C0000086": strErrorText = "STATUS_INVALID_VOLUME_LABEL"; break;
                case "C0000087": strErrorText = "STATUS_SECTION_NOT_EXTENDED"; break;
                case "C0000088": strErrorText = "STATUS_NOT_MAPPED_DATA"; break;
                case "C0000089": strErrorText = "STATUS_RESOURCE_DATA_NOT_FOUND"; break;
                case "C000008A": strErrorText = "STATUS_RESOURCE_TYPE_NOT_FOUND"; break;
                case "C000008B": strErrorText = "STATUS_RESOURCE_NAME_NOT_FOUND"; break;
                case "C000008C": strErrorText = "STATUS_ARRAY_BOUNDS_EXCEEDED"; break;
                case "C000008D": strErrorText = "STATUS_FLOAT_DENORMAL_OPERAND"; break;
                case "C000008E": strErrorText = "STATUS_FLOAT_DIVIDE_BY_ZERO"; break;
                case "C000008F": strErrorText = "STATUS_FLOAT_INEXACT_RESULT"; break;
                case "C0000090": strErrorText = "STATUS_FLOAT_INVALID_OPERATION"; break;
                case "C0000091": strErrorText = "STATUS_FLOAT_OVERFLOW"; break;
                case "C0000092": strErrorText = "STATUS_FLOAT_STACK_CHECK"; break;
                case "C0000093": strErrorText = "STATUS_FLOAT_UNDERFLOW"; break;
                case "C0000094": strErrorText = "STATUS_INTEGER_DIVIDE_BY_ZERO"; break;
                case "C0000095": strErrorText = "STATUS_INTEGER_OVERFLOW"; break;
                case "C0000096": strErrorText = "STATUS_PRIVILEGED_INSTRUCTION"; break;
                case "C0000097": strErrorText = "STATUS_TOO_MANY_PAGING_FILES"; break;
                case "C0000098": strErrorText = "STATUS_FILE_INVALID"; break;
                case "C0000099": strErrorText = "STATUS_ALLOTTED_SPACE_EXCEEDED"; break;
                case "C000009A": strErrorText = "STATUS_INSUFFICIENT_RESOURCES"; break;
                case "C000009B": strErrorText = "STATUS_DFS_EXIT_PATH_FOUND"; break;
                case "C000009C": strErrorText = "STATUS_DEVICE_DATA_ERROR"; break;
                case "C000009D": strErrorText = "STATUS_DEVICE_NOT_CONNECTED"; break;
                case "C000009E": strErrorText = "STATUS_DEVICE_POWER_FAILURE"; break;
                case "C000009F": strErrorText = "STATUS_FREE_VM_NOT_AT_BASE"; break;
                case "C00000A0": strErrorText = "STATUS_MEMORY_NOT_ALLOCATED"; break;
                case "C00000A1": strErrorText = "STATUS_WORKING_SET_QUOTA"; break;
                case "C00000A2": strErrorText = "STATUS_MEDIA_WRITE_PROTECTED"; break;
                case "C00000A3": strErrorText = "STATUS_DEVICE_NOT_READY"; break;
                case "C00000A4": strErrorText = "STATUS_INVALID_GROUP_ATTRIBUTES"; break;
                case "C00000A5": strErrorText = "STATUS_BAD_IMPERSONATION_LEVEL"; break;
                case "C00000A6": strErrorText = "STATUS_CANT_OPEN_ANONYMOUS"; break;
                case "C00000A7": strErrorText = "STATUS_BAD_VALIDATION_CLASS"; break;
                case "C00000A8": strErrorText = "STATUS_BAD_TOKEN_TYPE"; break;
                case "C00000A9": strErrorText = "STATUS_BAD_MASTER_BOOT_RECORD"; break;
                case "C00000AA": strErrorText = "STATUS_INSTRUCTION_MISALIGNMENT"; break;
                case "C00000AB": strErrorText = "STATUS_INSTANCE_NOT_AVAILABLE"; break;
                case "C00000AC": strErrorText = "STATUS_PIPE_NOT_AVAILABLE"; break;
                case "C00000AD": strErrorText = "STATUS_INVALID_PIPE_STATE"; break;
                case "C00000AE": strErrorText = "STATUS_PIPE_BUSY"; break;
                case "C00000AF": strErrorText = "STATUS_ILLEGAL_FUNCTION"; break;
                case "C00000B0": strErrorText = "STATUS_PIPE_DISCONNECTED"; break;
                case "C00000B1": strErrorText = "STATUS_PIPE_CLOSING"; break;
                case "C00000B2": strErrorText = "STATUS_PIPE_CONNECTED"; break;
                case "C00000B3": strErrorText = "STATUS_PIPE_LISTENING"; break;
                case "C00000B4": strErrorText = "STATUS_INVALID_READ_MODE"; break;
                case "C00000B5": strErrorText = "STATUS_IO_TIMEOUT"; break;
                case "C00000B6": strErrorText = "STATUS_FILE_FORCED_CLOSED"; break;
                case "C00000B7": strErrorText = "STATUS_PROFILING_NOT_STARTED"; break;
                case "C00000B8": strErrorText = "STATUS_PROFILING_NOT_STOPPED"; break;
                case "C00000B9": strErrorText = "STATUS_COULD_NOT_INTERPRET"; break;
                case "C00000BA": strErrorText = "STATUS_FILE_IS_A_DIRECTORY"; break;
                case "C00000BB": strErrorText = "STATUS_NOT_SUPPORTED"; break;
                case "C00000BC": strErrorText = "STATUS_REMOTE_NOT_LISTENING"; break;
                case "C00000BD": strErrorText = "STATUS_DUPLICATE_NAME"; break;
                case "C00000BE": strErrorText = "STATUS_BAD_NETWORK_PATH"; break;
                case "C00000BF": strErrorText = "STATUS_NETWORK_BUSY"; break;
                case "C00000C0": strErrorText = "STATUS_DEVICE_DOES_NOT_EXIST"; break;
                case "C00000C1": strErrorText = "STATUS_TOO_MANY_COMMANDS"; break;
                case "C00000C2": strErrorText = "STATUS_ADAPTER_HARDWARE_ERROR"; break;
                case "C00000C3": strErrorText = "STATUS_INVALID_NETWORK_RESPONSE"; break;
                case "C00000C4": strErrorText = "STATUS_UNEXPECTED_NETWORK_ERROR"; break;
                case "C00000C5": strErrorText = "STATUS_BAD_REMOTE_ADAPTER"; break;
                case "C00000C6": strErrorText = "STATUS_PRINT_QUEUE_FULL"; break;
                case "C00000C7": strErrorText = "STATUS_NO_SPOOL_SPACE"; break;
                case "C00000C8": strErrorText = "STATUS_PRINT_CANCELLED"; break;
                case "C00000C9": strErrorText = "STATUS_NETWORK_NAME_DELETED"; break;
                case "C00000CA": strErrorText = "STATUS_NETWORK_ACCESS_DENIED"; break;
                case "C00000CB": strErrorText = "STATUS_BAD_DEVICE_TYPE"; break;
                case "C00000CC": strErrorText = "STATUS_BAD_NETWORK_NAME"; break;
                case "C00000CD": strErrorText = "STATUS_TOO_MANY_NAMES"; break;
                case "C00000CE": strErrorText = "STATUS_TOO_MANY_SESSIONS"; break;
                case "C00000CF": strErrorText = "STATUS_SHARING_PAUSED"; break;
                case "C00000D0": strErrorText = "STATUS_REQUEST_NOT_ACCEPTED"; break;
                case "C00000D1": strErrorText = "STATUS_REDIRECTOR_PAUSED"; break;
                case "C00000D2": strErrorText = "STATUS_NET_WRITE_FAULT"; break;
                case "C00000D3": strErrorText = "STATUS_PROFILING_AT_LIMIT"; break;
                case "C00000D4": strErrorText = "STATUS_NOT_SAME_DEVICE"; break;
                case "C00000D5": strErrorText = "STATUS_FILE_RENAMED"; break;
                case "C00000D6": strErrorText = "STATUS_VIRTUAL_CIRCUIT_CLOSED"; break;
                case "C00000D7": strErrorText = "STATUS_NO_SECURITY_ON_OBJECT"; break;
                case "C00000D8": strErrorText = "STATUS_CANT_WAIT"; break;
                case "C00000D9": strErrorText = "STATUS_PIPE_EMPTY"; break;
                case "C00000DA": strErrorText = "STATUS_CANT_ACCESS_DOMAIN_INFO"; break;
                case "C00000DB": strErrorText = "STATUS_CANT_TERMINATE_SELF"; break;
                case "C00000DC": strErrorText = "STATUS_INVALID_SERVER_STATE"; break;
                case "C00000DD": strErrorText = "STATUS_INVALID_DOMAIN_STATE"; break;
                case "C00000DE": strErrorText = "STATUS_INVALID_DOMAIN_ROLE"; break;
                case "C00000DF": strErrorText = "STATUS_NO_SUCH_DOMAIN"; break;
                case "C00000E0": strErrorText = "STATUS_DOMAIN_EXISTS"; break;
                case "C00000E1": strErrorText = "STATUS_DOMAIN_LIMIT_EXCEEDED"; break;
                case "C00000E2": strErrorText = "STATUS_OPLOCK_NOT_GRANTED"; break;
                case "C00000E3": strErrorText = "STATUS_INVALID_OPLOCK_PROTOCOL"; break;
                case "C00000E4": strErrorText = "STATUS_INTERNAL_DB_CORRUPTION"; break;
                case "C00000E5": strErrorText = "STATUS_INTERNAL_ERROR"; break;
                case "C00000E6": strErrorText = "STATUS_GENERIC_NOT_MAPPED"; break;
                case "C00000E7": strErrorText = "STATUS_BAD_DESCRIPTOR_FORMAT"; break;
                case "C00000E8": strErrorText = "STATUS_INVALID_USER_BUFFER"; break;
                case "C00000E9": strErrorText = "STATUS_UNEXPECTED_IO_ERROR"; break;
                case "C00000EA": strErrorText = "STATUS_UNEXPECTED_MM_CREATE_ERR"; break;
                case "C00000EB": strErrorText = "STATUS_UNEXPECTED_MM_MAP_ERROR"; break;
                case "C00000EC": strErrorText = "STATUS_UNEXPECTED_MM_EXTEND_ERR"; break;
                case "C00000ED": strErrorText = "STATUS_NOT_LOGON_PROCESS"; break;
                case "C00000EE": strErrorText = "STATUS_LOGON_SESSION_EXISTS"; break;
                case "C00000EF": strErrorText = "STATUS_INVALID_PARAMETER_1"; break;
                case "C00000F0": strErrorText = "STATUS_INVALID_PARAMETER_2"; break;
                case "C00000F1": strErrorText = "STATUS_INVALID_PARAMETER_3"; break;
                case "C00000F2": strErrorText = "STATUS_INVALID_PARAMETER_4"; break;
                case "C00000F3": strErrorText = "STATUS_INVALID_PARAMETER_5"; break;
                case "C00000F4": strErrorText = "STATUS_INVALID_PARAMETER_6"; break;
                case "C00000F5": strErrorText = "STATUS_INVALID_PARAMETER_7"; break;
                case "C00000F6": strErrorText = "STATUS_INVALID_PARAMETER_8"; break;
                case "C00000F7": strErrorText = "STATUS_INVALID_PARAMETER_9"; break;
                case "C00000F8": strErrorText = "STATUS_INVALID_PARAMETER_10"; break;
                case "C00000F9": strErrorText = "STATUS_INVALID_PARAMETER_11"; break;
                case "C00000FA": strErrorText = "STATUS_INVALID_PARAMETER_12"; break;
                case "C00000FB": strErrorText = "STATUS_REDIRECTOR_NOT_STARTED"; break;
                case "C00000FC": strErrorText = "STATUS_REDIRECTOR_STARTED"; break;
                case "C00000FD": strErrorText = "STATUS_STACK_OVERFLOW"; break;
                case "C00000FE": strErrorText = "STATUS_NO_SUCH_PACKAGE"; break;
                case "C00000FF": strErrorText = "STATUS_BAD_FUNCTION_TABLE"; break;
                case "C0000100": strErrorText = "STATUS_VARIABLE_NOT_FOUND"; break;
                case "C0000101": strErrorText = "STATUS_DIRECTORY_NOT_EMPTY"; break;
                case "C0000102": strErrorText = "STATUS_FILE_CORRUPT_ERROR"; break;
                case "C0000103": strErrorText = "STATUS_NOT_A_DIRECTORY"; break;
                case "C0000104": strErrorText = "STATUS_BAD_LOGON_SESSION_STATE"; break;
                case "C0000105": strErrorText = "STATUS_LOGON_SESSION_COLLISION"; break;
                case "C0000106": strErrorText = "STATUS_NAME_TOO_LONG"; break;
                case "C0000107": strErrorText = "STATUS_FILES_OPEN"; break;
                case "C0000108": strErrorText = "STATUS_CONNECTION_IN_USE"; break;
                case "C0000109": strErrorText = "STATUS_MESSAGE_NOT_FOUND"; break;
                case "C000010A": strErrorText = "STATUS_PROCESS_IS_TERMINATING"; break;
                case "C000010B": strErrorText = "STATUS_INVALID_LOGON_TYPE"; break;
                case "C000010C": strErrorText = "STATUS_NO_GUID_TRANSLATION"; break;
                case "C000010D": strErrorText = "STATUS_CANNOT_IMPERSONATE"; break;
                case "C000010E": strErrorText = "STATUS_IMAGE_ALREADY_LOADED"; break;
                case "C000010F": strErrorText = "STATUS_ABIOS_NOT_PRESENT"; break;
                case "C0000110": strErrorText = "STATUS_ABIOS_LID_NOT_EXIST"; break;
                case "C0000111": strErrorText = "STATUS_ABIOS_LID_ALREADY_OWNED"; break;
                case "C0000112": strErrorText = "STATUS_ABIOS_NOT_LID_OWNER"; break;
                case "C0000113": strErrorText = "STATUS_ABIOS_INVALID_COMMAND"; break;
                case "C0000114": strErrorText = "STATUS_ABIOS_INVALID_LID"; break;
                case "C0000115": strErrorText = "STATUS_ABIOS_SELECTOR_NOT_AVAILABLE"; break;
                case "C0000116": strErrorText = "STATUS_ABIOS_INVALID_SELECTOR"; break;
                case "C0000117": strErrorText = "STATUS_NO_LDT"; break;
                case "C0000118": strErrorText = "STATUS_INVALID_LDT_SIZE"; break;
                case "C0000119": strErrorText = "STATUS_INVALID_LDT_OFFSET"; break;
                case "C000011A": strErrorText = "STATUS_INVALID_LDT_DESCRIPTOR"; break;
                case "C000011B": strErrorText = "STATUS_INVALID_IMAGE_NE_FORMAT"; break;
                case "C000011C": strErrorText = "STATUS_RXACT_INVALID_STATE"; break;
                case "C000011D": strErrorText = "STATUS_RXACT_COMMIT_FAILURE"; break;
                case "C000011E": strErrorText = "STATUS_MAPPED_FILE_SIZE_ZERO"; break;
                case "C000011F": strErrorText = "STATUS_TOO_MANY_OPENED_FILES"; break;
                case "C0000120": strErrorText = "STATUS_CANCELLED"; break;
                case "C0000121": strErrorText = "STATUS_CANNOT_DELETE"; break;
                case "C0000122": strErrorText = "STATUS_INVALID_COMPUTER_NAME"; break;
                case "C0000123": strErrorText = "STATUS_FILE_DELETED"; break;
                case "C0000124": strErrorText = "STATUS_SPECIAL_ACCOUNT"; break;
                case "C0000125": strErrorText = "STATUS_SPECIAL_GROUP"; break;
                case "C0000126": strErrorText = "STATUS_SPECIAL_USER"; break;
                case "C0000127": strErrorText = "STATUS_MEMBERS_PRIMARY_GROUP"; break;
                case "C0000128": strErrorText = "STATUS_FILE_CLOSED"; break;
                case "C0000129": strErrorText = "STATUS_TOO_MANY_THREADS"; break;
                case "C000012A": strErrorText = "STATUS_THREAD_NOT_IN_PROCESS"; break;
                case "C000012B": strErrorText = "STATUS_TOKEN_ALREADY_IN_USE"; break;
                case "C000012C": strErrorText = "STATUS_PAGEFILE_QUOTA_EXCEEDED"; break;
                case "C000012D": strErrorText = "STATUS_COMMITMENT_LIMIT"; break;
                case "C000012E": strErrorText = "STATUS_INVALID_IMAGE_LE_FORMAT"; break;
                case "C000012F": strErrorText = "STATUS_INVALID_IMAGE_NOT_MZ"; break;
                case "C0000130": strErrorText = "STATUS_INVALID_IMAGE_PROTECT"; break;
                case "C0000131": strErrorText = "STATUS_INVALID_IMAGE_WIN_16"; break;
                case "C0000132": strErrorText = "STATUS_LOGON_SERVER_CONFLICT"; break;
                case "C0000133": strErrorText = "STATUS_TIME_DIFFERENCE_AT_DC"; break;
                case "C0000134": strErrorText = "STATUS_SYNCHRONIZATION_REQUIRED"; break;
                case "C0000135": strErrorText = "STATUS_DLL_NOT_FOUND"; break;
                case "C0000136": strErrorText = "STATUS_OPEN_FAILED"; break;
                case "C0000137": strErrorText = "STATUS_IO_PRIVILEGE_FAILED"; break;
                case "C0000138": strErrorText = "STATUS_ORDINAL_NOT_FOUND"; break;
                case "C0000139": strErrorText = "STATUS_ENTRYPOINT_NOT_FOUND"; break;
                case "C000013A": strErrorText = "STATUS_CONTROL_C_EXIT"; break;
                case "C000013B": strErrorText = "STATUS_LOCAL_DISCONNECT"; break;
                case "C000013C": strErrorText = "STATUS_REMOTE_DISCONNECT"; break;
                case "C000013D": strErrorText = "STATUS_REMOTE_RESOURCES"; break;
                case "C000013E": strErrorText = "STATUS_LINK_FAILED"; break;
                case "C000013F": strErrorText = "STATUS_LINK_TIMEOUT"; break;
                case "C0000140": strErrorText = "STATUS_INVALID_CONNECTION"; break;
                case "C0000141": strErrorText = "STATUS_INVALID_ADDRESS"; break;
                case "C0000142": strErrorText = "STATUS_DLL_INIT_FAILED"; break;
                case "C0000143": strErrorText = "STATUS_MISSING_SYSTEMFILE"; break;
                case "C0000144": strErrorText = "STATUS_UNHANDLED_EXCEPTION"; break;
                case "C0000145": strErrorText = "STATUS_APP_INIT_FAILURE"; break;
                case "C0000146": strErrorText = "STATUS_PAGEFILE_CREATE_FAILED"; break;
                case "C0000147": strErrorText = "STATUS_NO_PAGEFILE"; break;
                case "C0000148": strErrorText = "STATUS_INVALID_LEVEL"; break;
                case "C0000149": strErrorText = "STATUS_WRONG_PASSWORD_CORE"; break;
                case "C000014A": strErrorText = "STATUS_ILLEGAL_FLOAT_CONTEXT"; break;
                case "C000014B": strErrorText = "STATUS_PIPE_BROKEN"; break;
                case "C000014C": strErrorText = "STATUS_REGISTRY_CORRUPT"; break;
                case "C000014D": strErrorText = "STATUS_REGISTRY_IO_FAILED"; break;
                case "C000014E": strErrorText = "STATUS_NO_EVENT_PAIR"; break;
                case "C000014F": strErrorText = "STATUS_UNRECOGNIZED_VOLUME"; break;
                case "C0000150": strErrorText = "STATUS_SERIAL_NO_DEVICE_INITED"; break;
                case "C0000151": strErrorText = "STATUS_NO_SUCH_ALIAS"; break;
                case "C0000152": strErrorText = "STATUS_MEMBER_NOT_IN_ALIAS"; break;
                case "C0000153": strErrorText = "STATUS_MEMBER_IN_ALIAS"; break;
                case "C0000154": strErrorText = "STATUS_ALIAS_EXISTS"; break;
                case "C0000155": strErrorText = "STATUS_LOGON_NOT_GRANTED"; break;
                case "C0000156": strErrorText = "STATUS_TOO_MANY_SECRETS"; break;
                case "C0000157": strErrorText = "STATUS_SECRET_TOO_LONG"; break;
                case "C0000158": strErrorText = "STATUS_INTERNAL_DB_ERROR"; break;
                case "C0000159": strErrorText = "STATUS_FULLSCREEN_MODE"; break;
                case "C000015A": strErrorText = "STATUS_TOO_MANY_CONTEXT_IDS"; break;
                case "C000015B": strErrorText = "STATUS_LOGON_TYPE_NOT_GRANTED"; break;
                case "C000015C": strErrorText = "STATUS_NOT_REGISTRY_FILE"; break;
                case "C000015D": strErrorText = "STATUS_NT_CROSS_ENCRYPTION_REQUIRED"; break;
                case "C000015E": strErrorText = "STATUS_DOMAIN_CTRLR_CONFIG_ERROR"; break;
                case "C000015F": strErrorText = "STATUS_FT_MISSING_MEMBER"; break;
                case "C0000160": strErrorText = "STATUS_ILL_FORMED_SERVICE_ENTRY"; break;
                case "C0000161": strErrorText = "STATUS_ILLEGAL_CHARACTER"; break;
                case "C0000162": strErrorText = "STATUS_UNMAPPABLE_CHARACTER"; break;
                case "C0000163": strErrorText = "STATUS_UNDEFINED_CHARACTER"; break;
                case "C0000164": strErrorText = "STATUS_FLOPPY_VOLUME"; break;
                case "C0000165": strErrorText = "STATUS_FLOPPY_ID_MARK_NOT_FOUND"; break;
                case "C0000166": strErrorText = "STATUS_FLOPPY_WRONG_CYLINDER"; break;
                case "C0000167": strErrorText = "STATUS_FLOPPY_UNKNOWN_ERROR"; break;
                case "C0000168": strErrorText = "STATUS_FLOPPY_BAD_REGISTERS"; break;
                case "C0000169": strErrorText = "STATUS_DISK_RECALIBRATE_FAILED"; break;
                case "C000016A": strErrorText = "STATUS_DISK_OPERATION_FAILED"; break;
                case "C000016B": strErrorText = "STATUS_DISK_RESET_FAILED"; break;
                case "C000016C": strErrorText = "STATUS_SHARED_IRQ_BUSY"; break;
                case "C000016D": strErrorText = "STATUS_FT_ORPHANING"; break;
                case "C000016E": strErrorText = "STATUS_BIOS_FAILED_TO_CONNECT_INTERRUPT"; break;
                case "C0000172": strErrorText = "STATUS_PARTITION_FAILURE"; break;
                case "C0000173": strErrorText = "STATUS_INVALID_BLOCK_LENGTH"; break;
                case "C0000174": strErrorText = "STATUS_DEVICE_NOT_PARTITIONED"; break;
                case "C0000175": strErrorText = "STATUS_UNABLE_TO_LOCK_MEDIA"; break;
                case "C0000176": strErrorText = "STATUS_UNABLE_TO_UNLOAD_MEDIA"; break;
                case "C0000177": strErrorText = "STATUS_EOM_OVERFLOW"; break;
                case "C0000178": strErrorText = "STATUS_NO_MEDIA"; break;
                case "C000017A": strErrorText = "STATUS_NO_SUCH_MEMBER"; break;
                case "C000017B": strErrorText = "STATUS_INVALID_MEMBER"; break;
                case "C000017C": strErrorText = "STATUS_KEY_DELETED"; break;
                case "C000017D": strErrorText = "STATUS_NO_LOG_SPACE"; break;
                case "C000017E": strErrorText = "STATUS_TOO_MANY_SIDS"; break;
                case "C000017F": strErrorText = "STATUS_LM_CROSS_ENCRYPTION_REQUIRED"; break;
                case "C0000180": strErrorText = "STATUS_KEY_HAS_CHILDREN"; break;
                case "C0000181": strErrorText = "STATUS_CHILD_MUST_BE_VOLATILE"; break;
                case "C0000182": strErrorText = "STATUS_DEVICE_CONFIGURATION_ERROR"; break;
                case "C0000183": strErrorText = "STATUS_DRIVER_INTERNAL_ERROR"; break;
                case "C0000184": strErrorText = "STATUS_INVALID_DEVICE_STATE"; break;
                case "C0000185": strErrorText = "STATUS_IO_DEVICE_ERROR"; break;
                case "C0000186": strErrorText = "STATUS_DEVICE_PROTOCOL_ERROR"; break;
                case "C0000187": strErrorText = "STATUS_BACKUP_CONTROLLER"; break;
                case "C0000188": strErrorText = "STATUS_LOG_FILE_FULL"; break;
                case "C0000189": strErrorText = "STATUS_TOO_LATE"; break;
                case "C000018A": strErrorText = "STATUS_NO_TRUST_LSA_SECRET"; break;
                case "C000018B": strErrorText = "STATUS_NO_TRUST_SAM_ACCOUNT"; break;
                case "C000018C": strErrorText = "STATUS_TRUSTED_DOMAIN_FAILURE"; break;
                case "C000018D": strErrorText = "STATUS_TRUSTED_RELATIONSHIP_FAILURE"; break;
                case "C000018E": strErrorText = "STATUS_EVENTLOG_FILE_CORRUPT"; break;
                case "C000018F": strErrorText = "STATUS_EVENTLOG_CANT_START"; break;
                case "C0000190": strErrorText = "STATUS_TRUST_FAILURE"; break;
                case "C0000191": strErrorText = "STATUS_MUTANT_LIMIT_EXCEEDED"; break;
                case "C0000192": strErrorText = "STATUS_NETLOGON_NOT_STARTED"; break;
                case "C0000193": strErrorText = "STATUS_ACCOUNT_EXPIRED"; break;
                case "C0000194": strErrorText = "STATUS_POSSIBLE_DEADLOCK"; break;
                case "C0000195": strErrorText = "STATUS_NETWORK_CREDENTIAL_CONFLICT"; break;
                case "C0000196": strErrorText = "STATUS_REMOTE_SESSION_LIMIT"; break;
                case "C0000197": strErrorText = "STATUS_EVENTLOG_FILE_CHANGED"; break;
                case "C0000198": strErrorText = "STATUS_NOLOGON_INTERDOMAIN_TRUST_ACCOUNT"; break;
                case "C0000199": strErrorText = "STATUS_NOLOGON_WORKSTATION_TRUST_ACCOUNT"; break;
                case "C000019A": strErrorText = "STATUS_NOLOGON_SERVER_TRUST_ACCOUNT"; break;
                case "C000019B": strErrorText = "STATUS_DOMAIN_TRUST_INCONSISTENT"; break;
                case "C000019C": strErrorText = "STATUS_FS_DRIVER_REQUIRED"; break;
                case "C0000202": strErrorText = "STATUS_NO_USER_SESSION_KEY"; break;
                case "C0000203": strErrorText = "STATUS_USER_SESSION_DELETED"; break;
                case "C0000204": strErrorText = "STATUS_RESOURCE_LANG_NOT_FOUND"; break;
                case "C0000205": strErrorText = "STATUS_INSUFF_SERVER_RESOURCES"; break;
                case "C0000206": strErrorText = "STATUS_INVALID_BUFFER_SIZE"; break;
                case "C0000207": strErrorText = "STATUS_INVALID_ADDRESS_COMPONENT"; break;
                case "C0000208": strErrorText = "STATUS_INVALID_ADDRESS_WILDCARD"; break;
                case "C0000209": strErrorText = "STATUS_TOO_MANY_ADDRESSES"; break;
                case "C000020A": strErrorText = "STATUS_ADDRESS_ALREADY_EXISTS"; break;
                case "C000020B": strErrorText = "STATUS_ADDRESS_CLOSED"; break;
                case "C000020C": strErrorText = "STATUS_CONNECTION_DISCONNECTED"; break;
                case "C000020D": strErrorText = "STATUS_CONNECTION_RESET"; break;
                case "C000020E": strErrorText = "STATUS_TOO_MANY_NODES"; break;
                case "C000020F": strErrorText = "STATUS_TRANSACTION_ABORTED"; break;
                case "C0000210": strErrorText = "STATUS_TRANSACTION_TIMED_OUT"; break;
                case "C0000211": strErrorText = "STATUS_TRANSACTION_NO_RELEASE"; break;
                case "C0000212": strErrorText = "STATUS_TRANSACTION_NO_MATCH"; break;
                case "C0000213": strErrorText = "STATUS_TRANSACTION_RESPONDED"; break;
                case "C0000214": strErrorText = "STATUS_TRANSACTION_INVALID_ID"; break;
                case "C0000215": strErrorText = "STATUS_TRANSACTION_INVALID_TYPE"; break;
                case "C0000216": strErrorText = "STATUS_NOT_SERVER_SESSION"; break;
                case "C0000217": strErrorText = "STATUS_NOT_CLIENT_SESSION"; break;
                case "C0000218": strErrorText = "STATUS_CANNOT_LOAD_REGISTRY_FILE"; break;
                case "C0000219": strErrorText = "STATUS_DEBUG_ATTACH_FAILED"; break;
                case "C000021A": strErrorText = "STATUS_SYSTEM_PROCESS_TERMINATED"; break;
                case "C000021B": strErrorText = "STATUS_DATA_NOT_ACCEPTED"; break;
                case "C000021C": strErrorText = "STATUS_NO_BROWSER_SERVERS_FOUND"; break;
                case "C000021D": strErrorText = "STATUS_VDM_HARD_ERROR"; break;
                case "C000021E": strErrorText = "STATUS_DRIVER_CANCEL_TIMEOUT"; break;
                case "C000021F": strErrorText = "STATUS_REPLY_MESSAGE_MISMATCH"; break;
                case "C0000220": strErrorText = "STATUS_MAPPED_ALIGNMENT"; break;
                case "C0000221": strErrorText = "STATUS_IMAGE_CHECKSUM_MISMATCH"; break;
                case "C0000222": strErrorText = "STATUS_LOST_WRITEBEHIND_DATA"; break;
                case "C0000223": strErrorText = "STATUS_CLIENT_SERVER_PARAMETERS_INVALID"; break;
                case "C0000224": strErrorText = "STATUS_PASSWORD_MUST_CHANGE"; break;
                case "C0000225": strErrorText = "STATUS_NOT_FOUND"; break;
                case "C0000226": strErrorText = "STATUS_NOT_TINY_STREAM"; break;
                case "C0000227": strErrorText = "STATUS_RECOVERY_FAILURE"; break;
                case "C0000228": strErrorText = "STATUS_STACK_OVERFLOW_READ"; break;
                case "C0000229": strErrorText = "STATUS_FAIL_CHECK"; break;
                case "C000022A": strErrorText = "STATUS_DUPLICATE_OBJECTID"; break;
                case "C000022B": strErrorText = "STATUS_OBJECTID_EXISTS"; break;
                case "C000022C": strErrorText = "STATUS_CONVERT_TO_LARGE"; break;
                case "C000022D": strErrorText = "STATUS_RETRY"; break;
                case "C000022E": strErrorText = "STATUS_FOUND_OUT_OF_SCOPE"; break;
                case "C000022F": strErrorText = "STATUS_ALLOCATE_BUCKET"; break;
                case "C0000230": strErrorText = "STATUS_PROPSET_NOT_FOUND"; break;
                case "C0000231": strErrorText = "STATUS_MARSHALL_OVERFLOW"; break;
                case "C0000232": strErrorText = "STATUS_INVALID_VARIANT"; break;
                case "C0000233": strErrorText = "STATUS_DOMAIN_CONTROLLER_NOT_FOUND"; break;
                case "C0000234": strErrorText = "STATUS_ACCOUNT_LOCKED_OUT"; break;
                case "C0000235": strErrorText = "STATUS_HANDLE_NOT_CLOSABLE"; break;
                case "C0000236": strErrorText = "STATUS_CONNECTION_REFUSED"; break;
                case "C0000237": strErrorText = "STATUS_GRACEFUL_DISCONNECT"; break;
                case "C0000238": strErrorText = "STATUS_ADDRESS_ALREADY_ASSOCIATED"; break;
                case "C0000239": strErrorText = "STATUS_ADDRESS_NOT_ASSOCIATED"; break;
                case "C000023A": strErrorText = "STATUS_CONNECTION_INVALID"; break;
                case "C000023B": strErrorText = "STATUS_CONNECTION_ACTIVE"; break;
                case "C000023C": strErrorText = "STATUS_NETWORK_UNREACHABLE"; break;
                case "C000023D": strErrorText = "STATUS_HOST_UNREACHABLE"; break;
                case "C000023E": strErrorText = "STATUS_PROTOCOL_UNREACHABLE"; break;
                case "C000023F": strErrorText = "STATUS_PORT_UNREACHABLE"; break;
                case "C0000240": strErrorText = "STATUS_REQUEST_ABORTED"; break;
                case "C0000241": strErrorText = "STATUS_CONNECTION_ABORTED"; break;
                case "C0000242": strErrorText = "STATUS_BAD_COMPRESSION_BUFFER"; break;
                case "C0000243": strErrorText = "STATUS_USER_MAPPED_FILE"; break;
                case "C0000244": strErrorText = "STATUS_AUDIT_FAILED"; break;
                case "C0000245": strErrorText = "STATUS_TIMER_RESOLUTION_NOT_SET"; break;
                case "C0000246": strErrorText = "STATUS_CONNECTION_COUNT_LIMIT"; break;
                case "C0000247": strErrorText = "STATUS_LOGIN_TIME_RESTRICTION"; break;
                case "C0000248": strErrorText = "STATUS_LOGIN_WKSTA_RESTRICTION"; break;
                case "C0000249": strErrorText = "STATUS_IMAGE_MP_UP_MISMATCH"; break;
                case "C0000250": strErrorText = "STATUS_INSUFFICIENT_LOGON_INFO"; break;
                case "C0000251": strErrorText = "STATUS_BAD_DLL_ENTRYPOINT"; break;
                case "C0000252": strErrorText = "STATUS_BAD_SERVICE_ENTRYPOINT"; break;
                case "C0000253": strErrorText = "STATUS_LPC_REPLY_LOST"; break;
                case "C0000254": strErrorText = "STATUS_IP_ADDRESS_CONFLICT1"; break;
                case "C0000255": strErrorText = "STATUS_IP_ADDRESS_CONFLICT2"; break;
                case "C0000256": strErrorText = "STATUS_REGISTRY_QUOTA_LIMIT"; break;
                case "C0000257": strErrorText = "STATUS_PATH_NOT_COVERED"; break;
                case "C0000258": strErrorText = "STATUS_NO_CALLBACK_ACTIVE"; break;
                case "C0000259": strErrorText = "STATUS_LICENSE_QUOTA_EXCEEDED"; break;
                case "C000025A": strErrorText = "STATUS_PWD_TOO_SHORT"; break;
                case "C000025B": strErrorText = "STATUS_PWD_TOO_RECENT"; break;
                case "C000025C": strErrorText = "STATUS_PWD_HISTORY_CONFLICT"; break;
                case "C000025E": strErrorText = "STATUS_PLUGPLAY_NO_DEVICE"; break;
                case "C000025F": strErrorText = "STATUS_UNSUPPORTED_COMPRESSION"; break;
                case "C0000260": strErrorText = "STATUS_INVALID_HW_PROFILE"; break;
                case "C0000261": strErrorText = "STATUS_INVALID_PLUGPLAY_DEVICE_PATH"; break;
                case "C0000262": strErrorText = "STATUS_DRIVER_ORDINAL_NOT_FOUND"; break;
                case "C0000263": strErrorText = "STATUS_DRIVER_ENTRYPOINT_NOT_FOUND"; break;
                case "C0000264": strErrorText = "STATUS_RESOURCE_NOT_OWNED"; break;
                case "C0000265": strErrorText = "STATUS_TOO_MANY_LINKS"; break;
                case "C0000266": strErrorText = "STATUS_QUOTA_LIST_INCONSISTENT"; break;
                case "C0000267": strErrorText = "STATUS_FILE_IS_OFFLINE"; break;
                case "C0000268": strErrorText = "STATUS_EVALUATION_EXPIRATION"; break;
                case "C0000269": strErrorText = "STATUS_ILLEGAL_DLL_RELOCATION"; break;
                case "C000026A": strErrorText = "STATUS_LICENSE_VIOLATION"; break;
                case "C000026B": strErrorText = "STATUS_DLL_INIT_FAILED_LOGOFF"; break;
                case "C000026C": strErrorText = "STATUS_DRIVER_UNABLE_TO_LOAD"; break;
                case "C000026D": strErrorText = "STATUS_DFS_UNAVAILABLE"; break;
                case "C000026E": strErrorText = "STATUS_VOLUME_DISMOUNTED"; break;
                case "C000026F": strErrorText = "STATUS_WX86_INTERNAL_ERROR"; break;
                case "C0000270": strErrorText = "STATUS_WX86_FLOAT_STACK_CHECK"; break;
                case "C0000271": strErrorText = "STATUS_VALIDATE_CONTINUE"; break;
                case "C0000272": strErrorText = "STATUS_NO_MATCH"; break;
                case "C0000273": strErrorText = "STATUS_NO_MORE_MATCHES"; break;
                case "C0000275": strErrorText = "STATUS_NOT_A_REPARSE_POINT"; break;
                case "C0000276": strErrorText = "STATUS_IO_REPARSE_TAG_INVALID"; break;
                case "C0000277": strErrorText = "STATUS_IO_REPARSE_TAG_MISMATCH"; break;
                case "C0000278": strErrorText = "STATUS_IO_REPARSE_DATA_INVALID"; break;
                case "C0000279": strErrorText = "STATUS_IO_REPARSE_TAG_NOT_HANDLED"; break;
                case "C0000280": strErrorText = "STATUS_REPARSE_POINT_NOT_RESOLVED"; break;
                case "C0000281": strErrorText = "STATUS_DIRECTORY_IS_A_REPARSE_POINT"; break;
                case "C0000282": strErrorText = "STATUS_RANGE_LIST_CONFLICT"; break;
                case "C0000283": strErrorText = "STATUS_SOURCE_ELEMENT_EMPTY"; break;
                case "C0000284": strErrorText = "STATUS_DESTINATION_ELEMENT_FULL"; break;
                case "C0000285": strErrorText = "STATUS_ILLEGAL_ELEMENT_ADDRESS"; break;
                case "C0000286": strErrorText = "STATUS_MAGAZINE_NOT_PRESENT"; break;
                case "C0000287": strErrorText = "STATUS_REINITIALIZATION_NEEDED"; break;
                case "80000288": strErrorText = "STATUS_DEVICE_REQUIRES_CLEANING"; break;
                case "80000289": strErrorText = "STATUS_DEVICE_DOOR_OPEN"; break;
                case "C000028A": strErrorText = "STATUS_ENCRYPTION_FAILED"; break;
                case "C000028B": strErrorText = "STATUS_DECRYPTION_FAILED"; break;
                case "C000028C": strErrorText = "STATUS_RANGE_NOT_FOUND"; break;
                case "C000028D": strErrorText = "STATUS_NO_RECOVERY_POLICY"; break;
                case "C000028E": strErrorText = "STATUS_NO_EFS"; break;
                case "C000028F": strErrorText = "STATUS_WRONG_EFS"; break;
                case "C0000290": strErrorText = "STATUS_NO_USER_KEYS"; break;
                case "C0000291": strErrorText = "STATUS_FILE_NOT_ENCRYPTED"; break;
                case "C0000292": strErrorText = "STATUS_NOT_EXPORT_FORMAT"; break;
                case "C0000293": strErrorText = "STATUS_FILE_ENCRYPTED"; break;
                case "40000294": strErrorText = "STATUS_WAKE_SYSTEM"; break;
                case "C0000295": strErrorText = "STATUS_WMI_GUID_NOT_FOUND"; break;
                case "C0000296": strErrorText = "STATUS_WMI_INSTANCE_NOT_FOUND"; break;
                case "C0000297": strErrorText = "STATUS_WMI_ITEMID_NOT_FOUND"; break;
                case "C0000298": strErrorText = "STATUS_WMI_TRY_AGAIN"; break;
                case "C0000299": strErrorText = "STATUS_SHARED_POLICY"; break;
                case "C000029A": strErrorText = "STATUS_POLICY_OBJECT_NOT_FOUND"; break;
                case "C000029B": strErrorText = "STATUS_POLICY_ONLY_IN_DS"; break;
                case "C000029C": strErrorText = "STATUS_VOLUME_NOT_UPGRADED"; break;
                case "C000029D": strErrorText = "STATUS_REMOTE_STORAGE_NOT_ACTIVE"; break;
                case "C000029E": strErrorText = "STATUS_REMOTE_STORAGE_MEDIA_ERROR"; break;
                case "C000029F": strErrorText = "STATUS_NO_TRACKING_SERVICE"; break;
                case "C00002A0": strErrorText = "STATUS_SERVER_SID_MISMATCH"; break;
                case "C00002A1": strErrorText = "STATUS_DS_NO_ATTRIBUTE_OR_VALUE"; break;
                case "C00002A2": strErrorText = "STATUS_DS_INVALID_ATTRIBUTE_SYNTAX"; break;
                case "C00002A3": strErrorText = "STATUS_DS_ATTRIBUTE_TYPE_UNDEFINED"; break;
                case "C00002A4": strErrorText = "STATUS_DS_ATTRIBUTE_OR_VALUE_EXISTS"; break;
                case "C00002A5": strErrorText = "STATUS_DS_BUSY"; break;
                case "C00002A6": strErrorText = "STATUS_DS_UNAVAILABLE"; break;
                case "C00002A7": strErrorText = "STATUS_DS_NO_RIDS_ALLOCATED"; break;
                case "C00002A8": strErrorText = "STATUS_DS_NO_MORE_RIDS"; break;
                case "C00002A9": strErrorText = "STATUS_DS_INCORRECT_ROLE_OWNER"; break;
                case "C00002AA": strErrorText = "STATUS_DS_RIDMGR_INIT_ERROR"; break;
                case "C00002AB": strErrorText = "STATUS_DS_OBJ_CLASS_VIOLATION"; break;
                case "C00002AC": strErrorText = "STATUS_DS_CANT_ON_NON_LEAF"; break;
                case "C00002AD": strErrorText = "STATUS_DS_CANT_ON_RDN"; break;
                case "C00002AE": strErrorText = "STATUS_DS_CANT_MOD_OBJ_CLASS"; break;
                case "C00002AF": strErrorText = "STATUS_DS_CROSS_DOM_MOVE_FAILED"; break;
                case "C00002B0": strErrorText = "STATUS_DS_GC_NOT_AVAILABLE"; break;
                case "C00002B1": strErrorText = "STATUS_DIRECTORY_SERVICE_REQUIRED"; break;
                case "C00002B2": strErrorText = "STATUS_REPARSE_ATTRIBUTE_CONFLICT"; break;
                case "C00002B3": strErrorText = "STATUS_CANT_ENABLE_DENY_ONLY"; break;
                case "C00002B4": strErrorText = "STATUS_FLOAT_MULTIPLE_FAULTS"; break;
                case "C00002B5": strErrorText = "STATUS_FLOAT_MULTIPLE_TRAPS"; break;
                case "C00002B6": strErrorText = "STATUS_DEVICE_REMOVED"; break;
                case "C00002B7": strErrorText = "STATUS_JOURNAL_DELETE_IN_PROGRESS"; break;
                case "C00002B8": strErrorText = "STATUS_JOURNAL_NOT_ACTIVE"; break;
                case "C00002B9": strErrorText = "STATUS_NOINTERFACE"; break;
                case "C00002C1": strErrorText = "STATUS_DS_ADMIN_LIMIT_EXCEEDED"; break;
                case "C00002C2": strErrorText = "STATUS_DRIVER_FAILED_SLEEP"; break;
                case "C00002C3": strErrorText = "STATUS_MUTUAL_AUTHENTICATION_FAILED"; break;
                case "C00002C4": strErrorText = "STATUS_CORRUPT_SYSTEM_FILE"; break;
                case "C00002C5": strErrorText = "STATUS_DATATYPE_MISALIGNMENT_ERROR"; break;
                case "C00002C6": strErrorText = "STATUS_WMI_READ_ONLY"; break;
                case "C00002C7": strErrorText = "STATUS_WMI_SET_FAILURE"; break;
                case "C00002C8": strErrorText = "STATUS_COMMITMENT_MINIMUM"; break;
                case "C00002C9": strErrorText = "STATUS_REG_NAT_CONSUMPTION"; break;
                case "C00002CA": strErrorText = "STATUS_TRANSPORT_FULL"; break;
                case "C00002CB": strErrorText = "STATUS_DS_SAM_INIT_FAILURE"; break;
                case "C00002CC": strErrorText = "STATUS_ONLY_IF_CONNECTED"; break;
                case "C00002CD": strErrorText = "STATUS_DS_SENSITIVE_GROUP_VIOLATION"; break;
                case "C00002CE": strErrorText = "STATUS_PNP_RESTART_ENUMERATION"; break;
                case "C00002CF": strErrorText = "STATUS_JOURNAL_ENTRY_DELETED"; break;
                case "C00002D0": strErrorText = "STATUS_DS_CANT_MOD_PRIMARYGROUPID"; break;
                case "C00002D1": strErrorText = "STATUS_SYSTEM_IMAGE_BAD_SIGNATURE"; break;
                case "C00002D2": strErrorText = "STATUS_PNP_REBOOT_REQUIRED"; break;
                case "C00002D3": strErrorText = "STATUS_POWER_STATE_INVALID"; break;
                case "C00002D4": strErrorText = "STATUS_DS_INVALID_GROUP_TYPE"; break;
                case "C00002D5": strErrorText = "STATUS_DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN"; break;
                case "C00002D6": strErrorText = "STATUS_DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN"; break;
                case "C00002D7": strErrorText = "STATUS_DS_GLOBAL_CANT_HAVE_LOCAL_MEMBER"; break;
                case "C00002D8": strErrorText = "STATUS_DS_GLOBAL_CANT_HAVE_UNIVERSAL_MEMBER"; break;
                case "C00002D9": strErrorText = "STATUS_DS_UNIVERSAL_CANT_HAVE_LOCAL_MEMBER"; break;
                case "C00002DA": strErrorText = "STATUS_DS_GLOBAL_CANT_HAVE_CROSSDOMAIN_MEMBER"; break;
                case "C00002DB": strErrorText = "STATUS_DS_LOCAL_CANT_HAVE_CROSSDOMAIN_LOCAL_MEMBER"; break;
                case "C00002DC": strErrorText = "STATUS_DS_HAVE_PRIMARY_MEMBERS"; break;
                case "C00002DD": strErrorText = "STATUS_WMI_NOT_SUPPORTED"; break;
                case "C00002DE": strErrorText = "STATUS_INSUFFICIENT_POWER"; break;
                case "C00002DF": strErrorText = "STATUS_SAM_NEED_BOOTKEY_PASSWORD"; break;
                case "C00002E0": strErrorText = "STATUS_SAM_NEED_BOOTKEY_FLOPPY"; break;
                case "C00002E1": strErrorText = "STATUS_DS_CANT_START"; break;
                case "C00002E2": strErrorText = "STATUS_DS_INIT_FAILURE"; break;
                case "C00002E3": strErrorText = "STATUS_SAM_INIT_FAILURE"; break;
                case "C00002E4": strErrorText = "STATUS_DS_GC_REQUIRED"; break;
                case "C00002E5": strErrorText = "STATUS_DS_LOCAL_MEMBER_OF_LOCAL_ONLY"; break;
                case "C00002E6": strErrorText = "STATUS_DS_NO_FPO_IN_UNIVERSAL_GROUPS"; break;
                case "C00002E7": strErrorText = "STATUS_DS_MACHINE_ACCOUNT_QUOTA_EXCEEDED"; break;
                case "C00002E8": strErrorText = "STATUS_MULTIPLE_FAULT_VIOLATION"; break;
                case "C00002E9": strErrorText = "STATUS_CURRENT_DOMAIN_NOT_ALLOWED"; break;
                case "C00002EA": strErrorText = "STATUS_CANNOT_MAKE"; break;
                case "C00002EB": strErrorText = "STATUS_SYSTEM_SHUTDOWN"; break;
                case "C00002EC": strErrorText = "STATUS_DS_INIT_FAILURE_CONSOLE"; break;
                case "C00002ED": strErrorText = "STATUS_DS_SAM_INIT_FAILURE_CONSOLE"; break;
                case "C00002EE": strErrorText = "STATUS_UNFINISHED_CONTEXT_DELETED"; break;
                case "C00002EF": strErrorText = "STATUS_NO_TGT_REPLY"; break;
                case "C00002F0": strErrorText = "STATUS_OBJECTID_NOT_FOUND"; break;
                case "C00002F1": strErrorText = "STATUS_NO_IP_ADDRESSES"; break;
                case "C00002F2": strErrorText = "STATUS_WRONG_CREDENTIAL_HANDLE"; break;
                case "C00002F3": strErrorText = "STATUS_CRYPTO_SYSTEM_INVALID"; break;
                case "C00002F4": strErrorText = "STATUS_MAX_REFERRALS_EXCEEDED"; break;
                case "C00002F5": strErrorText = "STATUS_MUST_BE_KDC"; break;
                case "C00002F6": strErrorText = "STATUS_STRONG_CRYPTO_NOT_SUPPORTED"; break;
                case "C00002F7": strErrorText = "STATUS_TOO_MANY_PRINCIPALS"; break;
                case "C00002F8": strErrorText = "STATUS_NO_PA_DATA"; break;
                case "C00002F9": strErrorText = "STATUS_PKINIT_NAME_MISMATCH"; break;
                case "C00002FA": strErrorText = "STATUS_SMARTCARD_LOGON_REQUIRED"; break;
                case "C00002FB": strErrorText = "STATUS_KDC_INVALID_REQUEST"; break;
                case "C00002FC": strErrorText = "STATUS_KDC_UNABLE_TO_REFER"; break;
                case "C00002FD": strErrorText = "STATUS_KDC_UNKNOWN_ETYPE"; break;
                case "C00002FE": strErrorText = "STATUS_SHUTDOWN_IN_PROGRESS"; break;
                case "C00002FF": strErrorText = "STATUS_SERVER_SHUTDOWN_IN_PROGRESS"; break;
                case "C0000300": strErrorText = "STATUS_NOT_SUPPORTED_ON_SBS"; break;
                case "C0000301": strErrorText = "STATUS_WMI_GUID_DISCONNECTED"; break;
                case "C0000302": strErrorText = "STATUS_WMI_ALREADY_DISABLED"; break;
                case "C0000303": strErrorText = "STATUS_WMI_ALREADY_ENABLED"; break;
                case "C0000304": strErrorText = "STATUS_MFT_TOO_FRAGMENTED"; break;
                case "C0000305": strErrorText = "STATUS_COPY_PROTECTION_FAILURE"; break;
                case "C0000306": strErrorText = "STATUS_CSS_AUTHENTICATION_FAILURE"; break;
                case "C0000307": strErrorText = "STATUS_CSS_KEY_NOT_PRESENT"; break;
                case "C0000308": strErrorText = "STATUS_CSS_KEY_NOT_ESTABLISHED"; break;
                case "C0000309": strErrorText = "STATUS_CSS_SCRAMBLED_SECTOR"; break;
                case "C000030A": strErrorText = "STATUS_CSS_REGION_MISMATCH"; break;
                case "C000030B": strErrorText = "STATUS_CSS_RESETS_EXHAUSTED"; break;
                case "C0000320": strErrorText = "STATUS_PKINIT_FAILURE"; break;
                case "C0000321": strErrorText = "STATUS_SMARTCARD_SUBSYSTEM_FAILURE"; break;
                case "C0000322": strErrorText = "STATUS_NO_KERB_KEY"; break;
                case "C0000350": strErrorText = "STATUS_HOST_DOWN"; break;
                case "C0000351": strErrorText = "STATUS_UNSUPPORTED_PREAUTH"; break;
                case "C0000352": strErrorText = "STATUS_EFS_ALG_BLOB_TOO_BIG"; break;
                case "C0000353": strErrorText = "STATUS_PORT_NOT_SET"; break;
                case "C0000354": strErrorText = "STATUS_DEBUGGER_INACTIVE"; break;
                case "C0000355": strErrorText = "STATUS_DS_VERSION_CHECK_FAILURE"; break;
                case "C0000356": strErrorText = "STATUS_AUDITING_DISABLED"; break;
                case "C0000357": strErrorText = "STATUS_PRENT4_MACHINE_ACCOUNT"; break;
                case "C0000358": strErrorText = "STATUS_DS_AG_CANT_HAVE_UNIVERSAL_MEMBER"; break;
                case "C0000359": strErrorText = "STATUS_INVALID_IMAGE_WIN_32"; break;
                case "C000035A": strErrorText = "STATUS_INVALID_IMAGE_WIN_64"; break;
                case "C000035B": strErrorText = "STATUS_BAD_BINDINGS"; break;
                case "C000035C": strErrorText = "STATUS_NETWORK_SESSION_EXPIRED"; break;
                case "C000035D": strErrorText = "STATUS_APPHELP_BLOCK"; break;
                case "C000035E": strErrorText = "STATUS_ALL_SIDS_FILTERED"; break;
                case "C000035F": strErrorText = "STATUS_NOT_SAFE_MODE_DRIVER"; break;
                case "C0000361": strErrorText = "STATUS_ACCESS_DISABLED_BY_POLICY_DEFAULT"; break;
                case "C0000362": strErrorText = "STATUS_ACCESS_DISABLED_BY_POLICY_PATH"; break;
                case "C0000363": strErrorText = "STATUS_ACCESS_DISABLED_BY_POLICY_PUBLISHER"; break;
                case "C0000364": strErrorText = "STATUS_ACCESS_DISABLED_BY_POLICY_OTHER"; break;
                case "C0000365": strErrorText = "STATUS_FAILED_DRIVER_ENTRY"; break;
                case "C0000366": strErrorText = "STATUS_DEVICE_ENUMERATION_ERROR"; break;
                case "00000367": strErrorText = "STATUS_WAIT_FOR_OPLOCK"; break;
                case "C0000368": strErrorText = "STATUS_MOUNT_POINT_NOT_RESOLVED"; break;
                case "C0000369": strErrorText = "STATUS_INVALID_DEVICE_OBJECT_PARAMETER"; break;
                case "C000036A": strErrorText = "STATUS_MCA_OCCURED"; break;
                case "C000036B": strErrorText = "STATUS_DRIVER_BLOCKED_CRITICAL"; break;
                case "C000036C": strErrorText = "STATUS_DRIVER_BLOCKED"; break;
                case "C000036D": strErrorText = "STATUS_DRIVER_DATABASE_ERROR"; break;
                case "C000036E": strErrorText = "STATUS_SYSTEM_HIVE_TOO_LARGE"; break;
                case "C000036F": strErrorText = "STATUS_INVALID_IMPORT_OF_NON_DLL"; break;
                case "40000370": strErrorText = "STATUS_DS_SHUTTING_DOWN"; break;
                case "C0000380": strErrorText = "STATUS_SMARTCARD_WRONG_PIN"; break;
                case "C0000381": strErrorText = "STATUS_SMARTCARD_CARD_BLOCKED"; break;
                case "C0000382": strErrorText = "STATUS_SMARTCARD_CARD_NOT_AUTHENTICATED"; break;
                case "C0000383": strErrorText = "STATUS_SMARTCARD_NO_CARD"; break;
                case "C0000384": strErrorText = "STATUS_SMARTCARD_NO_KEY_CONTAINER"; break;
                case "C0000385": strErrorText = "STATUS_SMARTCARD_NO_CERTIFICATE"; break;
                case "C0000386": strErrorText = "STATUS_SMARTCARD_NO_KEYSET"; break;
                case "C0000387": strErrorText = "STATUS_SMARTCARD_IO_ERROR"; break;
                case "C0000388": strErrorText = "STATUS_DOWNGRADE_DETECTED"; break;
                case "C0000389": strErrorText = "STATUS_SMARTCARD_CERT_REVOKED"; break;
                case "C000038A": strErrorText = "STATUS_ISSUING_CA_UNTRUSTED"; break;
                case "C000038B": strErrorText = "STATUS_REVOCATION_OFFLINE_C"; break;
                case "C000038C": strErrorText = "STATUS_PKINIT_CLIENT_FAILURE"; break;
                case "C000038D": strErrorText = "STATUS_SMARTCARD_CERT_EXPIRED"; break;
                case "C000038E": strErrorText = "STATUS_DRIVER_FAILED_PRIOR_UNLOAD"; break;
                case "C000038F": strErrorText = "STATUS_SMARTCARD_SILENT_CONTEXT"; break;
                case "C0000401": strErrorText = "STATUS_PER_USER_TRUST_QUOTA_EXCEEDED"; break;
                case "C0000402": strErrorText = "STATUS_ALL_USER_TRUST_QUOTA_EXCEEDED"; break;
                case "C0000403": strErrorText = "STATUS_USER_DELETE_TRUST_QUOTA_EXCEEDED"; break;
                case "C0000404": strErrorText = "STATUS_DS_NAME_NOT_UNIQUE"; break;
                case "C0000405": strErrorText = "STATUS_DS_DUPLICATE_ID_FOUND"; break;
                case "C0000406": strErrorText = "STATUS_DS_GROUP_CONVERSION_ERROR"; break;
                case "C0000407": strErrorText = "STATUS_VOLSNAP_PREPARE_HIBERNATE"; break;
                case "C0000408": strErrorText = "STATUS_USER2USER_REQUIRED"; break;
                case "C0000409": strErrorText = "STATUS_STACK_BUFFER_OVERRUN"; break;
                case "C000040A": strErrorText = "STATUS_NO_S4U_PROT_SUPPORT"; break;
                case "C000040B": strErrorText = "STATUS_CROSSREALM_DELEGATION_FAILURE"; break;
                case "C000040C": strErrorText = "STATUS_REVOCATION_OFFLINE_KDC"; break;
                case "C000040D": strErrorText = "STATUS_ISSUING_CA_UNTRUSTED_KDC"; break;
                case "C000040E": strErrorText = "STATUS_KDC_CERT_EXPIRED"; break;
                case "C000040F": strErrorText = "STATUS_KDC_CERT_REVOKED"; break;
                case "C0000410": strErrorText = "STATUS_PARAMETER_QUOTA_EXCEEDED"; break;
                case "C0000411": strErrorText = "STATUS_HIBERNATION_FAILURE"; break;
                case "C0000412": strErrorText = "STATUS_DELAY_LOAD_FAILED"; break;
                case "C0000413": strErrorText = "STATUS_AUTHENTICATION_FIREWALL_FAILED"; break;
                case "C0000414": strErrorText = "STATUS_VDM_DISALLOWED"; break;
                case "C0000415": strErrorText = "STATUS_HUNG_DISPLAY_DRIVER_THREAD"; break;
                case "C0009898": strErrorText = "STATUS_WOW_ASSERTION"; break;
                case "C0010001": strErrorText = "DBG_NO_STATE_CHANGE"; break;
                case "C0010002": strErrorText = "DBG_APP_NOT_IDLE"; break;
                case "C0020001": strErrorText = "RPC_NT_INVALID_STRING_BINDING"; break;
                case "C0020002": strErrorText = "RPC_NT_WRONG_KIND_OF_BINDING"; break;
                case "C0020003": strErrorText = "RPC_NT_INVALID_BINDING"; break;
                case "C0020004": strErrorText = "RPC_NT_PROTSEQ_NOT_SUPPORTED"; break;
                case "C0020005": strErrorText = "RPC_NT_INVALID_RPC_PROTSEQ"; break;
                case "C0020006": strErrorText = "RPC_NT_INVALID_STRING_UUID"; break;
                case "C0020007": strErrorText = "RPC_NT_INVALID_ENDPOINT_FORMAT"; break;
                case "C0020008": strErrorText = "RPC_NT_INVALID_NET_ADDR"; break;
                case "C0020009": strErrorText = "RPC_NT_NO_ENDPOINT_FOUND"; break;
                case "C002000A": strErrorText = "RPC_NT_INVALID_TIMEOUT"; break;
                case "C002000B": strErrorText = "RPC_NT_OBJECT_NOT_FOUND"; break;
                case "C002000C": strErrorText = "RPC_NT_ALREADY_REGISTERED"; break;
                case "C002000D": strErrorText = "RPC_NT_TYPE_ALREADY_REGISTERED"; break;
                case "C002000E": strErrorText = "RPC_NT_ALREADY_LISTENING"; break;
                case "C002000F": strErrorText = "RPC_NT_NO_PROTSEQS_REGISTERED"; break;
                case "C0020010": strErrorText = "RPC_NT_NOT_LISTENING"; break;
                case "C0020011": strErrorText = "RPC_NT_UNKNOWN_MGR_TYPE"; break;
                case "C0020012": strErrorText = "RPC_NT_UNKNOWN_IF"; break;
                case "C0020013": strErrorText = "RPC_NT_NO_BINDINGS"; break;
                case "C0020014": strErrorText = "RPC_NT_NO_PROTSEQS"; break;
                case "C0020015": strErrorText = "RPC_NT_CANT_CREATE_ENDPOINT"; break;
                case "C0020016": strErrorText = "RPC_NT_OUT_OF_RESOURCES"; break;
                case "C0020017": strErrorText = "RPC_NT_SERVER_UNAVAILABLE"; break;
                case "C0020018": strErrorText = "RPC_NT_SERVER_TOO_BUSY"; break;
                case "C0020019": strErrorText = "RPC_NT_INVALID_NETWORK_OPTIONS"; break;
                case "C002001A": strErrorText = "RPC_NT_NO_CALL_ACTIVE"; break;
                case "C002001B": strErrorText = "RPC_NT_CALL_FAILED"; break;
                case "C002001C": strErrorText = "RPC_NT_CALL_FAILED_DNE"; break;
                case "C002001D": strErrorText = "RPC_NT_PROTOCOL_ERROR"; break;
                case "C002001F": strErrorText = "RPC_NT_UNSUPPORTED_TRANS_SYN"; break;
                case "C0020021": strErrorText = "RPC_NT_UNSUPPORTED_TYPE"; break;
                case "C0020022": strErrorText = "RPC_NT_INVALID_TAG"; break;
                case "C0020023": strErrorText = "RPC_NT_INVALID_BOUND"; break;
                case "C0020024": strErrorText = "RPC_NT_NO_ENTRY_NAME"; break;
                case "C0020025": strErrorText = "RPC_NT_INVALID_NAME_SYNTAX"; break;
                case "C0020026": strErrorText = "RPC_NT_UNSUPPORTED_NAME_SYNTAX"; break;
                case "C0020028": strErrorText = "RPC_NT_UUID_NO_ADDRESS"; break;
                case "C0020029": strErrorText = "RPC_NT_DUPLICATE_ENDPOINT"; break;
                case "C002002A": strErrorText = "RPC_NT_UNKNOWN_AUTHN_TYPE"; break;
                case "C002002B": strErrorText = "RPC_NT_MAX_CALLS_TOO_SMALL"; break;
                case "C002002C": strErrorText = "RPC_NT_STRING_TOO_LONG"; break;
                case "C002002D": strErrorText = "RPC_NT_PROTSEQ_NOT_FOUND"; break;
                case "C002002E": strErrorText = "RPC_NT_PROCNUM_OUT_OF_RANGE"; break;
                case "C002002F": strErrorText = "RPC_NT_BINDING_HAS_NO_AUTH"; break;
                case "C0020030": strErrorText = "RPC_NT_UNKNOWN_AUTHN_SERVICE"; break;
                case "C0020031": strErrorText = "RPC_NT_UNKNOWN_AUTHN_LEVEL"; break;
                case "C0020032": strErrorText = "RPC_NT_INVALID_AUTH_IDENTITY"; break;
                case "C0020033": strErrorText = "RPC_NT_UNKNOWN_AUTHZ_SERVICE"; break;
                case "C0020034": strErrorText = "EPT_NT_INVALID_ENTRY"; break;
                case "C0020035": strErrorText = "EPT_NT_CANT_PERFORM_OP"; break;
                case "C0020036": strErrorText = "EPT_NT_NOT_REGISTERED"; break;
                case "C0020037": strErrorText = "RPC_NT_NOTHING_TO_EXPORT"; break;
                case "C0020038": strErrorText = "RPC_NT_INCOMPLETE_NAME"; break;
                case "C0020039": strErrorText = "RPC_NT_INVALID_VERS_OPTION"; break;
                case "C002003A": strErrorText = "RPC_NT_NO_MORE_MEMBERS"; break;
                case "C002003B": strErrorText = "RPC_NT_NOT_ALL_OBJS_UNEXPORTED"; break;
                case "C002003C": strErrorText = "RPC_NT_INTERFACE_NOT_FOUND"; break;
                case "C002003D": strErrorText = "RPC_NT_ENTRY_ALREADY_EXISTS"; break;
                case "C002003E": strErrorText = "RPC_NT_ENTRY_NOT_FOUND"; break;
                case "C002003F": strErrorText = "RPC_NT_NAME_SERVICE_UNAVAILABLE"; break;
                case "C0020040": strErrorText = "RPC_NT_INVALID_NAF_ID"; break;
                case "C0020041": strErrorText = "RPC_NT_CANNOT_SUPPORT"; break;
                case "C0020042": strErrorText = "RPC_NT_NO_CONTEXT_AVAILABLE"; break;
                case "C0020043": strErrorText = "RPC_NT_INTERNAL_ERROR"; break;
                case "C0020044": strErrorText = "RPC_NT_ZERO_DIVIDE"; break;
                case "C0020045": strErrorText = "RPC_NT_ADDRESS_ERROR"; break;
                case "C0020046": strErrorText = "RPC_NT_FP_DIV_ZERO"; break;
                case "C0020047": strErrorText = "RPC_NT_FP_UNDERFLOW"; break;
                case "C0020048": strErrorText = "RPC_NT_FP_OVERFLOW"; break;
                case "C0030001": strErrorText = "RPC_NT_NO_MORE_ENTRIES"; break;
                case "C0030002": strErrorText = "RPC_NT_SS_CHAR_TRANS_OPEN_FAIL"; break;
                case "C0030003": strErrorText = "RPC_NT_SS_CHAR_TRANS_SHORT_FILE"; break;
                case "C0030004": strErrorText = "RPC_NT_SS_IN_NULL_CONTEXT"; break;
                case "C0030005": strErrorText = "RPC_NT_SS_CONTEXT_MISMATCH"; break;
                case "C0030006": strErrorText = "RPC_NT_SS_CONTEXT_DAMAGED"; break;
                case "C0030007": strErrorText = "RPC_NT_SS_HANDLES_MISMATCH"; break;
                case "C0030008": strErrorText = "RPC_NT_SS_CANNOT_GET_CALL_HANDLE"; break;
                case "C0030009": strErrorText = "RPC_NT_NULL_REF_POINTER"; break;
                case "C003000A": strErrorText = "RPC_NT_ENUM_VALUE_OUT_OF_RANGE"; break;
                case "C003000B": strErrorText = "RPC_NT_BYTE_COUNT_TOO_SMALL"; break;
                case "C003000C": strErrorText = "RPC_NT_BAD_STUB_DATA"; break;
                case "C0020049": strErrorText = "RPC_NT_CALL_IN_PROGRESS"; break;
                case "C002004A": strErrorText = "RPC_NT_NO_MORE_BINDINGS"; break;
                case "C002004B": strErrorText = "RPC_NT_GROUP_MEMBER_NOT_FOUND"; break;
                case "C002004C": strErrorText = "EPT_NT_CANT_CREATE"; break;
                case "C002004D": strErrorText = "RPC_NT_INVALID_OBJECT"; break;
                case "C002004F": strErrorText = "RPC_NT_NO_INTERFACES"; break;
                case "C0020050": strErrorText = "RPC_NT_CALL_CANCELLED"; break;
                case "C0020051": strErrorText = "RPC_NT_BINDING_INCOMPLETE"; break;
                case "C0020052": strErrorText = "RPC_NT_COMM_FAILURE"; break;
                case "C0020053": strErrorText = "RPC_NT_UNSUPPORTED_AUTHN_LEVEL"; break;
                case "C0020054": strErrorText = "RPC_NT_NO_PRINC_NAME"; break;
                case "C0020055": strErrorText = "RPC_NT_NOT_RPC_ERROR"; break;
                case "40020056": strErrorText = "RPC_NT_UUID_LOCAL_ONLY"; break;
                case "C0020057": strErrorText = "RPC_NT_SEC_PKG_ERROR"; break;
                case "C0020058": strErrorText = "RPC_NT_NOT_CANCELLED"; break;
                case "C0030059": strErrorText = "RPC_NT_INVALID_ES_ACTION"; break;
                case "C003005A": strErrorText = "RPC_NT_WRONG_ES_VERSION"; break;
                case "C003005B": strErrorText = "RPC_NT_WRONG_STUB_VERSION"; break;
                case "C003005C": strErrorText = "RPC_NT_INVALID_PIPE_OBJECT"; break;
                case "C003005D": strErrorText = "RPC_NT_INVALID_PIPE_OPERATION"; break;
                case "C003005E": strErrorText = "RPC_NT_WRONG_PIPE_VERSION"; break;
                case "C003005F": strErrorText = "RPC_NT_PIPE_CLOSED"; break;
                case "C0030060": strErrorText = "RPC_NT_PIPE_DISCIPLINE_ERROR"; break;
                case "C0030061": strErrorText = "RPC_NT_PIPE_EMPTY"; break;
                case "C0020062": strErrorText = "RPC_NT_INVALID_ASYNC_HANDLE"; break;
                case "C0020063": strErrorText = "RPC_NT_INVALID_ASYNC_CALL"; break;
                case "400200AF": strErrorText = "RPC_NT_SEND_INCOMPLETE"; break;
                case "C0140001": strErrorText = "STATUS_ACPI_INVALID_OPCODE"; break;
                case "C0140002": strErrorText = "STATUS_ACPI_STACK_OVERFLOW"; break;
                case "C0140003": strErrorText = "STATUS_ACPI_ASSERT_FAILED"; break;
                case "C0140004": strErrorText = "STATUS_ACPI_INVALID_INDEX"; break;
                case "C0140005": strErrorText = "STATUS_ACPI_INVALID_ARGUMENT"; break;
                case "C0140006": strErrorText = "STATUS_ACPI_FATAL"; break;
                case "C0140007": strErrorText = "STATUS_ACPI_INVALID_SUPERNAME"; break;
                case "C0140008": strErrorText = "STATUS_ACPI_INVALID_ARGTYPE"; break;
                case "C0140009": strErrorText = "STATUS_ACPI_INVALID_OBJTYPE"; break;
                case "C014000A": strErrorText = "STATUS_ACPI_INVALID_TARGETTYPE"; break;
                case "C014000B": strErrorText = "STATUS_ACPI_INCORRECT_ARGUMENT_COUNT"; break;
                case "C014000C": strErrorText = "STATUS_ACPI_ADDRESS_NOT_MAPPED"; break;
                case "C014000D": strErrorText = "STATUS_ACPI_INVALID_EVENTTYPE"; break;
                case "C014000E": strErrorText = "STATUS_ACPI_HANDLER_COLLISION"; break;
                case "C014000F": strErrorText = "STATUS_ACPI_INVALID_DATA"; break;
                case "C0140010": strErrorText = "STATUS_ACPI_INVALID_REGION"; break;
                case "C0140011": strErrorText = "STATUS_ACPI_INVALID_ACCESS_SIZE"; break;
                case "C0140012": strErrorText = "STATUS_ACPI_ACQUIRE_GLOBAL_LOCK"; break;
                case "C0140013": strErrorText = "STATUS_ACPI_ALREADY_INITIALIZED"; break;
                case "C0140014": strErrorText = "STATUS_ACPI_NOT_INITIALIZED"; break;
                case "C0140015": strErrorText = "STATUS_ACPI_INVALID_MUTEX_LEVEL"; break;
                case "C0140016": strErrorText = "STATUS_ACPI_MUTEX_NOT_OWNED"; break;
                case "C0140017": strErrorText = "STATUS_ACPI_MUTEX_NOT_OWNER"; break;
                case "C0140018": strErrorText = "STATUS_ACPI_RS_ACCESS"; break;
                case "C0140019": strErrorText = "STATUS_ACPI_INVALID_TABLE"; break;
                case "C0140020": strErrorText = "STATUS_ACPI_REG_HANDLER_FAILED"; break;
                case "C0140021": strErrorText = "STATUS_ACPI_POWER_REQUEST_FAILED"; break;
                case "C00A0001": strErrorText = "STATUS_CTX_WINSTATION_NAME_INVALID"; break;
                case "C00A0002": strErrorText = "STATUS_CTX_INVALID_PD"; break;
                case "C00A0003": strErrorText = "STATUS_CTX_PD_NOT_FOUND"; break;
                case "400A0004": strErrorText = "STATUS_CTX_CDM_CONNECT"; break;
                case "400A0005": strErrorText = "STATUS_CTX_CDM_DISCONNECT"; break;
                case "C00A0006": strErrorText = "STATUS_CTX_CLOSE_PENDING"; break;
                case "C00A0007": strErrorText = "STATUS_CTX_NO_OUTBUF"; break;
                case "C00A0008": strErrorText = "STATUS_CTX_MODEM_INF_NOT_FOUND"; break;
                case "C00A0009": strErrorText = "STATUS_CTX_INVALID_MODEMNAME"; break;
                case "C00A000A": strErrorText = "STATUS_CTX_RESPONSE_ERROR"; break;
                case "C00A000B": strErrorText = "STATUS_CTX_MODEM_RESPONSE_TIMEOUT"; break;
                case "C00A000C": strErrorText = "STATUS_CTX_MODEM_RESPONSE_NO_CARRIER"; break;
                case "C00A000D": strErrorText = "STATUS_CTX_MODEM_RESPONSE_NO_DIALTONE"; break;
                case "C00A000E": strErrorText = "STATUS_CTX_MODEM_RESPONSE_BUSY"; break;
                case "C00A000F": strErrorText = "STATUS_CTX_MODEM_RESPONSE_VOICE"; break;
                case "C00A0010": strErrorText = "STATUS_CTX_TD_ERROR"; break;
                case "C00A0012": strErrorText = "STATUS_CTX_LICENSE_CLIENT_INVALID"; break;
                case "C00A0013": strErrorText = "STATUS_CTX_LICENSE_NOT_AVAILABLE"; break;
                case "C00A0014": strErrorText = "STATUS_CTX_LICENSE_EXPIRED"; break;
                case "C00A0015": strErrorText = "STATUS_CTX_WINSTATION_NOT_FOUND"; break;
                case "C00A0016": strErrorText = "STATUS_CTX_WINSTATION_NAME_COLLISION"; break;
                case "C00A0017": strErrorText = "STATUS_CTX_WINSTATION_BUSY"; break;
                case "C00A0018": strErrorText = "STATUS_CTX_BAD_VIDEO_MODE"; break;
                case "C00A0022": strErrorText = "STATUS_CTX_GRAPHICS_INVALID"; break;
                case "C00A0024": strErrorText = "STATUS_CTX_NOT_CONSOLE"; break;
                case "C00A0026": strErrorText = "STATUS_CTX_CLIENT_QUERY_TIMEOUT"; break;
                case "C00A0027": strErrorText = "STATUS_CTX_CONSOLE_DISCONNECT"; break;
                case "C00A0028": strErrorText = "STATUS_CTX_CONSOLE_CONNECT"; break;
                case "C00A002A": strErrorText = "STATUS_CTX_SHADOW_DENIED"; break;
                case "C00A002B": strErrorText = "STATUS_CTX_WINSTATION_ACCESS_DENIED"; break;
                case "C00A002E": strErrorText = "STATUS_CTX_INVALID_WD"; break;
                case "C00A002F": strErrorText = "STATUS_CTX_WD_NOT_FOUND"; break;
                case "C00A0030": strErrorText = "STATUS_CTX_SHADOW_INVALID"; break;
                case "C00A0031": strErrorText = "STATUS_CTX_SHADOW_DISABLED"; break;
                case "C00A0032": strErrorText = "STATUS_RDP_PROTOCOL_ERROR"; break;
                case "C00A0033": strErrorText = "STATUS_CTX_CLIENT_LICENSE_NOT_SET"; break;
                case "C00A0034": strErrorText = "STATUS_CTX_CLIENT_LICENSE_IN_USE"; break;
                case "C00A0035": strErrorText = "STATUS_CTX_SHADOW_ENDED_BY_MODE_CHANGE"; break;
                case "C00A0036": strErrorText = "STATUS_CTX_SHADOW_NOT_RUNNING"; break;
                case "C0040035": strErrorText = "STATUS_PNP_BAD_MPS_TABLE"; break;
                case "C0040036": strErrorText = "STATUS_PNP_TRANSLATION_FAILED"; break;
                case "C0040037": strErrorText = "STATUS_PNP_IRQ_TRANSLATION_FAILED"; break;
                case "C0040038": strErrorText = "STATUS_PNP_INVALID_ID"; break;
                case "C0150001": strErrorText = "STATUS_SXS_SECTION_NOT_FOUND"; break;
                case "C0150002": strErrorText = "STATUS_SXS_CANT_GEN_ACTCTX"; break;
                case "C0150003": strErrorText = "STATUS_SXS_INVALID_ACTCTXDATA_FORMAT"; break;
                case "C0150004": strErrorText = "STATUS_SXS_ASSEMBLY_NOT_FOUND"; break;
                case "C0150005": strErrorText = "STATUS_SXS_MANIFEST_FORMAT_ERROR"; break;
                case "C0150006": strErrorText = "STATUS_SXS_MANIFEST_PARSE_ERROR"; break;
                case "C0150007": strErrorText = "STATUS_SXS_ACTIVATION_CONTEXT_DISABLED"; break;
                case "C0150008": strErrorText = "STATUS_SXS_KEY_NOT_FOUND"; break;
                case "C0150009": strErrorText = "STATUS_SXS_VERSION_CONFLICT"; break;
                case "C015000A": strErrorText = "STATUS_SXS_WRONG_SECTION_TYPE"; break;
                case "C015000B": strErrorText = "STATUS_SXS_THREAD_QUERIES_DISABLED"; break;
                case "C015000C": strErrorText = "STATUS_SXS_ASSEMBLY_MISSING"; break;
                case "4015000D": strErrorText = "STATUS_SXS_RELEASE_ACTIVATION_CONTEXT"; break;
                case "C015000E": strErrorText = "STATUS_SXS_PROCESS_DEFAULT_ALREADY_SET"; break;
                case "C015000F": strErrorText = "STATUS_SXS_EARLY_DEACTIVATION"; break;
                case "C0150010": strErrorText = "STATUS_SXS_INVALID_DEACTIVATION"; break;
                case "C0150011": strErrorText = "STATUS_SXS_MULTIPLE_DEACTIVATION"; break;
                case "C0150012": strErrorText = "STATUS_SXS_SYSTEM_DEFAULT_ACTIVATION_CONTEXT_EMPTY"; break;
                case "C0150013": strErrorText = "STATUS_SXS_PROCESS_TERMINATION_REQUESTED"; break;
                case "C0150014": strErrorText = "STATUS_SXS_CORRUPT_ACTIVATION_STACK"; break;
                case "C0150015": strErrorText = "STATUS_SXS_CORRUPTION"; break;
                case "C0130001": strErrorText = "STATUS_CLUSTER_INVALID_NODE"; break;
                case "C0130002": strErrorText = "STATUS_CLUSTER_NODE_EXISTS"; break;
                case "C0130003": strErrorText = "STATUS_CLUSTER_JOIN_IN_PROGRESS"; break;
                case "C0130004": strErrorText = "STATUS_CLUSTER_NODE_NOT_FOUND"; break;
                case "C0130005": strErrorText = "STATUS_CLUSTER_LOCAL_NODE_NOT_FOUND"; break;
                case "C0130006": strErrorText = "STATUS_CLUSTER_NETWORK_EXISTS"; break;
                case "C0130007": strErrorText = "STATUS_CLUSTER_NETWORK_NOT_FOUND"; break;
                case "C0130008": strErrorText = "STATUS_CLUSTER_NETINTERFACE_EXISTS"; break;
                case "C0130009": strErrorText = "STATUS_CLUSTER_NETINTERFACE_NOT_FOUND"; break;
                case "C013000A": strErrorText = "STATUS_CLUSTER_INVALID_REQUEST"; break;
                case "C013000B": strErrorText = "STATUS_CLUSTER_INVALID_NETWORK_PROVIDER"; break;
                case "C013000C": strErrorText = "STATUS_CLUSTER_NODE_DOWN"; break;
                case "C013000D": strErrorText = "STATUS_CLUSTER_NODE_UNREACHABLE"; break;
                case "C013000E": strErrorText = "STATUS_CLUSTER_NODE_NOT_MEMBER"; break;
                case "C013000F": strErrorText = "STATUS_CLUSTER_JOIN_NOT_IN_PROGRESS"; break;
                case "C0130010": strErrorText = "STATUS_CLUSTER_INVALID_NETWORK"; break;
                case "C0130011": strErrorText = "STATUS_CLUSTER_NO_NET_ADAPTERS"; break;
                case "C0130012": strErrorText = "STATUS_CLUSTER_NODE_UP"; break;
                case "C0130013": strErrorText = "STATUS_CLUSTER_NODE_PAUSED"; break;
                case "C0130014": strErrorText = "STATUS_CLUSTER_NODE_NOT_PAUSED"; break;
                case "C0130015": strErrorText = "STATUS_CLUSTER_NO_SECURITY_CONTEXT"; break;
                case "C0130016": strErrorText = "STATUS_CLUSTER_NETWORK_NOT_INTERNAL"; break;
                case "C0130017": strErrorText = "STATUS_CLUSTER_POISONED"; break;
            }

            if (!String.IsNullOrEmpty(strErrorText))
                return "0x" + strErrorHex + " " + strErrorText + " (" + iErrorValue + ")";
            else
                return "0x" + strErrorHex + " (" + iErrorValue + ")";
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
    }

    internal class VistaStuff
    {
        /// <value>
        /// Returns true on Windows Vista or newer operating systems; otherwise, false.
        /// </value>
        [Browsable(false)]
        public static bool IsVistaOrNot
        {
            get {
                return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6;
            }
        }

        /// <value>
        /// Sets the memory and I/O priority on Windows Vista or newer operating systems
        /// </value>
        [Browsable(false)]
        public static void SetProcessPriority(IntPtr handle, ProcessPriorityClass priority)
        {
            if (IsVistaOrNot) {
                int prioIO = VistaStuff.PRIORITY_IO_NORMAL;
                int prioMemory = VistaStuff.PRIORITY_MEMORY_NORMAL;
                if (priority == ProcessPriorityClass.Idle || priority == ProcessPriorityClass.BelowNormal) {
                    prioIO = VistaStuff.PRIORITY_IO_LOW;
                    prioMemory = VistaStuff.PRIORITY_MEMORY_LOW;
                    SetPriorityClass(handle, PROCESS_MODE_BACKGROUND_BEGIN);
                } else
                    SetPriorityClass(handle, PROCESS_MODE_BACKGROUND_END);
                NtSetInformationProcess(handle, PROCESS_INFORMATION_IO_PRIORITY, ref prioIO, Marshal.SizeOf(prioIO));
                NtSetInformationProcess(handle, PROCESS_INFORMATION_MEMORY_PRIORITY, ref prioMemory, Marshal.SizeOf(prioMemory));
            }
        }

        /// <value>
        /// Sets the memory and I/O priority on Windows Vista or newer operating systems
        /// </value>
        [Browsable(false)]
        public static void SetThreadPriority(IntPtr handle, ThreadPriority priority)
        {
            if (IsVistaOrNot) {
                if (priority == ThreadPriority.Lowest || priority == ThreadPriority.BelowNormal)
                    SetThreadPriority(handle, THREAD_MODE_BACKGROUND_BEGIN);
                else
                    SetThreadPriority(handle, THREAD_MODE_BACKGROUND_END);
            }
        }

        public const int BS_COMMANDLINK = 0x0000000E;
        public const int BCM_SETNOTE = 0x00001609;
        public const int BCM_SETSHIELD = 0x0000160C;

        public const int TV_FIRST = 0x1100;
        public const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
        public const int TVM_GETEXTENDEDSTYLE = TV_FIRST + 45;
        public const int TVM_SETAUTOSCROLLINFO = TV_FIRST + 59;
        public const int TVS_NOHSCROLL = 0x8000;
        public const int TVS_EX_AUTOHSCROLL = 0x0020;
        public const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;
        public const int GWL_STYLE = -16;

        private const int PROCESS_INFORMATION_MEMORY_PRIORITY = 0x27;
        private const int PROCESS_INFORMATION_IO_PRIORITY = 0x21;
        private const int PRIORITY_MEMORY_NORMAL = 5;
        private const int PRIORITY_MEMORY_LOW = 3;
        private const int PRIORITY_IO_NORMAL = 2;
        private const int PRIORITY_IO_LOW = 1;
        private const uint PROCESS_MODE_BACKGROUND_BEGIN = 0x00100000;
        private const uint PROCESS_MODE_BACKGROUND_END = 0x00200000;
        private const int THREAD_MODE_BACKGROUND_BEGIN = 0x00010000;
        private const int THREAD_MODE_BACKGROUND_END = 0x00020000;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32", CharSet = CharSet.Unicode)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        [DllImport("ntdll", CharSet = CharSet.Unicode)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool SetPriorityClass(IntPtr handle, uint priorityClass);

        [DllImport("Kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentThread();

        [DllImport("Kernel32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetThreadPriority(IntPtr hThread, int nPriority);

        #region General Definitions

        // Various important window messages
        internal const int WM_USER = 0x0400;

        internal const int WM_ENTERIDLE = 0x0121;

        // FormatMessage constants and structs
        internal const int FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

        #endregion General Definitions

        #region File Operations Definitions

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        internal struct COMDLG_FILTERSPEC
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszName;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszSpec;
        }

        internal enum FDAP
        {
            FDAP_BOTTOM = 0x00000000,
            FDAP_TOP = 0x00000001,
        }

        internal enum FDE_SHAREVIOLATION_RESPONSE
        {
            FDESVR_DEFAULT = 0x00000000,
            FDESVR_ACCEPT = 0x00000001,
            FDESVR_REFUSE = 0x00000002
        }

        internal enum FDE_OVERWRITE_RESPONSE
        {
            FDEOR_DEFAULT = 0x00000000,
            FDEOR_ACCEPT = 0x00000001,
            FDEOR_REFUSE = 0x00000002
        }

        internal enum SIATTRIBFLAGS
        {
            SIATTRIBFLAGS_AND = 0x00000001, // if multiple items and the attirbutes together.
            SIATTRIBFLAGS_OR = 0x00000002, // if multiple items or the attributes together.
            SIATTRIBFLAGS_APPCOMPAT = 0x00000003, // Call GetAttributes directly on the ShellFolder for multiple attributes
        }

        internal enum SIGDN : uint
        {
            SIGDN_NORMALDISPLAY = 0x00000000,           // SHGDN_NORMAL
            SIGDN_PARENTRELATIVEPARSING = 0x80018001,   // SHGDN_INFOLDER | SHGDN_FORPARSING
            SIGDN_DESKTOPABSOLUTEPARSING = 0x80028000,  // SHGDN_FORPARSING
            SIGDN_PARENTRELATIVEEDITING = 0x80031001,   // SHGDN_INFOLDER | SHGDN_FOREDITING
            SIGDN_DESKTOPABSOLUTEEDITING = 0x8004c000,  // SHGDN_FORPARSING | SHGDN_FORADDRESSBAR
            SIGDN_FILESYSPATH = 0x80058000,             // SHGDN_FORPARSING
            SIGDN_URL = 0x80068000,                     // SHGDN_FORPARSING
            SIGDN_PARENTRELATIVEFORADDRESSBAR = 0x8007c001,     // SHGDN_INFOLDER | SHGDN_FORPARSING | SHGDN_FORADDRESSBAR
            SIGDN_PARENTRELATIVE = 0x80080001           // SHGDN_INFOLDER
        }

        [Flags]
        internal enum FOS : uint
        {
            FOS_OVERWRITEPROMPT = 0x00000002,
            FOS_STRICTFILETYPES = 0x00000004,
            FOS_NOCHANGEDIR = 0x00000008,
            FOS_PICKFOLDERS = 0x00000020,
            FOS_FORCEFILESYSTEM = 0x00000040, // Ensure that items returned are filesystem items.
            FOS_ALLNONSTORAGEITEMS = 0x00000080, // Allow choosing items that have no storage.
            FOS_NOVALIDATE = 0x00000100,
            FOS_ALLOWMULTISELECT = 0x00000200,
            FOS_PATHMUSTEXIST = 0x00000800,
            FOS_FILEMUSTEXIST = 0x00001000,
            FOS_CREATEPROMPT = 0x00002000,
            FOS_SHAREAWARE = 0x00004000,
            FOS_NOREADONLYRETURN = 0x00008000,
            FOS_NOTESTFILECREATE = 0x00010000,
            FOS_HIDEMRUPLACES = 0x00020000,
            FOS_HIDEPINNEDPLACES = 0x00040000,
            FOS_NODEREFERENCELINKS = 0x00100000,
            FOS_DONTADDTORECENT = 0x02000000,
            FOS_FORCESHOWHIDDEN = 0x10000000,
            FOS_DEFAULTNOMINIMODE = 0x20000000
        }

        internal enum CDCONTROLSTATE
        {
            CDCS_INACTIVE = 0x00000000,
            CDCS_ENABLED = 0x00000001,
            CDCS_VISIBLE = 0x00000002
        }

        #endregion File Operations Definitions

        #region KnownFolder Definitions

        internal enum FFFP_MODE
        {
            FFFP_EXACTMATCH,
            FFFP_NEARESTPARENTMATCH
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        internal struct KNOWNFOLDER_DEFINITION
        {
            internal VistaStuff.KF_CATEGORY category;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszName;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszCreator;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszDescription;

            internal Guid fidParent;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszRelativePath;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszParsingName;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszToolTip;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszLocalizedName;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszIcon;

            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pszSecurity;

            internal uint dwAttributes;
            internal VistaStuff.KF_DEFINITION_FLAGS kfdFlags;
            internal Guid ftidType;
        }

        internal enum KF_CATEGORY
        {
            KF_CATEGORY_VIRTUAL = 0x00000001,
            KF_CATEGORY_FIXED = 0x00000002,
            KF_CATEGORY_COMMON = 0x00000003,
            KF_CATEGORY_PERUSER = 0x00000004
        }

        [Flags]
        internal enum KF_DEFINITION_FLAGS
        {
            KFDF_PERSONALIZE = 0x00000001,
            KFDF_LOCAL_REDIRECT_ONLY = 0x00000002,
            KFDF_ROAMABLE = 0x00000004,
        }

        // Property System structs and consts
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        internal struct PROPERTYKEY
        {
            internal Guid fmtid;
            internal uint pid;
        }

        #endregion KnownFolder Definitions

        public const uint ERROR_CANCELLED = 0x800704C7;

        [Flags()]
        public enum FormatMessageFlags
        {
            FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,
            FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200,
            FORMAT_MESSAGE_FROM_STRING = 0x00000400,
            FORMAT_MESSAGE_FROM_HMODULE = 0x00000800,
            FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000,
            FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000
        }
    }
}
