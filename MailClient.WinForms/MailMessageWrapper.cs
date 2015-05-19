using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using S22.Imap;

namespace MailClient.WinForms
{
    public class MailMessageWrapper
    {
        public MailMessageWrapper()
        {
            From = new KeyValuePair<string, string>();
            To = new List<KeyValuePair<string, string>>();
            Attachments = new List<KeyValuePair<string, string>>();
        }

        public MailMessageWrapper(MailMessage msg) : this()
        {
            Message = msg;
        }
        private MailMessage _msg;
        [XmlIgnore]
        public MailMessage Message
        {
            get
            {
                return _msg;
            }
            set
            {
                _parseMessage(value);
                _msg = value;
            }
        }

        private void _parseMessage(MailMessage value)
        {
            Body = value.Body;
            Subject = value.Subject;
            DateTime = value.Date();
            From = new KeyValuePair<string, string>(){Key = value.From.Address,Value = value.From.DisplayName};
            var addresses = from addr in value.To
                select new KeyValuePair<String,String>(){Key = addr.Address, Value = addr.DisplayName};
            To.AddRange(addresses);
        }

        public DateTime? DateTime { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        /// <summary>
        /// Key - email
        /// Value - DisplayName
        /// </summary>
        public KeyValuePair<String, String> From { get; set; }
        /// <summary>
        /// Key - email
        /// Value - DisplayName
        /// </summary>
        public List<KeyValuePair<String, String>> To { get; set; }
        /// <summary>
        /// Key - path
        /// Value - filename
        /// </summary>
        public List<KeyValuePair<String,String>> Attachments { get; set; }
    }
}
