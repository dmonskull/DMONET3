using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestUI;
using XDevkit;

namespace DMONET3.Forms
{
    public partial class QuickLaunch : DevExpress.XtraEditors.XtraForm
    {
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        private string iniFilePath;
        private FileIniDataParser parser;
        private ImageList imageList;
        public Form1 form1;
        private string gameId;
        public QuickLaunch()
        {
            InitializeComponent();
            iniFilePath = Path.Combine(Application.StartupPath, "INIs/Quicklaunch/quicklaunch.ini");
            parser = new FileIniDataParser();
            InitializeImageList();
            InitializeContextMenu();
            LoadQuickLaunchItems();
        }
        private void QuickLaunch_Load(object sender, EventArgs e)
        {
            quickLaunchListView.View = View.LargeIcon;
            quickLaunchListView.LargeImageList = imageList;
        }
        private void InitializeImageList()
        {
            imageList = new ImageList();
            imageList.Images.Add("default", Image.FromFile("Images/cover.png"));
            imageList.ImageSize = new Size(64, 64);
        }
        private void InitializeContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem launchItem = new ToolStripMenuItem("Launch");
            ToolStripMenuItem removeItem = new ToolStripMenuItem("Remove Launch");
            ToolStripMenuItem changeImageItem = new ToolStripMenuItem("Change Image");
            ToolStripMenuItem refreshCoverItem = new ToolStripMenuItem("Refresh Cover");

            contextMenu.Items.AddRange(new ToolStripItem[] { launchItem, removeItem, changeImageItem, refreshCoverItem });

            quickLaunchListView.ContextMenuStrip = contextMenu;

            launchItem.Click += LaunchItem_Click;
            removeItem.Click += RemoveItem_Click;
            changeImageItem.Click += ChangeImageItem_Click;
        }
        private void LaunchItem_Click(object sender, EventArgs e)
        {
            if (quickLaunchListView.SelectedItems.Count == 1)
            {
                ListViewItem item = quickLaunchListView.SelectedItems[0];
                QuickLaunchItem quickLaunchItem = item.Tag as QuickLaunchItem;
                if (quickLaunchItem != null)
                {
                    string xexPath = quickLaunchItem.LaunchPath;
                    string directoryPath = Path.GetDirectoryName(xexPath);

                    try
                    {
                        Form1.xbCon.Reboot(xexPath, directoryPath, null, XboxRebootFlags.Title);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to launch: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void RemoveItem_Click(object sender, EventArgs e)
        {
            if (quickLaunchListView.SelectedItems.Count == 1)
            {
                ListViewItem item = quickLaunchListView.SelectedItems[0];
                QuickLaunchItem quickLaunchItem = item.Tag as QuickLaunchItem;

                if (quickLaunchItem != null)
                {
                    quickLaunchListView.Items.Remove(item);

                    // Remove from INI file
                    if (File.Exists(iniFilePath))
                    {
                        IniData data = parser.ReadFile(iniFilePath);
                        data.Sections.RemoveSection(quickLaunchItem.GameName);
                        parser.WriteFile(iniFilePath, data);
                    }
                }
            }
        }
        private void ChangeImageItem_Click(object sender, EventArgs e)
        {
            if (quickLaunchListView.SelectedItems.Count == 1)
            {
                ListViewItem item = quickLaunchListView.SelectedItems[0];
                QuickLaunchItem quickLaunchItem = item.Tag as QuickLaunchItem;

                if (quickLaunchItem != null)
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string iconPath = openFileDialog.FileName;
                            quickLaunchItem.IconPath = iconPath;

                            // Update the image list with the new image if not already present
                            if (!imageList.Images.ContainsKey(iconPath))
                            {
                                imageList.Images.Add(iconPath, Image.FromFile(iconPath));
                            }

                            item.ImageIndex = imageList.Images.IndexOfKey(iconPath) != -1 ? imageList.Images.IndexOfKey(iconPath) : imageList.Images.IndexOfKey("default");

                            // Update the INI file with the new image path
                            if (File.Exists(iniFilePath))
                            {
                                IniData data = parser.ReadFile(iniFilePath);
                                if (data.Sections.ContainsSection(quickLaunchItem.GameName))
                                {
                                    data[quickLaunchItem.GameName]["IconPath"] = iconPath;
                                    parser.WriteFile(iniFilePath, data);
                                }
                            }

                            // Refresh the item to show the new image
                            quickLaunchListView.Invalidate();
                            quickLaunchListView.Update();
                        }
                    }
                }
            }
        }
        public void AddQuickLaunchItem(string gameName, string launchPath, string iconPath)
        {
            // Ensure the icon is loaded into the image list
            if (!string.IsNullOrEmpty(iconPath) && File.Exists(iconPath) && !imageList.Images.ContainsKey(iconPath))
            {
                imageList.Images.Add(iconPath, Image.FromFile(iconPath));
            }

            ListViewItem item = new ListViewItem(gameName)
            {
                Tag = new QuickLaunchItem { GameName = gameName, LaunchPath = launchPath, IconPath = iconPath },
                ImageIndex = !string.IsNullOrEmpty(iconPath) && File.Exists(iconPath) ? imageList.Images.IndexOfKey(iconPath) : imageList.Images.IndexOfKey("default")
            };

            quickLaunchListView.Items.Add(item);
            SaveQuickLaunchItem(gameName, launchPath, iconPath);
        }
        private void SaveQuickLaunchItem(string gameName, string launchPath, string iconPath)
        {
            IniData data = File.Exists(iniFilePath) ? parser.ReadFile(iniFilePath) : new IniData();

            if (!data.Sections.ContainsSection(gameName))
            {
                data.Sections.AddSection(gameName);
            }

            data[gameName]["LaunchPath"] = launchPath;
            data[gameName]["IconPath"] = iconPath;

            parser.WriteFile(iniFilePath, data);
        }
        private void LoadQuickLaunchItems()
        {
            if (!File.Exists(iniFilePath)) return;

            IniData data = parser.ReadFile(iniFilePath);
            foreach (var section in data.Sections)
            {
                string gameName = section.SectionName;
                string launchPath = section.Keys["LaunchPath"];
                string iconPath = section.Keys["IconPath"];

                // Ensure the icon is loaded into the image list
                if (!string.IsNullOrEmpty(iconPath) && File.Exists(iconPath) && !imageList.Images.ContainsKey(iconPath))
                {
                    imageList.Images.Add(iconPath, Image.FromFile(iconPath));
                }

                ListViewItem item = new ListViewItem(gameName)
                {
                    Tag = new QuickLaunchItem { GameName = gameName, LaunchPath = launchPath, IconPath = iconPath },
                    ImageIndex = !string.IsNullOrEmpty(iconPath) && File.Exists(iconPath) ? imageList.Images.IndexOfKey(iconPath) : imageList.Images.IndexOfKey("default")
                };

                quickLaunchListView.Items.Add(item);
            }
        }
        private void quickLaunchListView_DoubleClick(object sender, EventArgs e)
        {
            if (quickLaunchListView.SelectedItems.Count == 1)
            {
                ListViewItem item = quickLaunchListView.SelectedItems[0];
                QuickLaunchItem quickLaunchItem = item.Tag as QuickLaunchItem;
                if (quickLaunchItem != null)
                {
                    string xexPath = quickLaunchItem.LaunchPath;
                    string directoryPath = Path.GetDirectoryName(xexPath);

                    try
                    {
                        Form1.xbCon.Reboot(xexPath, directoryPath, null, XboxRebootFlags.Title);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to launch: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (FileExplorer fileExplorer = new FileExplorer(form1, this, gameId))
            {
                fileExplorer.ShowDialog();
            }
        }
    }
    public class QuickLaunchItem
    {
        public string GameName { get; set; }
        public string LaunchPath { get; set; }
        public string IconPath { get; set; }
    }
}