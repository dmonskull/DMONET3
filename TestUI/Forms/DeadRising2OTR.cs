using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestUI;
using XDevkit;
using XDRPC;

namespace DMONET3.Forms
{
    public partial class DeadRising2OTF : DevExpress.XtraEditors.XtraForm
    {
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        public Form1 form1;

        public bool timergodmode;
        public bool money;
        public bool level;
        public bool health;
        public bool healthbars;
        public bool fastrun;
        public uint Levels = 3352533171U;
        public uint money2 = 3352533248U;
        public DeadRising2OTF()
        {
            InitializeComponent();
        }

        private void DeadRising2OTF_Load(object sender, EventArgs e)
        {

        }
        public bool ConnectToConsole2()
        {
            if (activeConnection && Form1.xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
            {
                return true;
            }
            try
            {
                xbManager = (XboxManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                Form1.xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                ConnectionCode = Form1.xbCon.OpenConnection(null);
                xboxConnection = Form1.xbCon.OpenConnection(null);

                if (!Form1.xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    Form1.xbCon.DebugTarget.ConnectAsDebugger("DMONET", XboxDebugConnectFlags.Force);
                }

                activeConnection = Form1.xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName);
                return activeConnection;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Could not connect to console: " + xbManager.DefaultConsole);
                return false;
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            byte test = Convert.ToByte(numericUpDown1.Value);
            byte[] test2 = BitConverter.GetBytes(test);
            Form1.xbCon.WriteBytes(Levels, test2);
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            simpleButton6.ForeColor = timergodmode ? Color.Red : Color.Green;
            if (!timergodmode)
            {
                Form1.xbCon.WriteBytes(0xC7D398EF, new byte[] { 0x0D });
                Form1.xbCon.WriteBytes(0xC7D398BC, new byte[] { 0x44, 0xBB, 0x80 });
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
            timergodmode = !timergodmode;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            simpleButton2.ForeColor = level ? Color.Red : Color.Green;
            byte[] levelData = level ? new byte[] { 0x00, 0x00, 0x00, 0x0A } : new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            Form1.xbCon.WriteBytes(0xC7D398B0, levelData);
            level = !level;
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            simpleButton3.ForeColor = money ? Color.Red : Color.Green;
            byte[] moneyData = money ? new byte[] { 0x00, 0x00, 0x00, 0x01 } : new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            Form1.xbCon.WriteBytes(money2, moneyData);
            money = !money;
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            simpleButton4.ForeColor = health ? Color.Red : Color.Green;
            byte[] healthData = health ? new byte[] { 0x42, 0x67, 0x00 } : new byte[] { 0x44, 0xBB, 0x80 };
            Form1.xbCon.WriteBytes(0xC7D398BC, healthData);
            health = !health;
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            simpleButton5.ForeColor = healthbars ? Color.Red : Color.Green;
            byte[] healthBarsData = healthbars ? new byte[] { 0x04 } : new byte[] { 0x0D };
            Form1.xbCon.WriteBytes(0xC7D398EF, healthBarsData);
            healthbars = !healthbars;
        }
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            simpleButton7.ForeColor = fastrun ? Color.Red : Color.Green;
            byte[] fastRunData = fastrun ? new byte[] { 0x042 } : new byte[] { 0x52 };
            Form1.xbCon.WriteBytes(0x82B388B0, fastRunData);
            fastrun = !fastrun;
        }
    }
}