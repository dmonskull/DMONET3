using DevExpress.XtraEditors;
using IniParser.Model;
using IniParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestUI;
using XDevkit;

namespace DMONET3.Forms
{
    public partial class FileExplorer : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public string currentPath = null;
        public string currentPathLabel = null;
        private readonly string baseTitle = "File Explorer";
        private Stack<string> navigationStack = new Stack<string>();
        private ImageList imageList;
        private Dictionary<string, string> fileExtensionToImageKeyMap;
        private Form1 mainForm;
        private QuickLaunch quickLauncher;
        private bool isQuickLaunchSetup;
        private string gameId;
        public FileExplorer(Form1 mainForm, QuickLaunch quickLauncher, string gameId, bool isQuickLaunchSetup)
        {
            InitializeComponent();
            InitializeImageList();
            InitializeFileExtensionToImageKeyMap();
            this.quickLauncher = quickLauncher;
            this.gameId = gameId;
            this.mainForm = mainForm;
            this.gameId = gameId;
            this.isQuickLaunchSetup = isQuickLaunchSetup;
        }

        private void FileExplorer_Load(object sender, EventArgs e)
        {
            LoadAllDrives();
        }
        #region CoreFunctions
        private void InitializeImageList()
        {
            imageList = new ImageList();
            imageList.Images.Add("drive", Image.FromFile("Images/drive.png"));
            imageList.Images.Add("folder", Image.FromFile("Images/folder.png"));
            imageList.Images.Add("file", Image.FromFile("Images/file.png"));
            imageList.Images.Add("xex", Image.FromFile("Images/xex.png"));
            imageList.Images.Add("ini", Image.FromFile("Images/ini.png"));
            imageList.Images.Add("txt", Image.FromFile("Images/txt.png"));
            imageList.Images.Add("zip", Image.FromFile("Images/zip.png"));
            imageList.Images.Add("default", Image.FromFile("Images/default.png"));
            imageList.ImageSize = new Size(32, 32);

            listView1.LargeImageList = imageList;
            listView1.SmallImageList = imageList;
        }
        private void InitializeFileExtensionToImageKeyMap()
        {
            fileExtensionToImageKeyMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { ".xex", "xex" },
                { ".ini", "ini" },
                { ".txt", "txt" },
                { ".zip", "zip" }
            };
        }
        private void UpdateFormTitle()
        {
            this.Text = $"{baseTitle} [{currentPathLabel}]";
        }
        private void LoadAllDrives()
        {
            listView1.Items.Clear();

            try
            {
                List<string> drives = new List<string>(Form1.xbCon.Drives.Split(','));
                drives.ForEach(drive =>
                {
                    ListViewItem driveItem = new ListViewItem(drive + ":\\")
                    {
                        ImageIndex = 0 // Set appropriate image index for drive icon
                    };
                    listView1.Items.Add(driveItem);
                });

                // Update current path label
                currentPathLabel = "Drives";
                currentPath = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load drives: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDirectoryFiles(string path)
        {
            listView1.Items.Clear();
            currentPath = path;

            int folderCount = 0;
            int fileCount = 0;

            try
            {
                IXboxFiles files = Form1.xbCon.DirectoryFiles(path);
                var sortedFiles = files.Cast<IXboxFile>()
                                       .OrderBy(f => !f.IsDirectory)
                                       .ThenBy(f => f.Name)
                                       .ToList();

                foreach (IXboxFile file in sortedFiles)
                {
                    string displayName = file.Name.Replace(currentPath, "").TrimStart('\\');
                    string extension = Path.GetExtension(file.Name);
                    string imageKey = file.IsDirectory ? "folder" : (fileExtensionToImageKeyMap.TryGetValue(extension, out var key) ? key : "default");

                    listView1.Items.Add(new ListViewItem(displayName)
                    {
                        Tag = file,
                        ImageIndex = imageList.Images.IndexOfKey(imageKey)
                    });

                    if (file.IsDirectory)
                        folderCount++;
                    else
                        fileCount++;
                }

                currentPathLabel = path;
                statusBarItem.Caption = $"Currently viewing {folderCount} folders, and {fileCount} files!";
                UpdateFormTitle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load directory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SendToQuicklaunchForm()
        {
            var selectedItem = listView1.FocusedItem;
            if (selectedItem != null)
            {
                IXboxFile file = selectedItem.Tag as IXboxFile;
                if (file != null && file.Name.EndsWith(".xex", StringComparison.OrdinalIgnoreCase))
                {
                    string fullPath = Path.Combine(file.Name).Replace("\\\\", "\\"); // Ensure no redundant parts

                    using (AddQuickLaunch addQuickLaunchForm = new AddQuickLaunch(fullPath))
                    {
                        if (addQuickLaunchForm.ShowDialog() == DialogResult.OK)
                        {
                            quickLauncher.AddQuickLaunchItem(addQuickLaunchForm.GameName, addQuickLaunchForm.LaunchPath, addQuickLaunchForm.IconPath);
                        }
                    }
                }
            }
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                ListViewItem item = listView1.SelectedItems[0];
                string path = item.Text;

                // Check if it's a drive or directory and load accordingly
                if (path.EndsWith(":\\"))
                {
                    LoadDirectoryFiles(path);
                }
                else
                {
                    // Ensure the current path is correctly formatted without redundant drive names
                    string trimmedCurrentPath = currentPath.TrimEnd('\\');
                    string newPath = trimmedCurrentPath.EndsWith(":") ? $"{trimmedCurrentPath}\\{path}" : $"{trimmedCurrentPath}\\{path}";
                    newPath = newPath.Replace("\\\\", "\\");

                    // Check if the selected item is a .xex file
                    if (path.EndsWith(".xex", StringComparison.OrdinalIgnoreCase))
                    {
                        string xexPath = newPath;
                        string directoryPath = xexPath.Substring(0, xexPath.LastIndexOf(@"\", StringComparison.Ordinal));

                        // Show confirmation dialog
                        DialogResult result = XtraMessageBox.Show($"Do you want to launch {path}?", "Launch?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
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
                    else
                    {
                        LoadDirectoryFiles(newPath);
                    }
                }
            }
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && listView1.FocusedItem != null && listView1.FocusedItem.Bounds.Contains(e.Location))
            {
                var selectedFile = listView1.FocusedItem.Tag as IXboxFile;
                barButtonItemSetQuickLaunch.Enabled = selectedFile != null && selectedFile.Name.EndsWith(".xex", StringComparison.OrdinalIgnoreCase);
                popupMenu2.ShowPopup(Control.MousePosition);
            }
        }
        #endregion
        #region BarButtons
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentPath) && currentPath.Contains("\\"))
            {
                string newPath = Path.GetDirectoryName(currentPath.TrimEnd('\\')) + "\\";

                if (newPath.Length > 3)
                {
                    LoadDirectoryFiles(newPath);
                }
                else
                {
                    LoadAllDrives();
                }
            }
            else
            {
                LoadAllDrives();
            }
            UpdateFormTitle();
        }
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listView1.View = View.LargeIcon;
        }
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listView1.View = View.SmallIcon;
        }
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listView1.View = View.List;
        }
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listView1.View = View.Tile;
        }
        private void barButtonItemSetQuickLaunch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SendToQuicklaunchForm();
        }
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SendToQuicklaunchForm();
        }
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!isQuickLaunchSetup)
            {
                XtraMessageBox.Show("This operation is only allowed when setting up a main quick launch path.");
                return;
            }

            var selectedItem = listView1.FocusedItem;
            if (selectedItem != null)
            {
                IXboxFile file = selectedItem.Tag as IXboxFile;
                if (file != null && file.Name.EndsWith(".xex", StringComparison.OrdinalIgnoreCase))
                {
                    string fullPath = Path.Combine(file.Name).Replace("\\\\", "\\");

                    // Save to quicklaunch.ini
                    string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "INIs/Quicklaunch/quicklaunch.ini");
                    var parser = new FileIniDataParser();
                    IniData data = File.Exists(filePath) ? parser.ReadFile(filePath) : new IniData();

                    if (!data.Sections.ContainsSection("Games"))
                    {
                        data.Sections.AddSection("Games");
                    }

                    if (data["Games"].ContainsKey(gameId) && !string.IsNullOrWhiteSpace(data["Games"][gameId]))
                    {
                        XtraMessageBox.Show($"Launch path for {gameId} is already set. Please clear it before setting a new path.");
                        return;
                    }

                    data["Games"][gameId] = fullPath;
                    parser.WriteFile(filePath, data);

                    XtraMessageBox.Show($"Launch path for {gameId} has been set.");
                    this.Close();
                }
            }
        }
        #endregion

    }
}