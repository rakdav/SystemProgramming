using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SystemProgramming
{
    internal class WindowFinder
    {
        private const int WM_GETTEXT = 0x000D;
        private const int WM_GETTEXTLENGTH = 0x000E;
        private const int WM_CLOSE = 0x0010;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        public static void KillWindow(string targetWindowTitle)
        {
            IntPtr targetWindowHandle = FindWindow(null, targetWindowTitle);
            if (targetWindowHandle == IntPtr.Zero)
            {
                Console.WriteLine("Target window not found");
                return;
            }
            int captionLength=SendMessage(targetWindowHandle,WM_GETTEXTLENGTH,0,IntPtr.Zero)+1;
            IntPtr captionBuffer=Marshal.AllocCoTaskMem(captionLength*2);
            SendMessage(targetWindowHandle,WM_GETTEXT,captionLength,captionBuffer);
            string? caption=Marshal.PtrToStringUni(captionBuffer);
            if (caption == null)
            {
                Console.WriteLine("Target window handle is invalid.");
                return;
            }
            Console.WriteLine($"Caption:{caption}");
            Marshal.FreeCoTaskMem(captionBuffer);
            int processId;
            GetWindowThreadProcessId(targetWindowHandle, out processId);
            if(processId != 0)
            {
                Process process=Process.GetProcessById(processId);
                process.Kill();
                Console.WriteLine("Process killed.");
            }
            Console.WriteLine();
        }
    }
}
