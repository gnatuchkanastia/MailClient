namespace MailClient.WinForms
{
    partial class AddressBookWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addressListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.entriesCountLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.addButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.copyAddressContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.copyAddressContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addressListView);
            this.groupBox1.Controls.Add(this.entriesCountLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 377);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Addresses";
            // 
            // addressListView
            // 
            this.addressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.addressListView.FullRowSelect = true;
            this.addressListView.GridLines = true;
            this.addressListView.Location = new System.Drawing.Point(6, 42);
            this.addressListView.MultiSelect = false;
            this.addressListView.Name = "addressListView";
            this.addressListView.Size = new System.Drawing.Size(449, 329);
            this.addressListView.TabIndex = 2;
            this.addressListView.UseCompatibleStateImageBehavior = false;
            this.addressListView.View = System.Windows.Forms.View.Details;
            this.addressListView.SelectedIndexChanged += new System.EventHandler(this.addressListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 222;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Email";
            this.columnHeader2.Width = 205;
            // 
            // entriesCountLabel
            // 
            this.entriesCountLabel.AutoSize = true;
            this.entriesCountLabel.Location = new System.Drawing.Point(77, 26);
            this.entriesCountLabel.Name = "entriesCountLabel";
            this.entriesCountLabel.Size = new System.Drawing.Size(13, 13);
            this.entriesCountLabel.TabIndex = 1;
            this.entriesCountLabel.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entries total:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.addButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.emailTextBox);
            this.groupBox2.Controls.Add(this.nameTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 119);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New address";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(124, 79);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(220, 23);
            this.addButton.TabIndex = 11;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Email: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Name: ";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(124, 48);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(220, 20);
            this.emailTextBox.TabIndex = 8;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(124, 19);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(220, 20);
            this.nameTextBox.TabIndex = 7;
            // 
            // copyAddressContextMenuStrip
            // 
            this.copyAddressContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem});
            this.copyAddressContextMenuStrip.Name = "copyAddressContextMenuStrip";
            this.copyAddressContextMenuStrip.Size = new System.Drawing.Size(170, 48);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // AddressBookWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 526);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddressBookWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Address book";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.copyAddressContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView addressListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label entriesCountLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ContextMenuStrip copyAddressContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
    }
}