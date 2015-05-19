using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            Task.Run(() =>
            {
                fetchAllMessages();
            });

        }

        private void fetchAllMessages()
        {
            toolStrip1.Invoke(() => statusLabel.Text = "Connecting to server...");
            using (var client = new ImapClient("imap.gmail.com", 993, "bsuir.250502@gmail.com", "lovekitkat", ssl: true))
            {
                toolStrip1.Invoke(() => statusLabel.Text = "Counting email messages...");
                var currentMsg = 0;
                var ids = client.Search(SearchCondition.All());
                var msgTotal = ids.Count();
                var msgs = client.GetMessages(ids);
                foreach (var message in msgs)
                {
                    toolStrip1.Invoke(() => statusLabel.Text = String.Format("{0:P} downloaded", ++currentMsg * 1.0 / msgTotal));
                    if (message.From == null) continue;
                    var item = new ListViewItem(message.Date().ToString());
                    item.SubItems.Add(message.Subject);
                    item.SubItems.Add(message.From.Address);

                    inboxListView.Invoke(() => inboxListView.Items.Add(item));
                }
                toolStrip1.Invoke(() => statusLabel.Text = "Ready");
            }
        }

        private void inboxListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            inboxListView.ContextMenuStrip = inboxListView.SelectedIndices.Count > 0 ? mailboxContextMenu : null;
        }

        private void readToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}