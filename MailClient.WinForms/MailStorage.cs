using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Xml.Serialization;

namespace MailClient.WinForms
{
    public class MailStorage 
    {
        private static MailStorage instance;

        private MailStorage()
        {
            Inbox = new List<MailMessageWrapper>();
            Sent = new List<MailMessageWrapper>();
            AddressBook = new List<KeyValuePair<string, string>>();
        }

        public Credentials Credentials { get; set; }
        public List<MailMessageWrapper> Inbox { get; set; }
        public List<MailMessageWrapper> Sent { get; set; }
        public List<KeyValuePair<String, String>> AddressBook { get; set; }
        public static Credentials CurrentCredentials { get; set; }

        public static MailStorage GetOrCreate(Credentials creds)
        {
            //load all xml-profiles
            //selects proper by username and pass
            //if requested profile does not exist - create new
            if (instance != null) return instance;
            var storages = LoadMailStorages();
            instance = storages.SingleOrDefault(s => s.Credentials.Username == creds.Username) ??
                       new MailStorage {Credentials = creds};
            return instance;
        }

        public static void SaveOnExit()
        {
            if (instance != null)
                SaveMailStorage(instance);
        }
        private static void SaveMailStorage(MailStorage s)
        {
            using (var fs = new FileStream(Path.Combine(Program.DefaultStoragePath, s.Credentials.Username.Replace("@","").ToString() + ".storage"), FileMode.Create))
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
                        var ser = new XmlSerializer(typeof (MailStorage));
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

    }
}