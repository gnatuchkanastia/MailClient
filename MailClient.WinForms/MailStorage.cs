using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MailClient.WinForms
{
    public class MailStorage : IDisposable
    {
        public static MailStorage GetOrCreate(Credentials creds)
        {
            //load all xml-profiles
            //selects proper by username and pass
            //if requested profile does not exist - create new
            var storages = LoadMailStorages();
            var requestedStorage = storages.SingleOrDefault(s => s.Credentials.Username == creds.Username);
            if (requestedStorage == null)
                return new MailStorage() {Credentials = creds};
            else
                return requestedStorage;
        }

        private static void SaveMailStorage(MailStorage s)
        {
            using (var fs = new FileStream(s.Credentials.GetHashCode().ToString(), FileMode.Create))
            {
                var ser = new XmlSerializer(typeof (MailStorage));
                ser.Serialize(fs, s);
            }
        }
        private static List<MailStorage> LoadMailStorages()
        {
            var result = new List<MailStorage>();
            try
            {
                var files = Directory.GetFiles(Program.DefaultStoragePath, "*.storage");
                foreach (var file in files)
                {
                    using (var fs = new FileStream(file, FileMode.Open))
                    {
                        var ser = new XmlSerializer(typeof(MailStorage));
                        var obj = ser.Deserialize(fs) as MailStorage;
                        if (obj != null)
                            result.Add(obj);
                    }
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        private static MailStorage instance;
        private MailStorage()
        {
            Inbox = new List<MailMessage>();
            Sent = new List<MailMessage>();
        }
        public Credentials Credentials { get; set; }
        public List<MailMessage> Inbox { get; set; }
        public List<MailMessage> Sent { get; set; }
        public void Dispose()
        {
            if (instance != null)
                SaveMailStorage(instance);
        }

        ~MailStorage()
        {
            Dispose();
        }
    }
}
