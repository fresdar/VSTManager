using CefSharp;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace VSTManager
{
    public partial class VSTManager : Form
    {
        public VSTManager()
        {
            InitializeComponent();
            // load last search
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "VSTManager\\Search";
            const string keyName = userRoot + "\\" + subkey;

            this.textBoxBrandSearch.Text = (string)Registry.GetValue(keyName, "Brand", "");
            this.textBoxModelSearch.Text = (string)Registry.GetValue(keyName, "Model", "");
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
                    try
                    {
                        string[] files = Directory.GetFiles(searchFolder, pref.VST3Extension, SearchOption.AllDirectories);

                        ListDirectory(this.treeView1.Nodes, files, nodeImage.vst3);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
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
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string path = e.Node.FullPath;

            this.pluginPropertyGrid.SelectedObject = null;
            string query = string.Empty;
            _properties = new PluginProperties(path);
            this.pluginPropertyGrid.SelectedObject = _properties;

            Preferences pref = new Preferences();
            string address = pref.SearchEngineBaseUrl + "/?q=" + _properties.Query;
            try
            {
                this.panelWebContent.Controls.Clear();
                if (browser != null)
                    browser.Dispose();

                browser = new CefSharp.WinForms.ChromiumWebBrowser(address);
                browser.Dock = DockStyle.Fill;
                browser.DownloadHandler = new DownloadHandler();
                this.panelWebContent.Controls.Add(browser);
            }
            catch (System.UriFormatException)
            {
                return;
            }

            Cursor.Current = Cursors.Default;
        }
        public CefSharp.WinForms.ChromiumWebBrowser browser = null;
        private const string resourcename = "http://mysearch.html";
        private void loadHtml(CefSharp.WinForms.ChromiumWebBrowser browser, string html)
        {
            System.IO.MemoryStream memorystream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(html));
            browser.RegisterResourceHandler(resourcename, memorystream);
            browser.Load(resourcename);
            browser.UnRegisterResourceHandler(resourcename);
        }
 
        private WebScraper m_scraper = new WebScraper();
        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.textBoxBrandSearch.Text) && string.IsNullOrEmpty(this.textBoxModelSearch.Text))
                    return;

                Cursor.Current = Cursors.WaitCursor;
                const string userRoot = "HKEY_CURRENT_USER";
                const string subkey = "VSTManager\\Search";
                const string keyName = userRoot + "\\" + subkey;

                Registry.SetValue(keyName, "Brand", this.textBoxBrandSearch.Text, RegistryValueKind.String);
                Registry.SetValue(keyName, "Model", this.textBoxModelSearch.Text, RegistryValueKind.String);

                m_scraper.Search(this.textBoxBrandSearch.Text.Replace(' ','+'), this.textBoxModelSearch.Text.Replace(' ', '+'), this.checkBoxModelSearchOp.Checked);

                string result = await m_scraper.getResult();
                this.panelWebContent.Controls.Clear();
                if (browser != null)
                    browser.Dispose();

                browser = new CefSharp.WinForms.ChromiumWebBrowser("about:blank");
                browser.Dock = DockStyle.Fill;
                browser.DownloadHandler = new DownloadHandler();

                loadHtml(browser, result);
                this.panelWebContent.Controls.Add(browser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (browser != null && browser.CanGoBack)
                browser.Back();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (browser != null && browser.CanGoForward)
                browser.Forward();
        }
    }
}
