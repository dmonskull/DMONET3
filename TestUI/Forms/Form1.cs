using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DMONET3.Forms;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestUI.Forms;
using XDevkit;
using XDRPC;

namespace TestUI // Made by DMONSKULL
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public static IXboxManager xbManager = null;
        public static IXboxConsole xbCon = null;
        public static bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        public Timer timer;
        private readonly string iniFilePath = AppDomain.CurrentDomain.BaseDirectory + "INIs/Settings/settings.ini";
        private IniData data;
        private QuickLaunch quickLauncher;

        public Form1()
        {
            InitializeComponent();
            LoadUserSettings();
        }

        #region FormStuff
        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialization code if needed
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
            barStaticItem3.Caption = "Playing: " + gameName + "\tTitle ID: " + gameId + "\tRunning Path: " + xbCon.RunningProcessInfo.ProgramName;
            ribbonControl1.ApplicationButtonText = Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U))
                .All(b => b == 0) ? "unknown" : Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).Trim().Trim(new char[1]);
        }

        private void LoadUserSettings()
        {
            var parser = new FileIniDataParser();
            data = parser.ReadFile(iniFilePath);
            if (!data["DMONSETTINGS"].ContainsKey("AutoConnect") || string.IsNullOrWhiteSpace(data["DMONSETTINGS"]["AutoConnect"]))
            {
                var result = XtraMessageBox.Show("Do you want to enable Auto Connect?", "DMONET", MessageBoxButtons.YesNo);
                data["DMONSETTINGS"]["AutoConnect"] = result == DialogResult.Yes ? "True" : "False";
                SaveSettings();
            }

            if (bool.TryParse(data["DMONSETTINGS"]["AutoConnect"], out bool autoConnect) && autoConnect)
            {
                if (ConnectToConsole())
                {
                    PullXboxUserInfo();
                }
            }
        }

        private void SaveSettings()
        {
            new FileIniDataParser().WriteFile(iniFilePath, data);
        }
        #endregion
        #region XboxHelperStuff
        public static bool ConnectToConsole()
        {
            if (activeConnection && xbCon.DebugTarget.IsDebuggerConnected(out var debuggerName, out var userName))
            {
                XtraMessageBox.Show("Connection to " + xbCon.Name + " already established!");
                return true;
            }
            try
            {
                xbManager = (XboxManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                var ConnectionCode = xbCon.OpenConnection(null);
                var xboxConnection = xbCon.OpenConnection(null);

                if (!xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    xbCon.DebugTarget.ConnectAsDebugger("Xbox Toolbox", XboxDebugConnectFlags.Force);
                }

                activeConnection = xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName);
                XtraMessageBox.Show(activeConnection ? "Connection to " + xbCon.Name + " established!" : "Attempted to connect to console: " + xbCon.Name + " but failed");
                return activeConnection;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Could not connect to console: " + xbManager.DefaultConsole);
                return false;
            }
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
                {0x4D5308BC, "Crackdown 2"},
                {0x4343081C, "Resident Evil 4"},
                {0x454108E6, "Skate 3"},
                {0x4343081F, "Dead Rising 2: OTR"},
                {0x465307E4, "Dark Souls 2"},
                {0x4D5307E6, "Halo 3"},
                {0x545107D1, "Saints Row"}
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
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "INIs/Quicklaunch/quicklaunch.ini");
            var parser = new FileIniDataParser();
            try
            {
                IniData data = parser.ReadFile(filePath);
                if (gameId != null && gameFolder != null)
                {
                    // Check for game-specific paths
                    if (data["Games"].ContainsKey(gameId))
                    {
                        string launchPath = data["Games"][gameId];
                        if (string.IsNullOrWhiteSpace(launchPath))
                        {
                            DialogResult result = XtraMessageBox.Show("The launch path is not set. Would you like to set it up?", "Setup Launch Path", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                FileExplorer fileExplorer = new FileExplorer(this, quickLauncher ,gameId);
                                fileExplorer.Show();
                            }
                            return;
                        }

                        try
                        {
                            string directoryPath = Path.GetDirectoryName(launchPath);
                            xbCon.Reboot(launchPath, directoryPath, null, XboxRebootFlags.Title);
                            return;
                        }
                        catch (Exception)
                        {
                            XtraMessageBox.Show("Please connect to the console first");
                            return;
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show($"Game ID '{gameId}' not found in the INI file.");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Game ID or Game Path is missing.");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"An error occurred while reading the INI file: {ex.Message}");
            }
        }
        public void PullXboxUserInfo()
        {
            try
            {
                barButtonItem1.Caption = "Reconnect";
                barStaticItem3.Caption = "Playing: " + GetCurrentTitleName() + "\tTitle ID: " + GetCurrentTitleId() + "\tRunning Path: " + xbCon.RunningProcessInfo.ProgramName.ToString();
                ribbonControl1.ApplicationButtonText = Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).All(b => b == 0) || Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).Any(c => c > 127) ? "unknown" : Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).Trim().Trim(new char[1]);
                InfoUpdater();
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region BarTools
        private void barButtonItem1_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            if (ConnectToConsole())
            {
                PullXboxUserInfo();
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
            QuickLaunch quickLaunch = new QuickLaunch();
            quickLaunch.Show();
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

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string gamesFolderName = new FileIniDataParser().ReadFile("INIs/Settings/settings.ini")["game folder"]["Games"];
                string Dir = xbCon.DebugTarget.RunningProcessInfo.ProgramName;
                int index = Dir.IndexOf(@"\" + gamesFolderName, StringComparison.OrdinalIgnoreCase);
                if (index >= 0)
                {
                    string xexPath = @"Hdd:\" + Dir.Substring(index + 1);
                    xbCon.Reboot(xexPath, xexPath.Substring(0, xexPath.LastIndexOf(@"\", StringComparison.Ordinal)), null, XboxRebootFlags.Title);
                }
                else
                {
                    MessageBox.Show($"Directory does not contain '{gamesFolderName}'.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("please connect to console first");
            }
        }
        #endregion
        #region TileStuffandGameLaunching
        // Game Tools
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
            DeadRising2OTF deadrising2otr = new DeadRising2OTF();
            deadrising2otr.Show();
        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            DarkSouls2 darkSouls2 = new DarkSouls2();
            darkSouls2.Show();
        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            Halo3 halo3 = new Halo3();
            halo3.Show();
        }

        private void tileItem7_ItemClick(object sender, TileItemEventArgs e)
        {
            SaintsRow saintsRow = new SaintsRow();
            saintsRow.Show();
        }

        private void tileItem8_ItemClick(object sender, TileItemEventArgs e)
        {
            Crackdown2 crackdown2 = new Crackdown2();
            crackdown2.Show();
        }

        // Other Tools
        private void tileItem16_ItemClick(object sender, TileItemEventArgs e)
        {
            ConsoleInfo consoleinfo = new ConsoleInfo();
            consoleinfo.Show();
        }

        private void tileItem14_ItemClick(object sender, TileItemEventArgs e)
        {
            ContentInject contentInject = new ContentInject();
            contentInject.Show();
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

        private void tileItem11_ItemClick(object sender, TileItemEventArgs e)
        {
            PeekAndPoke peekAndPoke = new PeekAndPoke();
            peekAndPoke.Show();
        }

        private void tileItem12_ItemClick(object sender, TileItemEventArgs e)
        {
            DashLaunchEditor editor = new DashLaunchEditor();
            editor.Show();
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

        private void tileItem5_RightItemClick(object sender, TileItemEventArgs e)
        {
            LaunchGameFromIni("DarkSouls2", "Games");
        }

        private void tileItem1_RightItemClick(object sender, TileItemEventArgs e)
        {
            LaunchGameFromIni("Halo3", "Games");
        }

        private void tileItem7_RightItemClick(object sender, TileItemEventArgs e)
        {
            LaunchGameFromIni("SaintsRow", "Games");
        }

        private void tileItem8_RightItemClick(object sender, TileItemEventArgs e)
        {
            LaunchGameFromIni("Crackdown2", "Games");
        }
        #endregion
    }
}
