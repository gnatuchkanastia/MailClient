namespace MailClient.WinForms
{
    partial class MessageWindow
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
            this.fromToLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fromtoTextBox = new System.Windows.Forms.TextBox();
            this.subjTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label3 = new System.Windows.Forms.Label();
            this.msgBodyRichTextBox = new System.Windows.Forms.RichTextBox();
            this.addressBookBtn = new System.Windows.Forms.Button();
            this.attachListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.delAttachContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAttachBtn = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.downloadAttachContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delAttachContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fromToLabel
            // 
            this.fromToLabel.AutoSize = true;
            this.fromToLabel.Location = new System.Drawing.Point(58, 9);
            this.fromToLabel.Name = "fromToLabel";
            this.fromToLabel.Size = new System.Drawing.Size(26, 13);
            this.fromToLabel.TabIndex = 0;
            this.fromToLabel.Text = "To: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Subject: ";
            // 
            // fromtoTextBox
            // 
            this.fromtoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fromtoTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.fromtoTextBox.Location = new System.Drawing.Point(90, 6);
            this.fromtoTextBox.Name = "fromtoTextBox";
            this.fromtoTextBox.Size = new System.Drawing.Size(420, 20);
            this.fromtoTextBox.TabIndex = 1;
            // 
            // subjTextBox
            // 
            this.subjTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subjTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.subjTextBox.Location = new System.Drawing.Point(90, 32);
            this.subjTextBox.Name = "subjTextBox";
            this.subjTextBox.Size = new System.Drawing.Size(501, 20);
            this.subjTextBox.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Message: ";
            // 
            // msgBodyRichTextBox
            // 
            this.msgBodyRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.msgBodyRichTextBox.Location = new System.Drawing.Point(90, 58);
            this.msgBodyRichTextBox.Name = "msgBodyRichTextBox";
            this.msgBodyRichTextBox.Size = new System.Drawing.Size(582, 273);
            this.msgBodyRichTextBox.TabIndex = 4;
            this.msgBodyRichTextBox.Text = "";
            // 
            // addressBookBtn
            // 
            this.addressBookBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addressBookBtn.Location = new System.Drawing.Point(516, 4);
            this.addressBookBtn.Name = "addressBookBtn";
            this.addressBookBtn.Size = new System.Drawing.Size(75, 23);
            this.addressBookBtn.TabIndex = 2;
            this.addressBookBtn.Text = "Adress book";
            this.addressBookBtn.UseVisualStyleBackColor = true;
            // 
            // attachListView
            // 
            this.attachListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.attachListView.FullRowSelect = true;
            this.attachListView.GridLines = true;
            this.attachListView.Location = new System.Drawing.Point(90, 337);
            this.attachListView.Name = "attachListView";
            this.attachListView.Size = new System.Drawing.Size(582, 100);
            this.attachListView.TabIndex = 6;
            this.attachListView.UseCompatibleStateImageBehavior = false;
            this.attachListView.View = System.Windows.Forms.View.Details;
            this.attachListView.SelectedIndexChanged += new System.EventHandler(this.attachListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 470;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 85;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 337);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Attachments: ";
            // 
            // delAttachContextMenu
            // 
            this.delAttachContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.delAttachContextMenu.Name = "delAttachContextMenu";
            this.delAttachContextMenu.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // addAttachBtn
            // 
            this.addAttachBtn.Location = new System.Drawing.Point(12, 414);
            this.addAttachBtn.Name = "addAttachBtn";
            this.addAttachBtn.Size = new System.Drawing.Size(72, 23);
            this.addAttachBtn.TabIndex = 5;
            this.addAttachBtn.Text = "Add";
            this.addAttachBtn.UseVisualStyleBackColor = true;
            this.addAttachBtn.Click += new System.EventHandler(this.addAttachBtn_Click);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(597, 4);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 48);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // downloadAttachContextMenu
            // 
            this.downloadAttachContextMenu.Name = "delAttachContextMenu";
            this.downloadAttachContextMenu.Size = new System.Drawing.Size(129, 26);
            // 
            // MessageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.addAttachBtn);
            this.Controls.Add(this.attachListView);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.addressBookBtn);
            this.Controls.Add(this.msgBodyRichTextBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.subjTextBox);
            this.Controls.Add(this.fromtoTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fromToLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MessageWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageWindow";
            this.delAttachContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fromToLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fromtoTextBox;
        private System.Windows.Forms.TextBox subjTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox msgBodyRichTextBox;
        private System.Windows.Forms.Button addressBookBtn;
        private System.Windows.Forms.ListView attachListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip delAttachContextMenu;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Button addAttachBtn;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.ContextMenuStrip downloadAttachContextMenu;
    }
}