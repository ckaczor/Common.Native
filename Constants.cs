using System;

namespace Common.Native
{
    public static class Constants
    {
        /// <summary>
        /// SetWindowPos flags
        /// </summary>
        [Flags]
        public enum WindowPositionFlags
        {
            AsyncWindowPosition = 0x4000,
            DeferErase = 0x2000,
            DrawFrame = 0x0020,
            FrameChanged = 0x0020,
            HideWindow = 0x0080,
            NoActivate = 0x0010,
            NoCopyBits = 0x0100,
            NoMove = 0x0002,
            NoOwnerZorder = 0x0200,
            NoRedraw = 0x0008,
            NoReposition = 0x0200,
            NoSendChanging = 0x0400,
            NoSize = 0x0001,
            NoZorder = 0x0004,
            ShowWindow = 0x0040,
        }

        /// <summary>
        /// WM_SYSCOMMAND resize direction
        /// </summary>
        public enum ResizeDirection
        {
            Left = 61441,
            Right = 61442,
            Top = 61443,
            TopLeft = 61444,
            TopRight = 61445,
            Bottom = 61446,
            BottomLeft = 61447,
            BottomRight = 61448,
        }

        /// <summary>
        /// Window messages
        /// </summary>
        public enum WindowMessage
        {
            Create = 0x0001,
            Destroy = 0x0002,
            Move = 0x0003,
            Moving = 0x0216,
            Size = 0x0005,
            Activate = 0x0006,
            SetFocus = 0x0007,
            KillFocus = 0x0008,
            Enable = 0x000a,
            SetRedraw = 0x000b,
            SetText = 0x000c,
            GetText = 0x000d,
            GetTextLength = 0x000e,
            Paint = 0x000f,
            Close = 0x0010,
            QueryEndSession = 0x0011,
            Quit = 0x0012,
            QueryOpen = 0x0013,
            EraseBackground = 0x0014,
            SystemColorChange = 0x0015,

            WindowPositionChanging = 0x0046,
            WindowPositionChanged = 0x0047,

            QueryDragIcon = 0x0037,
            GetIcon = 0x007F,
            SetIcon = 0x0080,
            CreateNonClientArea = 0x0081,
            DestroyNonClientArea = 0x0082,
            CalculateNonClientSize = 0x0083,
            HitTestNonClientArea = 0x0084,
            PaintNonClientArea = 0x0085,
            ActivateNonClientArea = 0x0086,
            GetDialogCode = 0x0087,
            SyncPaint = 0x0088,
            NonClientMouseMove = 0x00a0,
            NonClientLeftButtonDown = 0x00a1,
            NonClientLeftButtonUp = 0x00a2,
            NonClientLeftButtonDoubleClick = 0x00a3,
            NonClientRightButtonDown = 0x00a4,
            NonClientRightButtonUp = 0x00a5,
            NonClientRightButtonDoubleClick = 0x00a6,
            NonClientMiddleButtonDown = 0x00a7,
            NonClientMiddleButtonUp = 0x00a8,
            NonClientMiddleButtonDoubleClick = 0x00a9,

            SysKeyDown = 0x0104,
            SysKeyUp = 0x0105,
            SysChar = 0x0106,
            SysDeadChar = 0x0107,
            SysCommand = 0x0112,

            Hotkey = 0x312,

            DwmCompositionChanged = 0x031e,
            User = 0x0400,
            App = 0x8000,

            EnterSizeMove = 0x0231,
            ExitSizeMove = 0x0232
        }

        public enum HitTestValues
        {
            Error = -2,
            Transparent = -1,
            Nowhere = 0,
            Client = 1,
            Caption = 2,
            SystemMenu = 3,
            GrowBox = 4,
            Menu = 5,
            HorizontalScroll = 6,
            VerticalScroll = 7,
            MinimizeButton = 8,
            MaximizeButton = 9,
            Left = 10,
            Right = 11,
            Top = 12,
            TopLeft = 13,
            TopRight = 14,
            Bottom = 15,
            BottomLeft = 16,
            BottomRight = 17,
            Border = 18,
            Object = 19,
            Close = 20,
            Help = 21
        }

        [Flags]
        public enum ExtendedWindowStyles
        {
            ToolWindow = 0x00000080,
        }

        public enum GetWindowLongFields
        {
            ExStyle = -20,
        }

        [Flags]
        public enum ProcessAccess : uint
        {
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            SetSessionId = 0x00000004,
            VmOperation = 0x00000008,
            VmRead = 0x00000010,
            VmWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x00000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            StandardRightsRequired = 0x000F0000,
            Synchronize = 0x00100000,

            AllAccess =
                Terminate | CreateThread | SetSessionId | VmOperation | VmRead | VmWrite | DuplicateHandle |
                CreateProcess | SetQuota | SetInformation | QueryInformation | StandardRightsRequired | Synchronize
        }

        [Flags]
        public enum SnapshotFlags : uint
        {
            HeapList = 0x00000001,
            Process = 0x00000002,
            Thread = 0x00000004,
            Module = 0x00000008,
            Module32 = 0x00000010,
            All = (HeapList | Process | Thread | Module),
            Inherit = 0x80000000,
        }

        public enum ShowWindowCommand : uint
        {
            Hide = 0,
            ShowNormal = 1,
            Normal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            Maximize = 3,
            ShowNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActive = 7,
            ShowNa = 8,
            Restore = 9
        }

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            Normal = 0x0,
            Block = 0x1,
            AbortIfHung = 0x2,
            NoTimeoutIfNotHung = 0x8
        }

        public const int ICON_BIG = 1;
        public const int ICON_SMALL = 0;

        public enum ClassLongFlags : int
        {
            GCLP_MENUNAME = -8,
            GCLP_HBRBACKGROUND = -10,
            GCLP_HCURSOR = -12,
            GCLP_HICON = -14,
            GCLP_HMODULE = -16,
            GCL_CBWNDEXTRA = -18,
            GCL_CBCLSEXTRA = -20,
            GCLP_WNDPROC = -24,
            GCL_STYLE = -26,
            GCLP_HICONSM = -34,
            GCW_ATOM = -32
        }

        public static readonly IntPtr InvalidHandle = new IntPtr(-1);
    }
}
