using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSTManager
{
    public partial class PreferencesDialog : Form
    {
        public PreferencesDialog()
        {
            InitializeComponent();
            _properties = new Preferences();
            this.GeneralPrefPropGrid.SelectedObject = _properties;
            this.VST2FoldersListBox.Items.AddRange(_properties.VST2FolderList.ToArray());
            this.VST3FoldersListBox.Items.AddRange(_properties.VST3FolderList.ToArray());
        }

        private Preferences _properties;
        private void PreferencesDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            _properties.Save();
        }

        private void resetVST2FolderButton_Click(object sender, EventArgs e)
        {
            _properties.resetVST2FolderList();
            this.VST2FoldersListBox.Items.Clear();
            this.VST2FoldersListBox.Items.AddRange(_properties.VST2FolderList.ToArray());
        }
        private void clearVST2FolderButton_Click(object sender, EventArgs e)
        {
            _properties.VST2FolderList.Clear();
            this.VST2FoldersListBox.Items.Clear();
        }

        private void scanVST2FolderButton_Click(object sender, EventArgs e)
        {
            _properties.searchVST2Folders();
            this.VST2FoldersListBox.Items.Clear();
            this.VST2FoldersListBox.Items.AddRange(_properties.VST2FolderList.ToArray());
        }

        private void deleteVST2FolderButton_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(this.VST2FoldersListBox);
            selectedItems = this.VST2FoldersListBox.SelectedItems;

            if (this.VST2FoldersListBox.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    _properties.VST2FolderList.Remove(selectedItems[i] as string);
                    this.VST2FoldersListBox.Items.Remove(selectedItems[i]);
                }
            }
            else
                MessageBox.Show("Veuillez sélectionner un répertoire");
        }

        private void addVST2FolderButton_Click(object sender, EventArgs e)
        {
            this.VSTFolderBrowserDialog.Description = "VST2.x Folders";
            DialogResult result = this.VSTFolderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _properties.VST2FolderList.Add(this.VSTFolderBrowserDialog.SelectedPath);
                this.VST2FoldersListBox.Items.Add(this.VSTFolderBrowserDialog.SelectedPath);
            }
        }

        private void resetVST3FolderButton_Click(object sender, EventArgs e)
        {
            _properties.resetVST3FolderList();
            this.VST3FoldersListBox.Items.Clear();
            this.VST3FoldersListBox.Items.AddRange(_properties.VST3FolderList.ToArray());
        }

        private void clearVST3FolderButton_Click(object sender, EventArgs e)
        {
            _properties.VST3FolderList.Clear();
            this.VST3FoldersListBox.Items.Clear();
        }

        private void scanVST3FolderButton_Click(object sender, EventArgs e)
        {
            _properties.searchVST3Folders();
            this.VST3FoldersListBox.Items.Clear();
            this.VST3FoldersListBox.Items.AddRange(_properties.VST3FolderList.ToArray());
        }

        private void deleteVST3FolderButton_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(this.VST3FoldersListBox);
            selectedItems = this.VST3FoldersListBox.SelectedItems;

            if (this.VST3FoldersListBox.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    _properties.VST3FolderList.Remove(selectedItems[i] as string);
                    this.VST3FoldersListBox.Items.Remove(selectedItems[i]);
                }
            }
            else
                MessageBox.Show("Veuillez sélectionner un répertoire");
        }
        private void addVST3FolderButton_Click(object sender, EventArgs e)
        {
            this.VSTFolderBrowserDialog.Description = "VST3 Folders";
            DialogResult result = this.VSTFolderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _properties.VST3FolderList.Add(this.VSTFolderBrowserDialog.SelectedPath);
                this.VST3FoldersListBox.Items.Add(this.VSTFolderBrowserDialog.SelectedPath);
            }
        }
    }
}
