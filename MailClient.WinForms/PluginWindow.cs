using System;
using System.Windows.Forms;

namespace MailClient.WinForms
{
    public partial class PluginWindow : Form
    {
        public PluginWindow()
        {
            InitializeComponent();
            PluginManager.UpdatePluginListView(pluginListView);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var path = "";
                var dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.Filter = "Plugin library (*.dll) | *.dll";
                if (dialog.ShowDialog() == DialogResult.OK)
                    PluginManager.LoadNewPlugin(dialog.FileName);
                PluginManager.UpdatePluginListView(pluginListView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("{0}: {1}", ex.GetType(), ex.Message));
            }
        }
    }
}