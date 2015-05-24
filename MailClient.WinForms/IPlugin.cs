using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace MailClient.WinForms
{
    public interface IMessagePlugin : IDisposable
    {
        void ProcessMessage(MailMessage msg);
    }
}