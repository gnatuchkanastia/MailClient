using System;
using System.Collections.Generic;
using System.IO;
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

                    break;
                case ViewMessageDialogType.StoredMessage:
                    sendButton.Enabled = false;
                    addAttachBtn.Enabled = false;

                    fromtoTextBox.ReadOnly = true;
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
    }
}