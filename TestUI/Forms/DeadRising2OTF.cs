﻿using DevExpress.XtraEditors;
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

        public bool timergodmode;
        public bool money;
        public bool level;
        public bool health;
        public bool healthbars;
        public uint Levels = 3352533171U;
        public uint money2 = 3352533248U;
        public DeadRising2OTF()
        {
            InitializeComponent();
        }

        private void DeadRising2OTF_Load(object sender, EventArgs e)
        {
            try
            {
                ConnectToConsole2();
            }
            catch { }
        }
        public bool ConnectToConsole2()
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
                    return true;
                }
                xbCon.DebugTarget.ConnectAsDebugger("Xbox Toolbox", XboxDebugConnectFlags.Force);
                if (!xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    XtraMessageBox.Show("Attempted to connect to console: " + xbCon.Name + " but failed");
                    return false;
                }
                activeConnection = true;
                return true;
            }
            if (xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
            {
                return true;
            }
            activeConnection = false;
            return ConnectToConsole2();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            byte test = Convert.ToByte(numericUpDown1.Value);
            byte[] test2 = BitConverter.GetBytes(test);
            xbCon.WriteBytes(Levels, test2);
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (!timergodmode)
            {
                simpleButton6.ForeColor = Color.Green;
                xbCon.WriteBytes(0xC7D398EF, new byte[] { 0x0D });
                xbCon.WriteBytes(0xC7D398BC, new byte[] { 0x44, 0xBB, 0x80 });
                timer1.Start();
            }
            else
            {
                simpleButton6.ForeColor = Color.Red;
                timer1.Stop();
            }
            timergodmode = !timergodmode;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (!level)
            {
                simpleButton2.ForeColor = Color.Green;
                xbCon.WriteBytes(0xC7D398B0, new byte[] { 0x0F, 0xFF, 0xFF, 0xFF });
            }
            else
            {
                simpleButton2.ForeColor = Color.Red;
                xbCon.WriteBytes(0xC7D398B0, new byte[] { 0x00, 0x00, 0x00, 0x0A });
            }
            level = !level;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (!money)
            {
                simpleButton3.ForeColor = Color.Green;
                xbCon.WriteBytes(money2, new byte[] { 0x0F, 0xFF, 0xFF, 0xFF });
            }
            else
            {
                simpleButton3.ForeColor = Color.Red;
                xbCon.WriteBytes(money2, new byte[] { 0x00, 0x00, 0x00, 0x01 });
            }
            money = !money;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (!health)
            {
                simpleButton4.ForeColor = Color.Green;
                xbCon.WriteBytes(0xC7D398BC, new byte[] { 0x44, 0xBB, 0x80 });
            }
            else
            {
                simpleButton4.ForeColor = Color.Red;
                xbCon.WriteBytes(0xC7D398BC, new byte[] { 0x42, 0x67, 0x00 });
            }
            health = !health;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (!healthbars)
            {
                simpleButton5.ForeColor = Color.Green;
                xbCon.WriteBytes(0xC7D398EF, new byte[] { 0x0D });
            }
            else
            {
                simpleButton5.ForeColor = Color.Red;
                xbCon.WriteBytes(0xC7D398EF, new byte[] { 0x04 });
            }
            healthbars = !healthbars;
        }
    }
}