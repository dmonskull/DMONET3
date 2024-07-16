using ContentInjector;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMONET3.Forms
{
    public partial class SwapProfileID : DevExpress.XtraEditors.XtraForm
    {
        public SwapProfileID()
        {
            InitializeComponent();
        }
        private void SwapProfileID_Load(object sender, EventArgs e)
        {
            if (Globals.settings == "profid")
            {
                Text = "Profile Id Swap";
                labelControl1.Text = "Profile Id:";
                textEdit1.Text = "E000000000000000";
                if (Globals.ProfileId != null)
                {
                    textEdit1.Text = Globals.ProfileId;
                }
            }
        }
        private void extract(string nameSpace, string outdir, string name)
        {
            using (Stream manifestResourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream(nameSpace + "." + name))
            {
                using (BinaryReader binaryReader = new BinaryReader(manifestResourceStream))
                {
                    using (FileStream fileStream = new FileStream(outdir + "\\" + name, FileMode.OpenOrCreate))
                    {
                        using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                        {
                            binaryWriter.Write(binaryReader.ReadBytes((int)manifestResourceStream.Length));
                        }
                    }
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            extract("ContentInjector", Application.StartupPath, "KV.bin");
            Thread.Sleep(10000);
            File.Delete(Application.StartupPath + "\\KV.bin");
        }
    }
}