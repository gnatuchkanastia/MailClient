using System;
using System.Windows.Forms;

namespace MailClient.CryptoPlugin
{
    public partial class KeyphrasePromptWindow : Form
    {
        public KeyphrasePromptWindow()
        {
            InitializeComponent();
        }

        public String GetPassPhrase(string pass)
        {
            ShowDialog();
            return textBox1.Text;
        }
    }
}