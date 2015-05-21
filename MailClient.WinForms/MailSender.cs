using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace MailClient.WinForms
{
    public static class MailSender
    {
        public static event SendCompletedEventHandler SendCompleted;

        public static void Send(MailMessage msg)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(MailStorage.CurrentCredentials.Username,
                    MailStorage.CurrentCredentials.Password);

                if (SendCompleted != null)
                    client.SendCompleted += SendCompleted;
                client.SendAsync(msg, null);
                MailStorage.GetOrCreate(MailStorage.CurrentCredentials).Sent.Add(new MailMessageWrapper(msg){DateTime = DateTime.Now});
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error sending message");
            }
        }
    }
}
