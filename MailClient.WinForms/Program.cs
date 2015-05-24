using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MailClient.WinForms
{
    static class WinAPI
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint RegisterWindowMessage(string lpString);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        public const uint HWND_BROADCAST = 0xFFFF;
        public const short SW_RESTORE = 9;
    }
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        public static uint BringToFrontMessage;
        public static String DefaultStoragePath = Path.Combine(Application.StartupPath, "Storages");
        public static String DefaultAttachmentPath = Path.Combine(Application.StartupPath, "Attachments");
        public static Random r = new Random();
        public static String GenerateRandomFileName(String folder)
        {
            var files = from file in Directory.GetFiles(folder)
                        select Path.GetFileNameWithoutExtension(file);
            var name = "";
            do
            {
                name = r.Next().ToString() + r.Next().ToString();
            } while (files.Contains(name));
            return name + ".attachment";
        }
        [STAThread]
        private static void Main()
        {
            BringToFrontMessage = WinAPI.RegisterWindowMessage("MyAppBringToFront");
            bool createdMutex = false;

            using (Mutex oneApp = new Mutex(true, "MyAppMutex", out createdMutex))
            {
                if (createdMutex)
                {
                    if (!Directory.Exists(DefaultStoragePath))
                        Directory.CreateDirectory(DefaultStoragePath);
                    if (!Directory.Exists(DefaultAttachmentPath))
                        Directory.CreateDirectory(DefaultAttachmentPath);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new MessageWindow(ViewMessageDialogType.StoredMessage));
                    Application.Run(new MainWindow());
                    MailStorage.SaveOnExit();
                    PluginManager.SaveOnExit();
                }
                else
                {
                    WinAPI.PostMessage(
                      (IntPtr)WinAPI.HWND_BROADCAST,
                      BringToFrontMessage,
                      IntPtr.Zero,
                      IntPtr.Zero);
                }
            }
        }
    }
}