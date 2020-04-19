namespace VSTManager
{
    partial class VSTManager
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VSTManager));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkDuplicatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewVST2xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewVST3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeviewImageList = new System.Windows.Forms.ImageList(this.components);
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.pluginWebSplitContainer = new System.Windows.Forms.SplitContainer();
            this.pluginSplitContainer = new System.Windows.Forms.SplitContainer();
            this.vstInfoListBox = new System.Windows.Forms.ListBox();
            this.pluginPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pluginWebSplitContainer)).BeginInit();
            this.pluginWebSplitContainer.Panel2.SuspendLayout();
            this.pluginWebSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pluginSplitContainer)).BeginInit();
            this.pluginSplitContainer.Panel1.SuspendLayout();
            this.pluginSplitContainer.Panel2.SuspendLayout();
            this.pluginSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.pluginsToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fileToolStripMenuItem.Text = "Fichier";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.preferencesToolStripMenuItem.Text = "Préférences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.quitToolStripMenuItem.Text = "&Quitter";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanToolStripMenuItem,
            this.checkDuplicatesToolStripMenuItem});
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.scanToolStripMenuItem.Text = "Scanner";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.scanToolStripMenuItem_Click);
            // 
            // checkDuplicatesToolStripMenuItem
            // 
            this.checkDuplicatesToolStripMenuItem.Name = "checkDuplicatesToolStripMenuItem";
            this.checkDuplicatesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.checkDuplicatesToolStripMenuItem.Text = "Détecter les doublons";
            this.checkDuplicatesToolStripMenuItem.Click += new System.EventHandler(this.checkDuplicatesToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewVST2xToolStripMenuItem,
            this.viewVST3ToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.viewToolStripMenuItem.Text = "Affichage";
            // 
            // viewVST2xToolStripMenuItem
            // 
            this.viewVST2xToolStripMenuItem.Checked = true;
            this.viewVST2xToolStripMenuItem.CheckOnClick = true;
            this.viewVST2xToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewVST2xToolStripMenuItem.Name = "viewVST2xToolStripMenuItem";
            this.viewVST2xToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.viewVST2xToolStripMenuItem.Text = "VST 2.x";
            // 
            // viewVST3ToolStripMenuItem
            // 
            this.viewVST3ToolStripMenuItem.Checked = true;
            this.viewVST3ToolStripMenuItem.CheckOnClick = true;
            this.viewVST3ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewVST3ToolStripMenuItem.Name = "viewVST3ToolStripMenuItem";
            this.viewVST3ToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.viewVST3ToolStripMenuItem.Text = "VST 3";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.treeviewImageList;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(154, 426);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // treeviewImageList
            // 
            this.treeviewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeviewImageList.ImageStream")));
            this.treeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeviewImageList.Images.SetKeyName(0, "folder.gif");
            this.treeviewImageList.Images.SetKeyName(1, "vst2.gif");
            this.treeviewImageList.Images.SetKeyName(2, "vst3.gif");
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.treeView1);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.pluginWebSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(800, 426);
            this.mainSplitContainer.SplitterDistance = 154;
            this.mainSplitContainer.TabIndex = 2;
            // 
            // pluginWebSplitContainer
            // 
            this.pluginWebSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginWebSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.pluginWebSplitContainer.Name = "pluginWebSplitContainer";
            // 
            // pluginWebSplitContainer.Panel2
            // 
            this.pluginWebSplitContainer.Panel2.Controls.Add(this.pluginSplitContainer);
            this.pluginWebSplitContainer.Size = new System.Drawing.Size(642, 426);
            this.pluginWebSplitContainer.SplitterDistance = 462;
            this.pluginWebSplitContainer.TabIndex = 1;
            // 
            // pluginSplitContainer
            // 
            this.pluginSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.pluginSplitContainer.Name = "pluginSplitContainer";
            this.pluginSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // pluginSplitContainer.Panel1
            // 
            this.pluginSplitContainer.Panel1.Controls.Add(this.vstInfoListBox);
            // 
            // pluginSplitContainer.Panel2
            // 
            this.pluginSplitContainer.Panel2.Controls.Add(this.pluginPropertyGrid);
            this.pluginSplitContainer.Size = new System.Drawing.Size(176, 426);
            this.pluginSplitContainer.SplitterDistance = 328;
            this.pluginSplitContainer.TabIndex = 7;
            // 
            // vstInfoListBox
            // 
            this.vstInfoListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vstInfoListBox.FormattingEnabled = true;
            this.vstInfoListBox.Location = new System.Drawing.Point(0, 0);
            this.vstInfoListBox.Name = "vstInfoListBox";
            this.vstInfoListBox.Size = new System.Drawing.Size(176, 328);
            this.vstInfoListBox.TabIndex = 0;
            // 
            // pluginPropertyGrid
            // 
            this.pluginPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.pluginPropertyGrid.Name = "pluginPropertyGrid";
            this.pluginPropertyGrid.Size = new System.Drawing.Size(176, 94);
            this.pluginPropertyGrid.TabIndex = 6;
            // 
            // VSTManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "VSTManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VST Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.pluginWebSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pluginWebSplitContainer)).EndInit();
            this.pluginWebSplitContainer.ResumeLayout(false);
            this.pluginSplitContainer.Panel1.ResumeLayout(false);
            this.pluginSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pluginSplitContainer)).EndInit();
            this.pluginSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.PropertyGrid pluginPropertyGrid;
        private System.Windows.Forms.SplitContainer pluginSplitContainer;
        private System.Windows.Forms.ListBox vstInfoListBox;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewVST2xToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewVST3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkDuplicatesToolStripMenuItem;
        private System.Windows.Forms.ImageList treeviewImageList;
        private System.Windows.Forms.SplitContainer pluginWebSplitContainer;
    }
}

