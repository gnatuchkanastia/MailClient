using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MailClient.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
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
        }
        public static String DefaultStoragePath = Path.Combine(Application.StartupPath, "Storages");
        public static String DefaultAttachmentPath = Path.Combine(Application.StartupPath, "Attachments");
        public static Random r  = new Random();
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
    }
}