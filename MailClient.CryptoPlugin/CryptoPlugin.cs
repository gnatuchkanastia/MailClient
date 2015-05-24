using System;
using System.Net.Mail;
using System.Windows.Forms;
using MailClient.WinForms;

namespace MailClient.CryptoPlugin
{
    internal class CryptoPlugin : IMessagePlugin
    {
        public void ProcessMessage(MailMessage msg)
        {
            var key = new KeyphrasePromptWindow().PromptForPassPhrase();
            CryptMessage(msg, key);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        
        private void CryptMessage(MailMessage msg, string key)
        {
            var body = msg.Body.ToCharArray();
            var code = key.ToCharArray();
            for (var i = 0; i < body.Length;)
                for (var j = 0; j < code.Length; j++)
                    if (i > body.Length - 1) break;
                    else body[i++] ^= code[j];
            msg.Body = new String(body);
        }
    }
}