using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MailClient.WinForms
{
    public partial class MessageWindow : Form
    {
        private readonly ViewMessageDialogType _msgType = ViewMessageDialogType.NotSet;
        private readonly List<String> selectedFiles = new List<string>();
        public MessageWindow(ViewMessageDialogType msgType = ViewMessageDialogType.NewMessage)
        {
            _msgType = msgType;
            InitializeComponent();
            PostInitialize();
        }

        private void PostInitialize()
        {
            switch (_msgType)
            {
                case ViewMessageDialogType.NotSet:
                    throw new NotImplementedException();
                    break;
                case ViewMessageDialogType.NewMessage:
                    replyButton.Enabled = false;
                    fromTextBox.Text = String.Format("{0} ({1})",MailStorage.CurrentCredentials.DisplayName, MailStorage.CurrentCredentials.Username);
                    sendButton.Click += sendButton_Click;
                    break;
                case ViewMessageDialogType.StoredMessage:
                    sendButton.Text = "Forward";
                    sendButton.Click += forwardButton_Click;
                    addAttachBtn.Enabled = false;

                    toTextBox.ReadOnly = true;
                    subjTextBox.ReadOnly = true;
                    msgBodyRichTextBox.ReadOnly = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string formatLength(long bytes)
        {
            var resultFactor = "B";
            if (bytes > 1024)
            {
                bytes /= 1024;
                resultFactor = "KB";
            }
            if (bytes > 1024)
            {
                bytes /= 1024;
                resultFactor = "MB";
            }
            return String.Format("{0} {1}", bytes, resultFactor);
        }

        private void attachListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_msgType == ViewMessageDialogType.NewMessage)
                attachListView.ContextMenuStrip = attachListView.SelectedIndices.Count > 0 ? delAttachContextMenu : null;
            else if (_msgType == ViewMessageDialogType.StoredMessage)
                attachListView.ContextMenuStrip = attachListView.SelectedIndices.Count > 0
                    ? downloadAttachContextMenu
                    : null;
        }

        private void updateAttachmentList(List<String> paths)
        {
            attachListView.Items.Clear();
            foreach (var path in  paths)
            {
                var name = Path.GetFileName(path);
                var item = new ListViewItem(name);
                item.Tag = path;
                item.SubItems.Add(formatLength(new FileInfo(path).Length));
                attachListView.Items.Insert(0, item);
            }
        }

        private void addAttachBtn_Click(object sender, EventArgs e)
        {
            var d = new OpenFileDialog();
            d.Multiselect = true;
            var invalidFiles = new List<String>();
            if (d.ShowDialog() == DialogResult.OK)
            {
                foreach (var path in d.FileNames)
                {
                    var info = new FileInfo(path);
                    if (info.Length > 1048576) // > 1 MB
                    {
                        invalidFiles.Add(Path.GetFileName(path));
                        continue;
                    }
                    selectedFiles.Add(path);
                }
            }
            updateAttachmentList(selectedFiles);
            if (invalidFiles.Count > 0)
                MessageBox.Show(String.Format("Following files will not be attached due to their size (> 1 MB):\n{0}",
                    String.Join("\n", invalidFiles)));
            updateAttachmentList(selectedFiles);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in attachListView.SelectedItems)
            {
                selectedFiles.Remove(item.Tag as string);
                attachListView.Items.Remove(item);
            }
        }

        private MailMessageWrapper currentMsg;
        public void ShowReadMessage(MailMessageWrapper msg)
        {
            currentMsg = msg;
            fromTextBox.Text = String.Format("{0} ({1})", msg.From.Value, msg.From.Key);
            foreach (var kv in msg.To)
                toTextBox.Text += String.Format("{0} ({1}); ", kv.Value, kv.Key);
            toTextBox.Text = toTextBox.Text.Trim();
            subjTextBox.Text = msg.Subject;
            msgBodyRichTextBox.Text = msg.Body;
            foreach (var a in msg.Attachments)
            {
                var item = new ListViewItem(a.Value);
                item.SubItems.Add(formatLength(new FileInfo(a.Key).Length));
                attachListView.Items.Add(item);
            }
            this.ShowDialog();
        }
        public void ShowForwardMessage(MailMessageWrapper msg)
        {
            currentMsg = msg;
            fromTextBox.Text = String.Format("{0} ({1})", MailStorage.CurrentCredentials.DisplayName, MailStorage.CurrentCredentials.Username);
            subjTextBox.Text = "RE: " + msg.Subject;
            msgBodyRichTextBox.Text = msg.Body;
            this.ShowDialog();
        }
        public void ShowReplyMessage(MailMessageWrapper msg)
        {
            currentMsg = msg;
            fromTextBox.Text = String.Format("{0} ({1})", MailStorage.CurrentCredentials.DisplayName, MailStorage.CurrentCredentials.Username);
            var toList = msg.To.Where(addr => addr.Key != MailStorage.CurrentCredentials.Username).ToList();
            toList.Add(msg.From);
            foreach (var kv in toList)
                toTextBox.Text += String.Format("{0} ({1}); ", kv.Value, kv.Key);
            toTextBox.Text = toTextBox.Text.Trim();
            subjTextBox.Text = "RE: " + msg.Subject;
            msgBodyRichTextBox.Text = "";
            this.ShowDialog();
        }
        private void forwardButton_Click(object sender, EventArgs e)
        {
            this.Close();
            var msgForm = new MessageWindow(ViewMessageDialogType.NewMessage);
            msgForm.ShowForwardMessage(currentMsg);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            ComposeMessageAndSend();
        }

        private void replyButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
            var msgForm = new MessageWindow(ViewMessageDialogType.NewMessage);
            msgForm.ShowReplyMessage(currentMsg);
        }

        private void ComposeMessageAndSend()
        {
            var matches = Regex.Matches(toTextBox.Text, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            var mailingList = (from Match m in matches select m.Value).ToList();
            if (mailingList.Count < 1)
            {
                MessageBox.Show(
                    "Please, choose at least 1 recipient. \n*Use '(' and ')' to separate emails from display names.", "Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (msgBodyRichTextBox.Text.Trim().Length < 1)
            {
                MessageBox.Show("Please, enter message you want to send", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (subjTextBox.Text.Trim().Length < 1)
            {
                MessageBox.Show("Please, enter message subject", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var msg = new MailMessage();
            msg.From = new MailAddress(MailStorage.CurrentCredentials.Username);
            foreach (var a in mailingList)
                msg.To.Add(new MailAddress(a));
            
            msg.Subject = subjTextBox.Text;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = msgBodyRichTextBox.Text;
            foreach(var f in selectedFiles)
                msg.Attachments.Add(new Attachment(f));
            sendButton.Enabled = false;
            MailSender.Send(msg);
            this.Close();

        }

        private void addressBookBtn_Click(object sender, EventArgs e)
        {
            var addressBookWindow = new AddressBookWindow();
            addressBookWindow.ShowDialog();
        }
    }
}