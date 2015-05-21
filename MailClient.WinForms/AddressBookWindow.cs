using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient.WinForms
{
    public partial class AddressBookWindow : Form
    {
        public AddressBookWindow()
        {
            InitializeComponent();
            UpdateAddressListView();
        }

        private bool viewContainsAddress(ListView view, string address)
        {
            foreach (ListViewItem item in view.Items)
                if (item.SubItems[1].Text == address)
                    return true;
            return false;
        }
        private void UpdateAddressListView()
        {
            foreach (var a in MailStorage.GetOrCreate(MailStorage.CurrentCredentials).AddressBook.OrderBy(k => k.Value))
            {
                var item = new ListViewItem(a.Value);
                item.SubItems.Add(a.Key);
                if (!viewContainsAddress(addressListView,a.Key))
                    addressListView.Items.Insert(0, item);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text.Trim().Length < 1)
                MessageBox.Show("Please enter name");
            if (emailTextBox.Text.Trim().Length < 1)
                MessageBox.Show("Please enter address");
            var kv = new KeyValuePair<string, string>() {Key = emailTextBox.Text, Value = nameTextBox.Text};
            var book = MailStorage.GetOrCreate(MailStorage.CurrentCredentials).AddressBook;
            if (book.Any(kp => kp.Key.Trim().ToLower() == emailTextBox.Text.Trim().ToLower()))
                MessageBox.Show("Such address already exists");
            else
                book.Add(kv);
            nameTextBox.Clear();
            emailTextBox.Clear();
            UpdateAddressListView();
        }

        private void addressListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (addressListView.SelectedItems.Count > 0)
                addressListView.ContextMenuStrip = copyAddressContextMenuStrip;
            else
                addressListView.ContextMenuStrip = null;
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = addressListView.SelectedItems[0];
            Clipboard.SetText(String.Format("{0} ({1}); ",item.SubItems[0].Text,item.SubItems[1].Text));
        }
    }
}
