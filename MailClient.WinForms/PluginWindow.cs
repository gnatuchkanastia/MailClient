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

        private void pluginListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            pluginListView.ContextMenuStrip = pluginListView.SelectedIndices.Count > 0
                ? deletePluginContextMenuStrip
                : null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var name = pluginListView.SelectedItems[0].Text;
            PluginManager.UnloadPlugin(name);
            PluginManager.UpdatePluginListView(pluginListView);
        }

    }
}