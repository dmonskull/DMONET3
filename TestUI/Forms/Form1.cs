using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XDRPC;
using XDevkit;
using DevExpress.XtraBars;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using IniParser;
using IniParser.Model;
using TestUI.Forms;
using DMONET3.Forms;
using System.Threading.Tasks;

namespace TestUI // Made by DMONSKULL
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        public Timer timer;
        public Form1()
        {
            InitializeComponent();
        }
        #region FormStuff
        private void Form1_Load(object sender, EventArgs e)
        {
            DialogResult result = XtraMessageBox.Show("Do you want to connect to the console?", "Connect to Console", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (ConnectToConsole())
                {
                    barButtonItem1.Caption = "Reconnect";
                    barStaticItem3.Caption = "Playing: " + GetCurrentTitleName() + "\tTitle ID: " + GetCurrentTitleId() + "\tRunning Path: " + xbCon.RunningProcessInfo.ProgramName.ToString();
                    ribbonControl1.ApplicationButtonText = Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).All(b => b == 0) || Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).Any(c => c > 127) ? "unknown" : Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).Trim().Trim(new char[1]);
                    InfoUpdater();
                }
            }
            else
            {

            }
        }
        public void InfoUpdater()
        {
            timer = new Timer();
            timer.Interval = 5 * 60 * 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            string gameName = GetCurrentTitleName();
            string gameId = GetCurrentTitleId();
            barStaticItem3.Caption = "Playing: " + GetCurrentTitleName() + "\tTitle ID: " + GetCurrentTitleId() + "\tRunning Path: " + xbCon.RunningProcessInfo.ProgramName.ToString();
            ribbonControl1.ApplicationButtonText = Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U))
                .All(b => b == 0) ? "unknown" : Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).Trim().Trim(new char[1]);
        }
        #endregion

        #region XboxHelperStuff
        public bool ConnectToConsole()
        {
            if (!activeConnection)
            {
                xbManager = (XboxManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                ConnectionCode = xbCon.OpenConnection(null);
                try
                {
                    xboxConnection = xbCon.OpenConnection(null);
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Could not connect to console: " + xbManager.DefaultConsole);
                    return false;
                }
                if (xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    activeConnection = true;
                    XtraMessageBox.Show("Connection to " + xbCon.Name + " established!");
                    return true;
                }
                xbCon.DebugTarget.ConnectAsDebugger("Xbox Toolbox", XboxDebugConnectFlags.Force);
                if (!xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    XtraMessageBox.Show("Attempted to connect to console: " + xbCon.Name + " but failed");
                    return false;
                }
                activeConnection = true;
                XtraMessageBox.Show("Connection to " + xbCon.Name + " established!");
                return true;
            }
            if (xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
            {
                XtraMessageBox.Show("Connection to " + xbCon.Name + " already established!");
                return true;
            }
            activeConnection = false;
            return ConnectToConsole();
        }
        public string GetCurrentTitleId()
        {
            uint currentTitleId = xbCon.GetCurrentTitleId();
            string hexTitleId = "0x" + currentTitleId.ToString("X8");
            return hexTitleId;
        }
        public string GetCurrentTitleName()
        {
            Dictionary<uint, string> gameDictionary = new Dictionary<uint, string>()
            {
                {0xFFFE07D1, "Xbox 360 Dashboard"},
                {0x4D5307DC, "Crackdown"},
                {0x4343081C, "Resident Evil 4"},
                {0x454108E6, "Skate 3"},
                {0x4343081F, "Dead Rising 2: OTR"},
            };
            uint currentTitleIdNumeric = xbCon.GetCurrentTitleId();
            if (gameDictionary.ContainsKey(currentTitleIdNumeric))
            {
                return gameDictionary[currentTitleIdNumeric];
            }
            else
            {
                return "Unknown Game";
            }
        }
        public void LaunchGameFromIni(string gameId, string gameFolder)
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "settings.ini");
            var parser = new FileIniDataParser();
            try
            {
                IniData data = parser.ReadFile(filePath);
                if (gameId != null && gameFolder != null)
                {
                    if (data.Sections.ContainsSection("games"))
                    {
                        foreach (KeyData key in data["games"])
                        {
                            if (key.KeyName.Equals(gameId, StringComparison.OrdinalIgnoreCase))
                            {
                                string gameName = key.Value;
                                try
                                {
                                    var xexPath = $@"Hdd:\{gameFolder}\{gameName}\default.xex";
                                    var directoryPath = xexPath.Substring(0, xexPath.LastIndexOf(@"\", StringComparison.Ordinal));
                                    xbCon.Reboot(xexPath, directoryPath, null, XboxRebootFlags.Title);
                                    return;
                                }
                                catch (Exception)
                                {
                                    XtraMessageBox.Show("Please connect to the console first");
                                    return;
                                }
                            }
                        }
                        XtraMessageBox.Show($"Game ID '{gameId}' not found in the INI file.");
                    }
                    else
                    {
                        XtraMessageBox.Show("No games found in the INI file.");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Game ID or game folder is missing.");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"An error occurred while reading the INI file: {ex.Message}");
            }
        }
        #endregion

        #region BarTools
        private void barButtonItem1_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            if (ConnectToConsole())
            {
                barButtonItem1.Caption = "Reconnect";
                barStaticItem1.Caption = "Playing: " + GetCurrentTitleName() + "\tTitle ID: " + GetCurrentTitleId() + "\tRunning Path: " + xbCon.RunningProcessInfo.ProgramName.ToString();
            }
        }
        private void barButtonItem2_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            try
            {
                xbCon.Reboot();
            }
            catch
            {
                XtraMessageBox.Show("Unable to reach console");
            }
        }
        private async void barStaticItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clipboard.SetText(barStaticItem3.Caption.Replace("\t", "\n"));
            string originalText = barStaticItem4.Caption;
            barStaticItem4.Caption = "Copied";
            await Task.Delay(1000);
            barStaticItem4.Caption = originalText;
        }
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            QuickLaunchEditor quickLaunchEditor = new QuickLaunchEditor();
            quickLaunchEditor.Show();
        }
        #endregion

        #region TileStuffandGameLaunching
        // Game and Other Tools
        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            Crackdown crackdown = new Crackdown();
            crackdown.Show();
        }
        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            ResidentEvil4 residentevil4 = new ResidentEvil4();
            residentevil4.Show();
        }
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            Skate3 skate3 = new Skate3();
            skate3.Show();
        }
        private void tileItem6_ItemClick(object sender, TileItemEventArgs e)
        {
            DeadRising2OTF deadrising2otf = new DeadRising2OTF();
            deadrising2otf.Show();
        }
        private void tileItem16_ItemClick(object sender, TileItemEventArgs e)
        {
            ConsoleInfo consoleinfo = new ConsoleInfo();
            consoleinfo.Show();
        }
        private void tileItem9_ItemClick(object sender, TileItemEventArgs e)
        {
            INIEditor inieditor = new INIEditor();
            inieditor.Show();
        }
        private void tileItem10_ItemClick(object sender, TileItemEventArgs e)
        {
            Neighborhood neighborhood = new Neighborhood();
            neighborhood.Show();
        }
        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            SettingsPage settingsPage = new SettingsPage();
            settingsPage.Show();
        }
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            ScreenshotPage screenshotPage = new ScreenshotPage();
            screenshotPage.Show();
        }
        //Launching Games
        private void tileItem3_RightItemClick(object sender, TileItemEventArgs e)
        {
            LaunchGameFromIni("Crackdown", "Games");
        }
        private void tileItem2_RightItemClick(object sender, TileItemEventArgs e)
        {
            LaunchGameFromIni("RE4", "Games");
        }
        private void tileItem4_RightItemClick(object sender, TileItemEventArgs e)
        {
            LaunchGameFromIni("Skate3", "Games");
        }
        private void tileItem6_RightItemClick(object sender, TileItemEventArgs e)
        {
            LaunchGameFromIni("DeadRising2OTF", "Games");
        }
        #endregion

    }

}
