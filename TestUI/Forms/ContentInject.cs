using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ContentInjector;
using XDevkit;
using XDRPC;
using TestUI;

namespace DMONET3.Forms
{
    public partial class ContentInject : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        public Form1 form1;
        public ContentInject()
        {
            InitializeComponent();
            MainScreen();
            if (Globals.isConnected == true)
            {
                this.WorkingScreen();
                this.EXEDrop();
            }
        }
        private void ContentInject_Load(object sender, EventArgs e)
        {

        }

        #region Core Functions
        private void EXEDrop()
        {
            Globals.isConnected = true;
            MultiFiles();
        }
        private void MultiFiles()
        {
            while (backgroundWorker1.IsBusy)
            {
                Thread.Sleep(100);
            }
            backgroundWorker1.RunWorkerAsync();
        }
        private void WorkingScreen()
        {
            base.Height = 381;
            string[] array = new string[]
            {
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            };
            Globals.Failed = false;
            richTextBox1.Clear();
            array[0] = "--------------------------------------------------------------------------------";
            array[1] = "";
            array[2] = "";
            array[3] = "";
            array[4] = "";
            array[5] = "";
            array[6] = "";
            array[7] = "";
            array[8] = "";
            array[9] = "--------------------------------------------------------------------------------";
            array[10] = "";
            array[11] = "";
            array[12] = "";
            array[13] = "";
            array[14] = "--------------------------------------------------------------------------------";
            array[15] = "";
            array[16] = "";
            array[17] = "";
            array[18] = "--------------------------------------------------------------------------------";
            richTextBox1.Lines = array;
        }
        private void MainScreen()
        {
            base.Height = 381;
            Globals.AtMS = true;
            Globals.Back2MS = false;
            string[] array = new string[]
            {
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
            };
            array[0] = "--------------------------------------------------------------------------------";
            array[1] = "Simply Drag and Drop any Xbox 360 Content, and it will be installed to";
            array[2] = "its appropriate location.  If file already exists on console, it will be";
            array[3] = "replaced.  To skip this screen, drop file directly onto exe.";
            array[4] = "This tool requires XBDM and uses JRPC2 for xNotifys(Not Required)";
            array[5] = "This tool has Auto-Update functionality,  if I ever push an Update.";
            array[6] = "--------------------------------------------------------------------------------";
            array[7] = "Supports:\tAccounts/Profiles\tGames on Demand";
            array[8] = "\t\tAvatar Items\t\tGameSaves";
            array[9] = "\t\tArcade Games\t\tIndie Games";
            array[10] = "\t\tDemos\t\t\tSystems Items";
            array[11] = "\t\tDLC\t\t\tTitle Updates";
            array[12] = "\t\tGamerPics    \t\tThemes";
            array[13] = "--------------------------------------------------------------------------------";
            array[14] = "";
            array[15] = "";
            array[16] = "";
            array[17] = "--------------------------------------------------------------------------------";
            array[18] = "\t\t     Credits to Cliftonm1996";
            array[19] = "--------------------------------------------------------------------------------";
            array[22] = "--------------------------------------------------------------------------------";
            richTextBox1.Lines = array;
        }
        public void Connecting()
        {
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                Globals.changeLine(richTextBox1, 16, "Status: Attempting to Connect to Console.");
            }));
            while (Globals.Connecting)
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 11, "Connecting to Console..");
                }));
                Thread.Sleep(150);
                if (!Globals.Connecting)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 11, "Connecting to Console...");
                }));
                Thread.Sleep(150);
                if (!Globals.Connecting)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 11, "Connecting to Console..");
                }));
                Thread.Sleep(150);
                if (!Globals.Connecting)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 11, "Connecting to Console.");
                }));
                Thread.Sleep(150);
                if (!Globals.Connecting)
                {
                    break;
                }
            }
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                Globals.changeLine(richTextBox1, 11, "Connection to Console established.");
                if (Globals.Failed)
                {
                    Globals.changeLine(richTextBox1, 11, "Connection to Console Failed!");
                    Globals.changeLine(richTextBox1, 16, "Status: Failed to Connect to console!");
                }
            }));
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
        public void Processing()
        {
            while (Globals.Pushing)
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 12, Globals.PushingtoConsole + ".");
                }));
                Thread.Sleep(300);
                if (!Globals.Pushing)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 12, Globals.PushingtoConsole + "..");
                }));
                Thread.Sleep(300);
                if (!Globals.Pushing)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 12, Globals.PushingtoConsole + "...");
                }));
                Thread.Sleep(300);
                if (!Globals.Pushing)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 12, Globals.PushingtoConsole + "....");
                }));
                Thread.Sleep(300);
                if (!Globals.Pushing)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 12, Globals.PushingtoConsole + ".....");
                }));
                Thread.Sleep(300);
                if (!Globals.Pushing)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 12, Globals.PushingtoConsole + "....");
                }));
                Thread.Sleep(300);
                if (!Globals.Pushing)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 12, Globals.PushingtoConsole + "...");
                }));
                Thread.Sleep(300);
                if (!Globals.Pushing)
                {
                    break;
                }
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 12, Globals.PushingtoConsole + "..");
                }));
                Thread.Sleep(300);
                if (!Globals.Pushing)
                {
                    break;
                }
            }
            Globals.sw.Stop();
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                Globals.changeLine(richTextBox1, 12, "Pushed to Console!");
                Globals.changeLine(richTextBox1, 15, "Finished in " + Globals.sw.Elapsed.ToString("mm' min. 'ss'.'ff").Replace("00 min. 0", "").Replace("00 min. ", "") + " sec.");
                Globals.changeLine(richTextBox1, 16, "Drop another file or Press any key to close.");
                Globals.changeLine(richTextBox1, 17, "Press \"B\" to go back to the Main Screen.");
                Globals.Back2MS = true;
            }));
        }
        private void DeterminePath()
        {
            Globals.consolePath = Path.Combine(new string[]
            {
                string.Concat(new string[]
                {
                    "HDD:\\Content\\",
                    Globals.ProfileId,
                    "\\",
                    Globals.TitleId,
                    "\\",
                    Globals.Type,
                    "\\"
                })
            });
            Globals.consoleFile = Path.Combine(new string[]
            {
                string.Concat(new string[]
                {
                    "HDD:\\Content\\",
                    Globals.ProfileId,
                    "\\",
                    Globals.TitleId,
                    "\\",
                    Globals.Type,
                    "\\",
                    Path.GetFileName(Globals.file)
                })
            });
        }
        private void DetermineInfo()
        {
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(Globals.file, FileMode.Open)))
            {
                binaryReader.BaseStream.Position = 1042L;
                Globals.Fileinfo = Globals.Hex2Text(BitConverter.ToString(binaryReader.ReadBytes(255)).Replace("-", null).Replace("00", null));
            }
            if (Globals.Fileinfo == string.Empty)
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 3, "Display Name: (Blank)");
                }));
                return;
            }
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                Globals.changeLine(richTextBox1, 3, "Display Name: " + Globals.Fileinfo);
            }));
        }
        private void DetermineName()
        {
            if (Globals.isConnected)
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 11, "Connection to Console established.");
                }));
            }
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(Globals.file, FileMode.Open)))
            {
                binaryReader.BaseStream.Position = 5778L;
                Globals.Filename = Globals.Hex2Text(BitConverter.ToString(binaryReader.ReadBytes(126)).Replace("-", null).Replace("00", null));
            }
            if (Globals.Filename == string.Empty)
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 2, "Title Name: (Blank)");
                }));
            }
            else
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 2, "Title Name: " + Globals.Filename);
                }));
            }
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                Globals.changeLine(richTextBox1, 16, "Status: Getting File Information.");
            }));
        }
        private void DetermineProfileId()
        {
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(Globals.file, FileMode.Open)))
            {
                binaryReader.BaseStream.Position = 881L;
                Globals.ProfileId = BitConverter.ToString(binaryReader.ReadBytes(8)).Replace("-", null);
            }
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                Globals.changeLine(richTextBox1, 6, "Profile Id: " + Globals.ProfileId);
            }));
        }
        private void DetermineType()
        {
            string str = "";
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(Globals.file, FileMode.Open)))
            {
                binaryReader.BaseStream.Position = 836L;
                Globals.Type = BitConverter.ToString(binaryReader.ReadBytes(4)).Replace("-", null);
            }
            if (Globals.Type == "00000002")
            {
                str = "File Type: DLC";
            }
            else if (Globals.Type == "000B0000")
            {
                str = "File Type: Title Update";
            }
            else if (Globals.Type == "00009000")
            {
                str = "File Type: Avatar Item";
            }
            else if (Globals.Type == "00010000")
            {
                str = "File Type: Profile";
            }
            else if (Globals.Type == "00000001")
            {
                str = "File Type: GameSave";
            }
            else if (Globals.Type == "00020000")
            {
                str = "File Type: GamerPic";
                Globals.TitleId = "FFFE07D1";
                Globals.isGamerPic = true;
            }
            else if (Globals.Type == "00030000")
            {
                str = "File Type: Theme";
            }
            else if (Globals.Type == "00040000")
            {
                str = "File Type: System Item";
                Globals.TitleId = "FFFE07DF";
                Globals.isGamerPic = true;
            }
            else if (Globals.Type == "00080000")
            {
                str = "File Type: Demo";
            }
            else if (Globals.Type == "000D0000")
            {
                str = "File Type: Arcade";
            }
            else if (Globals.Type == "00007000")
            {
                str = "File Type: Game on Demand";
                Globals.isGOD = true;
            }
            else
            {
                Globals.Failed = true;
                str = "Failed to determine Xbox 360 file Type.    Please report this to me so i can add it.";
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 16, "Status: Failed! Unknown Xbox 360 file type.");
                }));
            }
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                Globals.changeLine(richTextBox1, 4, str);
            }));
        }
        private void DetermineTitleId()
        {
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                Globals.lines = richTextBox1.Lines;
            }));
            string titleId = Globals.TitleId;
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(Globals.file, FileMode.Open)))
            {
                binaryReader.BaseStream.Position = 864L;
                if (!Globals.isGamerPic)
                {
                    Globals.TitleId = BitConverter.ToString(binaryReader.ReadBytes(4)).Replace("-", null);
                }
            }
            if (titleId == "FFFE07D1" || titleId == "FFFE07DF")
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 5, "Title Id: 00000000.   Is " + Globals.lines[4].Replace("File Type: ", "") + ", Install to " + Globals.TitleId);
                }));
            }
            else if (titleId != "FFFE07D1" && titleId != "FFFE07DF")
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 5, "Title Id: " + Globals.TitleId);
                }));
            }
            Globals.isGamerPic = false;
        }
        private void SendFile()
        {
            try
            {
                Globals.PushingtoConsole = "Pushing to Console";
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 16, "Status: Pushing to Console. Closing tool now may freeze Console.");
                    Globals.changeLine(richTextBox1, 12, Globals.PushingtoConsole);
                }));
                Globals.Pushing = true;
                new Thread(new ThreadStart(this.Processing)).Start();
                Form1.xbCon.SendFile(Globals.file, Globals.consoleFile);
                Globals.Pushing = false;
            }
            catch
            {
                Globals.Pushing = false;
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 15, "Status: Failed to Push file to Console! Connection timmed out? try again");
                    Globals.changeLine(richTextBox1, 12, "Failed to Push to console");
                    Globals.changeLine(richTextBox1, 16, "Drop another file or Press any key to close.");
                    Globals.changeLine(richTextBox1, 17, "Press \"B\" to go back to the Main Screen.");
                    Globals.Back2MS = true;
                }));
            }
        }
        private void SendGOD()
        {
            try
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 16, "Status: Pushing to Console. Closing tool now may freeze Console.");
                    base.Height = 261;
                }));
                Globals.PushingtoConsole = "Pushing to Console.";
                DirectoryInfo directoryInfo = new DirectoryInfo(Globals.file + ".data");
                int num2 = directoryInfo.GetFiles().Length;
                Globals.Pushing = true;
                new Thread(new ThreadStart(this.Processing)).Start();
                Form1.xbCon.SendFile(Globals.file, Globals.consoleFile);
                int num3 = 1;
                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    Globals.PushingtoConsole = string.Concat(new object[]
                    {
                        "Pushing file ",
                        num3,
                        "/",
                        num2,
                        ", (",
                        fileInfo.Name,
                        ") to Console"
                    });
                    string text = string.Concat(new object[]
                    {
                        "GOD file ",
                        num3,
                        "/",
                        num2,
                        " (",
                        fileInfo.Name,
                        ")"
                    });
                    Form1.xbCon.SendFile(fileInfo.FullName, Globals.GODDataConsoleDir + "\\" + fileInfo.Name);
                    num3++;
                }
                Globals.Pushing = false;
            }
            catch
            {
                Globals.Pushing = false;
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 16, "Status: Failed to Push file to Console!");
                    Globals.changeLine(richTextBox1, 12, "Failed to Push to console");
                }));
            }
        }
        private void DetermineSize()
        {
            long length = new FileInfo(Globals.file).Length;
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                Globals.changeLine(richTextBox1, 7, "Size: " + Globals.HandleSize(length));
            }));
            if (Globals.isGOD)
            {
                base.BeginInvoke(new MethodInvoker(delegate ()
                {
                    Globals.changeLine(richTextBox1, 7, "Size: " + Globals.HandleSize(Globals.GODbytes = Convert.ToInt64(Globals.GetDirectorySize(Globals.file + ".data"))));
                }));
            }
        }
        private void SwapProfId()
        {
            try
            {
                Globals.settings = "profid";
                new SwapProfileID().ShowDialog();
            }
            catch
            {
            }
        }
        private void MakeConsoleDir()
        {
            try
            {
                Form1.xbCon.MakeDirectory("HDD:\\Content\\" + Globals.ProfileId);
            }
            catch
            {
            }
            try
            {
                Form1.xbCon.MakeDirectory("HDD:\\Content\\" + Globals.ProfileId + "\\" + Globals.TitleId);
            }
            catch
            {
            }
            try
            {
                Form1.xbCon.MakeDirectory(string.Concat(new string[]
                {
                    "HDD:\\Content\\",
                    Globals.ProfileId,
                    "\\",
                    Globals.TitleId,
                    "\\",
                    Globals.Type
                }));
            }
            catch
            {
            }
            if (Globals.isGOD)
            {
                try
                {
                    Globals.GODDataConsoleDir = Globals.consoleFile + ".data";
                    Form1.xbCon.MakeDirectory(Globals.GODDataConsoleDir);
                }
                catch
                {
                }
            }
        }
        #endregion
        #region Automated Stuff
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Globals.AtMS = false;
            Globals.sw.Start();
            this.DetermineName();
            this.DetermineInfo();
            this.DetermineType();
            this.DetermineTitleId();
            this.DetermineProfileId();
            if (this.checkEdit1.Checked)
            {
                this.SwapProfId();
            }
            this.DetermineProfileId();
            this.DeterminePath();
            this.DetermineSize();

            if (!Globals.isConnected)
            {
                Thread.Sleep(500);
                ConnectToConsole2();
            }
            Thread.Sleep(500);
            if (!Globals.Failed)
            {
                this.MakeConsoleDir();
            }
            if (!Globals.Failed && !Globals.isGOD)
            {
                this.SendFile();
            }
            if (Globals.isGOD)
            {
                this.SendGOD();
            }
            Thread.Sleep(1000);
            Globals.ClearStrings();

            Globals.KeyExit = true;
            base.BeginInvoke(new MethodInvoker(delegate ()
            {
                base.Height = 237;

            }));
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
        }

        private void ContentInject_DragDrop(object sender, DragEventArgs e)
        {
            Globals.KeyExit = false;
            while (this.backgroundWorker1.IsBusy)
            {
                Thread.Sleep(1000);
            }
            this.WorkingScreen();
            string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
            for (int i = 0; i < array.Length; i++)
            {
                Globals.file = array[i];
                this.MultiFiles();
            }
        }

        private void ContentInject_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 'U' || e.KeyChar == 'u') && Globals.Updatable && !Globals.Connecting && !Globals.Pushing)
            {

            }
            if ((e.KeyChar == 'D' || e.KeyChar == 'd') && Globals.Updatable && !Globals.Connecting && !Globals.Pushing)
            {
                MessageBox.Show(Globals.Details);
                return;
            }
            if (Globals.KeyExit && e.KeyChar != 'B' && e.KeyChar != 'b')
            {
                Environment.Exit(Environment.ExitCode);
                return;
            }
            if ((e.KeyChar == 'B' || e.KeyChar == 'b') && Globals.Back2MS && !Globals.AtMS)
            {
                this.MainScreen();
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 'U' || e.KeyChar == 'u') && Globals.Updatable && !Globals.Connecting && !Globals.Pushing)
            {

            }
            if ((e.KeyChar == 'D' || e.KeyChar == 'd') && Globals.Updatable && !Globals.Connecting && !Globals.Pushing)
            {
                MessageBox.Show(Globals.Details);
                return;
            }
            if (Globals.KeyExit && e.KeyChar != 'B' && e.KeyChar != 'b')
            {
                Environment.Exit(Environment.ExitCode);
                return;
            }
            if ((e.KeyChar == 'B' || e.KeyChar == 'b') && Globals.Back2MS && !Globals.AtMS)
            {
                this.MainScreen();
            }
        }

        private void ContentInject_DragEnter(object sender, DragEventArgs e)
        {
            if (Globals.Pushing || Globals.Connecting)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
            }
            else if (!Globals.Pushing && !Globals.Connecting && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
        }
        #endregion

    }
}