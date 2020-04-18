namespace VSTManager
{
    partial class PreferencesDialog
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
            this.GeneralPrefPropGrid = new System.Windows.Forms.PropertyGrid();
            this.GeneralTabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.VST2TabPage = new System.Windows.Forms.TabPage();
            this.addVST2FolderButton = new System.Windows.Forms.Button();
            this.VST2FoldersListBox = new System.Windows.Forms.ListBox();
            this.VST3TabPage = new System.Windows.Forms.TabPage();
            this.deleteVST3FolderButton = new System.Windows.Forms.Button();
            this.scanVST3FolderButton = new System.Windows.Forms.Button();
            this.addVST3FolderButton = new System.Windows.Forms.Button();
            this.VST3FoldersListBox = new System.Windows.Forms.ListBox();
            this.VSTFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.clearVST3FolderButton = new System.Windows.Forms.Button();
            this.clearVST2FolderButton = new System.Windows.Forms.Button();
            this.deleteVST2FolderButton = new System.Windows.Forms.Button();
            this.scanVST2FolderButton = new System.Windows.Forms.Button();
            this.resetVST2FolderButton = new System.Windows.Forms.Button();
            this.resetVST3FolderButton = new System.Windows.Forms.Button();
            this.GeneralTabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.VST2TabPage.SuspendLayout();
            this.VST3TabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // GeneralPrefPropGrid
            // 
            this.GeneralPrefPropGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralPrefPropGrid.Location = new System.Drawing.Point(3, 3);
            this.GeneralPrefPropGrid.Name = "GeneralPrefPropGrid";
            this.GeneralPrefPropGrid.Size = new System.Drawing.Size(786, 418);
            this.GeneralPrefPropGrid.TabIndex = 0;
            // 
            // GeneralTabControl
            // 
            this.GeneralTabControl.Controls.Add(this.generalTabPage);
            this.GeneralTabControl.Controls.Add(this.VST2TabPage);
            this.GeneralTabControl.Controls.Add(this.VST3TabPage);
            this.GeneralTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralTabControl.Location = new System.Drawing.Point(0, 0);
            this.GeneralTabControl.Name = "GeneralTabControl";
            this.GeneralTabControl.SelectedIndex = 0;
            this.GeneralTabControl.Size = new System.Drawing.Size(800, 450);
            this.GeneralTabControl.TabIndex = 1;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.GeneralPrefPropGrid);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalTabPage.Size = new System.Drawing.Size(792, 424);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // VST2TabPage
            // 
            this.VST2TabPage.Controls.Add(this.resetVST2FolderButton);
            this.VST2TabPage.Controls.Add(this.clearVST2FolderButton);
            this.VST2TabPage.Controls.Add(this.deleteVST2FolderButton);
            this.VST2TabPage.Controls.Add(this.scanVST2FolderButton);
            this.VST2TabPage.Controls.Add(this.addVST2FolderButton);
            this.VST2TabPage.Controls.Add(this.VST2FoldersListBox);
            this.VST2TabPage.Location = new System.Drawing.Point(4, 22);
            this.VST2TabPage.Name = "VST2TabPage";
            this.VST2TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.VST2TabPage.Size = new System.Drawing.Size(792, 424);
            this.VST2TabPage.TabIndex = 1;
            this.VST2TabPage.Text = "VST2.x";
            this.VST2TabPage.UseVisualStyleBackColor = true;
            // 
            // addVST2FolderButton
            // 
            this.addVST2FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addVST2FolderButton.Location = new System.Drawing.Point(628, 6);
            this.addVST2FolderButton.Name = "addVST2FolderButton";
            this.addVST2FolderButton.Size = new System.Drawing.Size(75, 23);
            this.addVST2FolderButton.TabIndex = 1;
            this.addVST2FolderButton.Text = "Ajouter";
            this.addVST2FolderButton.UseVisualStyleBackColor = true;
            this.addVST2FolderButton.Click += new System.EventHandler(this.addVST2FolderButton_Click);
            // 
            // VST2FoldersListBox
            // 
            this.VST2FoldersListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VST2FoldersListBox.FormattingEnabled = true;
            this.VST2FoldersListBox.Location = new System.Drawing.Point(6, 44);
            this.VST2FoldersListBox.Name = "VST2FoldersListBox";
            this.VST2FoldersListBox.Size = new System.Drawing.Size(778, 368);
            this.VST2FoldersListBox.TabIndex = 0;
            // 
            // VST3TabPage
            // 
            this.VST3TabPage.Controls.Add(this.resetVST3FolderButton);
            this.VST3TabPage.Controls.Add(this.clearVST3FolderButton);
            this.VST3TabPage.Controls.Add(this.deleteVST3FolderButton);
            this.VST3TabPage.Controls.Add(this.scanVST3FolderButton);
            this.VST3TabPage.Controls.Add(this.addVST3FolderButton);
            this.VST3TabPage.Controls.Add(this.VST3FoldersListBox);
            this.VST3TabPage.Location = new System.Drawing.Point(4, 22);
            this.VST3TabPage.Name = "VST3TabPage";
            this.VST3TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.VST3TabPage.Size = new System.Drawing.Size(792, 424);
            this.VST3TabPage.TabIndex = 2;
            this.VST3TabPage.Text = "VST3";
            this.VST3TabPage.UseVisualStyleBackColor = true;
            // 
            // deleteVST3FolderButton
            // 
            this.deleteVST3FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteVST3FolderButton.Location = new System.Drawing.Point(709, 6);
            this.deleteVST3FolderButton.Name = "deleteVST3FolderButton";
            this.deleteVST3FolderButton.Size = new System.Drawing.Size(75, 23);
            this.deleteVST3FolderButton.TabIndex = 3;
            this.deleteVST3FolderButton.Text = "Supprimer";
            this.deleteVST3FolderButton.UseVisualStyleBackColor = true;
            this.deleteVST3FolderButton.Click += new System.EventHandler(this.deleteVST3FolderButton_Click);
            // 
            // scanVST3FolderButton
            // 
            this.scanVST3FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scanVST3FolderButton.Location = new System.Drawing.Point(500, 6);
            this.scanVST3FolderButton.Name = "scanVST3FolderButton";
            this.scanVST3FolderButton.Size = new System.Drawing.Size(75, 23);
            this.scanVST3FolderButton.TabIndex = 2;
            this.scanVST3FolderButton.Text = "Scanner";
            this.scanVST3FolderButton.UseVisualStyleBackColor = true;
            this.scanVST3FolderButton.Click += new System.EventHandler(this.scanVST3FolderButton_Click);
            // 
            // addVST3FolderButton
            // 
            this.addVST3FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addVST3FolderButton.Location = new System.Drawing.Point(628, 6);
            this.addVST3FolderButton.Name = "addVST3FolderButton";
            this.addVST3FolderButton.Size = new System.Drawing.Size(75, 23);
            this.addVST3FolderButton.TabIndex = 1;
            this.addVST3FolderButton.Text = "Ajouter";
            this.addVST3FolderButton.UseVisualStyleBackColor = true;
            this.addVST3FolderButton.Click += new System.EventHandler(this.addVST3FolderButton_Click);
            // 
            // VST3FoldersListBox
            // 
            this.VST3FoldersListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VST3FoldersListBox.FormattingEnabled = true;
            this.VST3FoldersListBox.Location = new System.Drawing.Point(6, 44);
            this.VST3FoldersListBox.Name = "VST3FoldersListBox";
            this.VST3FoldersListBox.Size = new System.Drawing.Size(778, 368);
            this.VST3FoldersListBox.TabIndex = 0;
            // 
            // clearVST3FolderButton
            // 
            this.clearVST3FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearVST3FolderButton.Location = new System.Drawing.Point(419, 6);
            this.clearVST3FolderButton.Name = "clearVST3FolderButton";
            this.clearVST3FolderButton.Size = new System.Drawing.Size(75, 23);
            this.clearVST3FolderButton.TabIndex = 4;
            this.clearVST3FolderButton.Text = "Vider";
            this.clearVST3FolderButton.UseVisualStyleBackColor = true;
            this.clearVST3FolderButton.Click += new System.EventHandler(this.clearVST3FolderButton_Click);
            // 
            // clearVST2FolderButton
            // 
            this.clearVST2FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearVST2FolderButton.Location = new System.Drawing.Point(419, 6);
            this.clearVST2FolderButton.Name = "clearVST2FolderButton";
            this.clearVST2FolderButton.Size = new System.Drawing.Size(75, 23);
            this.clearVST2FolderButton.TabIndex = 7;
            this.clearVST2FolderButton.Text = "Vider";
            this.clearVST2FolderButton.UseVisualStyleBackColor = true;
            this.clearVST2FolderButton.Click += new System.EventHandler(this.clearVST2FolderButton_Click);
            // 
            // deleteVST2FolderButton
            // 
            this.deleteVST2FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteVST2FolderButton.Location = new System.Drawing.Point(709, 6);
            this.deleteVST2FolderButton.Name = "deleteVST2FolderButton";
            this.deleteVST2FolderButton.Size = new System.Drawing.Size(75, 23);
            this.deleteVST2FolderButton.TabIndex = 6;
            this.deleteVST2FolderButton.Text = "Supprimer";
            this.deleteVST2FolderButton.UseVisualStyleBackColor = true;
            this.deleteVST2FolderButton.Click += new System.EventHandler(this.deleteVST2FolderButton_Click);
            // 
            // scanVST2FolderButton
            // 
            this.scanVST2FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scanVST2FolderButton.Location = new System.Drawing.Point(500, 6);
            this.scanVST2FolderButton.Name = "scanVST2FolderButton";
            this.scanVST2FolderButton.Size = new System.Drawing.Size(75, 23);
            this.scanVST2FolderButton.TabIndex = 5;
            this.scanVST2FolderButton.Text = "Scanner";
            this.scanVST2FolderButton.UseVisualStyleBackColor = true;
            this.scanVST2FolderButton.Click += new System.EventHandler(this.scanVST2FolderButton_Click);
            // 
            // resetVST2FolderButton
            // 
            this.resetVST2FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetVST2FolderButton.Location = new System.Drawing.Point(338, 6);
            this.resetVST2FolderButton.Name = "resetVST2FolderButton";
            this.resetVST2FolderButton.Size = new System.Drawing.Size(75, 23);
            this.resetVST2FolderButton.TabIndex = 8;
            this.resetVST2FolderButton.Text = "Réinitialiser";
            this.resetVST2FolderButton.UseVisualStyleBackColor = true;
            this.resetVST2FolderButton.Click += new System.EventHandler(this.resetVST2FolderButton_Click);
            // 
            // resetVST3FolderButton
            // 
            this.resetVST3FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetVST3FolderButton.Location = new System.Drawing.Point(338, 6);
            this.resetVST3FolderButton.Name = "resetVST3FolderButton";
            this.resetVST3FolderButton.Size = new System.Drawing.Size(75, 23);
            this.resetVST3FolderButton.TabIndex = 9;
            this.resetVST3FolderButton.Text = "Réinitialiser";
            this.resetVST3FolderButton.UseVisualStyleBackColor = true;
            this.resetVST3FolderButton.Click += new System.EventHandler(this.resetVST3FolderButton_Click);
            // 
            // PreferencesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GeneralTabControl);
            this.Name = "PreferencesDialog";
            this.Text = "Preferences";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreferencesDialog_FormClosing);
            this.GeneralTabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.VST2TabPage.ResumeLayout(false);
            this.VST3TabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PropertyGrid GeneralPrefPropGrid;
        private System.Windows.Forms.TabControl GeneralTabControl;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.TabPage VST2TabPage;
        private System.Windows.Forms.ListBox VST2FoldersListBox;
        private System.Windows.Forms.TabPage VST3TabPage;
        private System.Windows.Forms.ListBox VST3FoldersListBox;
        private System.Windows.Forms.Button addVST2FolderButton;
        private System.Windows.Forms.Button addVST3FolderButton;
        protected System.Windows.Forms.FolderBrowserDialog VSTFolderBrowserDialog;
        private System.Windows.Forms.Button scanVST3FolderButton;
        private System.Windows.Forms.Button deleteVST3FolderButton;
        private System.Windows.Forms.Button clearVST3FolderButton;
        private System.Windows.Forms.Button clearVST2FolderButton;
        private System.Windows.Forms.Button deleteVST2FolderButton;
        private System.Windows.Forms.Button scanVST2FolderButton;
        private System.Windows.Forms.Button resetVST2FolderButton;
        private System.Windows.Forms.Button resetVST3FolderButton;
    }
}