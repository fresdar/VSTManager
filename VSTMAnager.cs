using CefSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSTManager
{
    public partial class VSTManager : Form
    {
        public VSTManager()
        {
            InitializeComponent();
        }

        private enum nodeImage { folder, vst2, vst3 }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ListDirectory(TreeNodeCollection nodes, string[] files, nodeImage image)
        {
            foreach (var path in files)
            {
                TreeNodeCollection curNodes = nodes;
                string[] nodeNames = path.Split('\\');
                int i = 1;
                foreach (var nodeName in nodeNames)
                {
                    TreeNode[] newNode = curNodes
                                    .Cast<TreeNode>()
                                    .Where(r => r.Text == nodeName).ToArray();
                    if (newNode.Length == 0)
                    {
                        // last item 
                        int idx = (int)((i == nodeNames.Length)? image : nodeImage.folder);
                        curNodes = curNodes.Add(nodeName, nodeName, idx, idx).Nodes;
                    }
                    else
                    {
                        curNodes = newNode[0].Nodes;
                    }
                    i++;
                }
            }
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();

            Preferences pref = new Preferences();
            if (this.viewVST2xToolStripMenuItem.Checked)
            {
                pref.VST2FolderList.ForEach(delegate (String searchFolder)
                {
                    string[] files = Directory.GetFiles(searchFolder, pref.VST2Extension, SearchOption.AllDirectories);

                    ListDirectory(this.treeView1.Nodes, files, nodeImage.vst2);
                });
            }
            if (this.viewVST3ToolStripMenuItem.Checked)
            {
                pref.VST3FolderList.ForEach(delegate (String searchFolder)
                {
                    string[] files = Directory.GetFiles(searchFolder, pref.VST3Extension, SearchOption.AllDirectories);

                    ListDirectory(this.treeView1.Nodes, files, nodeImage.vst3);
                });
            }
            this.treeView1.ExpandAll();
        }

        private void checkDuplicatesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreferencesDialog dialog = new PreferencesDialog();
            dialog.ShowDialog();
        }

        private PluginProperties _properties;

        private static void AddKeywords(string data, ref List<string> keywords)
        {
            string[] words = data.Split(' ');
            foreach (string w in words)
            {
                if (!string.IsNullOrEmpty(w))
                {
                    if (!keywords.Exists(e => e == w))
                        keywords.Add(w);
                }
            }
        }
        private string GetPluginQuery(string path)
        {
            string query = string.Empty;
            List<string> keywords = new List<string>();

            if (!string.IsNullOrEmpty(_properties.CompanyName))
            {
                AddKeywords(_properties.CompanyName, ref keywords);
            }
            else
            {
                string[] words = path.Split('\\');
                if (words.Length > 1 && !words[words.Length - 2].Equals("VST3",StringComparison.InvariantCultureIgnoreCase))
                {
                    AddKeywords(words[words.Length - 2], ref keywords);
                }
            }
            if (!string.IsNullOrEmpty(_properties.ProductName))
            {
                AddKeywords(_properties.ProductName, ref keywords);
            }
            else
            {
                string[] words = path.Split('\\');
                if (words.Length > 1)
                {
                    AddKeywords(words[words.Length - 1].Replace(".dll","").Replace(".vst3",""), ref keywords);
                }
            }
            AddKeywords("vst", ref keywords);
            query = string.Join("+", keywords);

            return query;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string path = e.Node.FullPath;
            FileInfo fi = new FileInfo(path);

            this.vstInfoListBox.Items.Clear();
            this.pluginPropertyGrid.SelectedObject = null;
            string query = string.Empty;

            if (!fi.Attributes.HasFlag(FileAttributes.Directory))
            {
                //vst plugin info
                _properties = new PluginProperties();
                _properties.FullPath = path;
                _properties.Size = fi.Length;
                FileVersionInfo version = FileVersionInfo.GetVersionInfo(path);
                _properties.ProductVersion = version.ProductVersion;
                _properties.FileVersion = version.FileVersion;
                _properties.CompanyName = version.CompanyName;
                _properties.ProductName = version.ProductName;
                _properties.FileDescription = version.FileDescription;
                this.pluginPropertyGrid.SelectedObject = _properties;
                query = GetPluginQuery(path);
                VstPluginProperties vstProperties = new VstPluginProperties(path);
                List<string> vstInfo = vstProperties.getPluginInfo();
                if (vstInfo != null)
                {
                    this.vstInfoListBox.Items.AddRange(vstInfo.ToArray());
                }
                vstProperties.Dispose();
            }
            else
            {
                string[] words = path.Split('\\');
                if (words.Length > 1)
                {
                    query = words[words.Length - 1].Replace(' ', '+') + "+vst";
                }
            }
            Preferences pref = new Preferences();
            string address = pref.SearchEngineBaseUrl + "/?q=" + query;
            try
            {
                this.pluginWebSplitContainer.Panel1.Controls.Clear();
                this.pluginWebSplitContainer.Panel1.Controls.Clear();
                if (browser != null)
                    browser.Dispose();

                browser = new CefSharp.WinForms.ChromiumWebBrowser(address);
                browser.Dock = DockStyle.Fill;
                browser.DownloadHandler = new DownloadHandler();
                this.pluginWebSplitContainer.Panel1.Controls.Add(browser);
            }
            catch (System.UriFormatException)
            {
                return;
            }

            Cursor.Current = Cursors.Default;
        }

        private CefSharp.WinForms.ChromiumWebBrowser browser = null;
     }
}
