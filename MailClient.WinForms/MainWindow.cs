using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using S22.Imap;
using System.Threading.Tasks;

namespace MailClient.WinForms
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            MailStorage.CurrentCredentials = new Credentials()
            {
                DisplayName = "Группа 250502",
                Password = "lovekitkat",
                Username = "bsuir.250502@gmail.com"
            };
            var storage = MailStorage.GetOrCreate(MailStorage.CurrentCredentials);
            UpdateListView(storage);
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            var newMsgForm = new MessageWindow(ViewMessageDialogType.NewMessage);
            newMsgForm.ShowDialog();
        }

        private TabPage createTabWithListView(string tabHeader)
        {
            var tab = new TabPage();
            var listView = new ListView();
            var columns = new List<ColumnHeader>() { new ColumnHeader(), new ColumnHeader(), new ColumnHeader() };

            tab.Controls.Add(listView);
            tab.Location = new Point(4, 22);
            tab.Name = tabHeader;
            tab.Padding = new Padding(3);
            tab.Size = new Size(665, 293);
            tab.TabIndex = 0;
            tab.Text = tabHeader;
            tab.UseVisualStyleBackColor = true;

            columns[0].Text = "Date";
            columns[0].Width = 113;

            columns[1].Text = "Subject";
            columns[1].Width = 385;

            columns[2].Text = "From";
            columns[2].Width = 118;

            listView.Columns.AddRange(columns.ToArray());
            listView.Dock = DockStyle.Fill;
            listView.FullRowSelect = true;
            listView.GridLines = true;
            listView.Location = new Point(3, 3);
            listView.Name = "mailListView";
            listView.Size = new Size(659, 287);
            listView.TabIndex = 0;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;

            return tab;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
                fetchAllMessages(MailStorage.CurrentCredentials);
        }

        internal void fetchAllMessages(Credentials creds)
        {
            Task.Run(() =>
            {
                toolStrip1.Invoke(() => statusLabel.Text = "Connecting to server...");
                using (var client = new ImapClient("imap.gmail.com", 993, creds.Username, creds.Password, ssl: true))
                {
                    var storage = MailStorage.GetOrCreate(creds);

                    toolStrip1.Invoke(() => statusLabel.Text = "Counting email messages...");
                    var currentMsg = 0;
                    var ids = storage.Inbox.Count > 0 ? client.Search(SearchCondition.SentSince((DateTime)storage.Inbox[0].DateTime)) : client.Search(SearchCondition.All());
                    var msgTotal = ids.Count();
                    var msgs = client.GetMessages(ids);
                    foreach (var message in msgs)
                    {
                        var m = new MailMessageWrapper(message);
                        foreach (var attachment in message.Attachments)
                        {
                            var stream = attachment.ContentStream;
                            var attachPath = Path.Combine(Program.DefaultAttachmentPath, Program.GenerateRandomFileName(Program.DefaultAttachmentPath));
                            using (var fs = new FileStream(attachPath, FileMode.Create))
                                stream.CopyTo(fs);
                            m.Attachments.Add(new KeyValuePair<string, string>() { Key = attachPath, Value = attachment.Name });
                        }
                        toolStrip1.Invoke(() => statusLabel.Text = String.Format("{0:P} downloaded", ++currentMsg * 1.0 / msgTotal));
                        if (message.From == null) continue;
                        if (!storage.Inbox.Any(ms => ms.Subject == m.Subject))
                            storage.Inbox.Insert(0, m);
                        if (!storage.AddressBook.Any(kv => kv.Key == message.From.Address))
                            storage.AddressBook.Add(new KeyValuePair<string, string>() { Key = message.From.Address, Value = message.From.DisplayName });
                    }
                    UpdateListView(storage);
                }
            });
        }

        private void UpdateListView(MailStorage storage)
        {
            foreach (var message in storage.Inbox.OrderBy(m => m.DateTime))
            {
                var item = new ListViewItem(message.DateTime.ToString());
                item.SubItems.Add(message.Subject);
                item.SubItems.Add(message.From.Value);

                inboxListView.Invoke(() =>
                {
                    if (!listViewContainsMsg(inboxListView, message.Subject))
                        inboxListView.Items.Insert(0, item);
                });
            }
            foreach (var message in storage.Sent.OrderBy(m => m.DateTime))
            {
                var item = new ListViewItem(message.DateTime.ToString());
                item.SubItems.Add(message.Subject);
                item.SubItems.Add(storage.TryResolveEmail(message.To[0].Key));
                sentListView.Invoke(() =>
                {
                    if (!listViewContainsMsg(sentListView, message.Subject))
                        sentListView.Items.Insert(0, item);
                });
            }
            toolStrip1.Invoke(() => statusLabel.Text = "Ready");
        }

        private bool listViewContainsMsg(ListView listView, string subj)
        {
            return listView.Items.Cast<ListViewItem>()
                .Any(item => item.SubItems[1].Text == subj);
        }


        private void inboxListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            inboxListView.ContextMenuStrip = inboxListView.SelectedIndices.Count > 0 ? mailboxContextMenu : null;
        }

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var storage = MailStorage.GetOrCreate(MailStorage.CurrentCredentials);
            var allMessages = new List<MailMessageWrapper>();
            allMessages.AddRange(storage.Inbox);
            allMessages.AddRange(storage.Sent);
            var view = (from Control control in tabControl1.SelectedTab.Controls
                where control is ListView
                select control as ListView).First();
            var subj = view.SelectedItems[0].SubItems[1].Text;
            var msg = allMessages.First(m => m.Subject == subj);

            var msgForm = new MessageWindow(ViewMessageDialogType.StoredMessage);
            msgForm.ShowReadMessage(msg);
            fetchAllMessages(MailStorage.CurrentCredentials);
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            if (inboxListView.SelectedIndices.Count == 1)
                readToolStripMenuItem_Click(sender, e);
        }
    }
}