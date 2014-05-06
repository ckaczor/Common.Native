using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Common.Native
{
    public static class TypeExtensions
    {
        public static int HighWord(this int value)
        {
            return ((short) (((value) >> 16) & 0xFFFF));
        }

        public static int LowWord(this int value)
        {
            return ((short) value);
        }

        public static int HighWord(this IntPtr value)
        {
            return ((int) value).HighWord();
        }

        public static int LowWord(this IntPtr value)
        {
            return ((int) value).LowWord();
        }
    }

    public static class Functions
    {
        public static class Window
        {
            public static bool IsChild(IntPtr childHandle, IntPtr parentHandle)
            {
                if (childHandle == parentHandle)
                    return true;

                if (childHandle == IntPtr.Zero)
                    return false;

                return IsChild(User32.GetParent(childHandle), parentHandle);
            }

            public static string GetText(IntPtr hWnd)
            {
                // Allocate correct string length first
                int length = User32.GetWindowTextLength(hWnd);
                var sb = new StringBuilder(length + 1);
                User32.GetWindowText(hWnd, sb, sb.Capacity);
                return sb.ToString();
            }

            public static string GetClass(IntPtr hWnd)
            {
                // Allocate correct string length first
                const int length = 255;
                var sb = new StringBuilder(length + 1);
                User32.RealGetWindowClass(hWnd, sb, sb.Capacity);
                return sb.ToString();
            }

            public static void Show(IntPtr hWnd, Constants.ShowWindowCommand command)
            {
                var windowPlacement = new Structures.WindowPlacement();
                User32.GetWindowPlacement(hWnd, ref windowPlacement);
                windowPlacement.ShowCommand = command;
                User32.SetWindowPlacement(hWnd, ref windowPlacement);
            }

            public static Icon GetSmallIcon(IntPtr hWnd)
            {
                try
                {
                    IntPtr hIcon;

                    User32.SendMessageTimeout(hWnd,
                        (uint) Constants.WindowMessage.GetIcon,
                        (IntPtr) Constants.ICON_SMALL,
                        IntPtr.Zero,
                        Constants.SendMessageTimeoutFlags.AbortIfHung,
                        1000,
                        out hIcon);

                    if (hIcon == IntPtr.Zero)
                    {
                        hIcon = User32.GetClassLongPtr(hWnd, Constants.ClassLongFlags.GCLP_HICONSM);
                    }

                    if (hIcon == IntPtr.Zero)
                    {
                        User32.SendMessageTimeout(
                            hWnd,
                            (uint) Constants.WindowMessage.QueryDragIcon,
                            IntPtr.Zero,
                            IntPtr.Zero,
                            Constants.SendMessageTimeoutFlags.AbortIfHung,
                            1000,
                            out hIcon);
                    }

                    return hIcon == IntPtr.Zero ? null : Icon.FromHandle(hIcon);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static class User32
        {
            public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowPlacement(IntPtr hWnd, ref Structures.WindowPlacement lpwndpl);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref Structures.WindowPlacement lpwndpl);

            [DllImport("user32.dll")]
            public static extern uint RealGetWindowClass(IntPtr hwnd, [Out] StringBuilder pszType, int cchType);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern int GetWindowTextLength(IntPtr hWnd);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam, Constants.SendMessageTimeoutFlags flags, uint timeout, out IntPtr result);

            [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
            public static extern IntPtr GetParent(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll")]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern uint RegisterWindowMessage(string lpString);

            public static IntPtr SetWindowLongPtrSmart(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
            {
                if (IntPtr.Size == 8)
                    return SetWindowLongPtr(hWnd, nIndex, dwNewLong);

                return new IntPtr(SetWindowLong(hWnd, nIndex, dwNewLong.ToInt32()));
            }

            [DllImport("user32.dll", EntryPoint = "GetClassLong")]
            public static extern uint GetClassLongPtr32(IntPtr hWnd, Constants.ClassLongFlags nIndex);

            [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
            public static extern IntPtr GetClassLongPtr64(IntPtr hWnd, Constants.ClassLongFlags nIndex);

            public static IntPtr GetClassLongPtr(IntPtr hWnd, Constants.ClassLongFlags nIndex)
            {
                if (IntPtr.Size > 4)
                    return GetClassLongPtr64(hWnd, nIndex);
                
                return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
            }
        }

        public static class Kernel32
        {
            [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
            public static extern void SetLastError(int dwErrorCode);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr CreateToolhelp32Snapshot(Constants.SnapshotFlags flags, uint processId);

            [DllImport("kernel32.dll")]
            public static extern bool Process32First(IntPtr handle, ref Structures.ProcessEntry32 processInfo);

            [DllImport("kernel32.dll")]
            public static extern bool Process32Next(IntPtr handle, ref Structures.ProcessEntry32 processInfo);

            [DllImport("kernel32.dll")]
            public static extern bool CloseHandle(IntPtr handle);

            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(Constants.ProcessAccess desiredAccess, bool inheritHandle, uint processId);

            [DllImport("kernel32.dll")]
            public static extern bool GetProcessTimes(IntPtr processHandle, out long creationTime, out long exitTime, out long kernelTime, out long userTime);

            [DllImport("kernel32.dll")]
            public static extern uint GetCurrentThreadId();
        }
    }
}
