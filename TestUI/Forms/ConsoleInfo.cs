using System;
using System.Text;
using TestUI;
using XDevkit;
using XDRPC;

namespace DMONET3.Forms
{
    public partial class ConsoleInfo : DevExpress.XtraEditors.XtraForm
    {
        public Form1 form1;
        public ConsoleInfo()
        {
            InitializeComponent();
        }

        private void ConsoleInfo_Load(object sender, EventArgs e)
        {
            PullInfo();
        }
        static string ReverseIP(string ip)
        {
            string[] parts = ip.Split('.');
            Array.Reverse(parts);
            return string.Join(".", parts);
        }
        public void PullInfo()
        {
            string gamertag2 = Encoding.BigEndianUnicode.GetString(Form1.xbCon.ReadBytes(2175412476U, 30U)).Trim().Trim(new char[1]);
            gamertag.Text = Encoding.BigEndianUnicode.GetString(Form1.xbCon.ReadBytes(2175412476U, 30U)).Trim().Trim(new char[1]);
            CPUKeyText.Text = Form1.xbCon.GetCPUKey();
            IPText.Text = ReverseIP(Form1.xbCon.GetConsoleIP());
            KernalVersion.Text = "";
            ConsoleType.Text = Form1.xbCon.ConsoleType.ToString();
            pictureBox1.ImageLocation = "https://mygamerprofile.net/card/nxe/" + gamertag2 + ".png";
            pictureBox2.ImageLocation = "http://avatar.xboxlive.com/avatar/" + gamertag2 + "/avatar-body.png";
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PullInfo();
        }
    }
}