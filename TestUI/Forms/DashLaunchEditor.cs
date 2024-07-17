using DevExpress.XtraEditors;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using XDevkit;

namespace DMONET3.Forms
{
    public partial class DashLaunchEditor : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        private IXboxManager xbManager = null;
        private IXboxConsole xbCon = null;
        private bool activeConnection = false;
        private string debuggerName = null;
        private string userName = null;

        private readonly Dictionary<string, ToggleSwitch> settingToToggleSwitchMap = new Dictionary<string, ToggleSwitch>();
        private IniData data;

        public DashLaunchEditor()
        {
            InitializeComponent();
        }

        private void DashLaunchEditor_Load(object sender, EventArgs e)
        {
            try
            {
                ConnectToConsole2();
                LoadSettings();
            }
            catch { }
        }
        private bool ConnectToConsole2()
        {
            if (activeConnection && xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                return true;

            try
            {
                xbManager = (XboxManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                xbCon.OpenConnection(null);

                if (!xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                    xbCon.DebugTarget.ConnectAsDebugger("Xbox Toolbox", XboxDebugConnectFlags.Force);

                activeConnection = xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName);
                return activeConnection;
            }
            catch
            {
                XtraMessageBox.Show("Could not connect to console: " + xbManager.DefaultConsole);
                return false;
            }
        }
        private void LoadSettings()
        {
            string iniFilePath = AppDomain.CurrentDomain.BaseDirectory + "launch.ini";
            xbCon.ReceiveFile(iniFilePath, "Hdd:\\launch.ini");

            var parser = new FileIniDataParser();
            data = parser.ReadFile(iniFilePath);

            var settingsSection = data["Settings"];
            int x = 20, y = 20;
            int verticalSpacing = 45, horizontalSpacing = 145;
            int maxWidth = 0, maxHeight = 0;

            foreach (var setting in settingsSection)
            {
                var label = new LabelControl
                {
                    Location = new Point(x, y),
                    Text = setting.KeyName,
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Width = 125
                };

                var toggleSwitch = new ToggleSwitch
                {
                    Location = new Point(x, y + 20),
                    Name = setting.KeyName + "ToggleSwitch",
                    IsOn = setting.Value.Equals("true", StringComparison.OrdinalIgnoreCase),
                    Width = 125,
                };

                toggleSwitch.Toggled += (s, ev) => SaveSettings();

                Controls.Add(label);
                Controls.Add(toggleSwitch);
                settingToToggleSwitchMap[setting.KeyName] = toggleSwitch;

                y += verticalSpacing;
                maxWidth = Math.Max(maxWidth, x + horizontalSpacing);
                maxHeight = Math.Max(maxHeight, y + verticalSpacing);
                if (y > this.ClientSize.Height - 100)
                {
                    y = 20;
                    x += horizontalSpacing;
                }
            }

            this.ClientSize = new Size(maxWidth + 40, maxHeight + 40);
        }
        private void SaveSettings()
        {
            foreach (var entry in settingToToggleSwitchMap)
                data["Settings"][entry.Key] = entry.Value.IsOn ? "true" : "false";

            string iniFilePath = AppDomain.CurrentDomain.BaseDirectory + "launch.ini";
            var parser = new FileIniDataParser();
            parser.WriteFile(iniFilePath, data);

            xbCon.SendFile(iniFilePath, "Hdd:\\launch.ini");
        }
    }
}
