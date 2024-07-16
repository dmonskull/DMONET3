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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using XDevkit;
using XDRPC;
using Be.Windows.Forms;
using System.Text.RegularExpressions;
using PeekPoker.Interface;
using System.Threading;

namespace DMONET3.Forms
{
    public partial class PeekAndPoke : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        private readonly AutoCompleteStringCollection _data = new AutoCompleteStringCollection();
        private byte[] _old;
        private RealTimeMemory _rtm;
        public PeekAndPoke()
        {
            InitializeComponent();
        }

        private void PeekAndPoke_Load(object sender, EventArgs e)
        {
            try
            {
                ConnectToConsole2();
            }
            catch { }
            ipAddressTextBox.Text = ReverseIP(xbCon.GetConsoleIP());
        }
        public bool ConnectToConsole2()
        {
            if (activeConnection && xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
            {
                return true;
            }
            try
            {
                xbManager = (XboxManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                ConnectionCode = xbCon.OpenConnection(null);
                xboxConnection = xbCon.OpenConnection(null);

                if (!xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    xbCon.DebugTarget.ConnectAsDebugger("Xbox Toolbox", XboxDebugConnectFlags.Force);
                }

                activeConnection = xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName);
                return activeConnection;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Could not connect to console: " + xbManager.DefaultConsole);
                return false;
            }
        }
        static string ReverseIP(string ip)
        {
            string[] parts = ip.Split('.');
            Array.Reverse(parts);
            return string.Join(".", parts);
        }
        private void Connect(object a)
        {
            if (ipAddressTextBox.Text.ToUpper() == "DEBUG") //For debugging PP without a connection to xbox
            {
                return; //Bypass needing to connect to xbox for debugging purposes.
            }
            try
            {
                if (!Regex.IsMatch(ipAddressTextBox.Text, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
                //Checks if valid IP
                {
                    MessageBox.Show(" IP Address is not valid!");
                    return;
                }

                _rtm = new RealTimeMemory(ipAddressTextBox.Text, 0, 0); //initialize real time memory


                if (!_rtm.Connect())
                {
                    throw new Exception("Connection Failed!");
                }
                else
                {
                    MessageBox.Show("Connected!");
                }
            }
            catch (Exception)
            {

            }
        }
        private void ChangeNumericMaxMin()
        {
            if (isSigned.Checked)
            {
                NumericInt8.Maximum = SByte.MaxValue;
                NumericInt8.Minimum = SByte.MinValue;
                NumericInt16.Maximum = Int16.MaxValue;
                NumericInt16.Minimum = Int16.MinValue;
                NumericInt32.Maximum = Int32.MaxValue;
                NumericInt32.Minimum = Int32.MinValue;
            }
            else
            {
                NumericInt8.Maximum = Byte.MaxValue;
                NumericInt8.Minimum = Byte.MinValue;
                NumericInt16.Maximum = UInt16.MaxValue;
                NumericInt16.Minimum = UInt16.MinValue;
                NumericInt32.Maximum = UInt32.MaxValue;
                NumericInt32.Minimum = UInt32.MinValue;
            }
        }
        private void AutoComplete()
        {
            peekPokeAddressTextBox2.AutoCompleteCustomSource = _data; //put the auto complete data into the textbox
            int count = _data.Count;
            for (int index = 0; index < count; index++)
            {
                string value = _data[index];
                //if the text in peek or poke text box is not in autocomplete data - Add it
                if (!ReferenceEquals(value, peekPokeAddressTextBox2.Text))
                    _data.Add(peekPokeAddressTextBox2.Text);
            }
        }
        private void SetHexBoxByteProvider(DynamicByteProvider value)
        {
            if (hexBox.InvokeRequired)
                Invoke((MethodInvoker)(() => SetHexBoxByteProvider(value)));
            else
            {
                hexBox.ByteProvider = value;
            }
        }
        private void SetHexBoxRefresh()
        {
            if (hexBox.InvokeRequired)
                Invoke((MethodInvoker)(SetHexBoxRefresh));
            else
            {
                hexBox.Refresh();
            }
        }
        private string GetTextBoxText(Control control)
        {
            //recursion
            string returnVal = "";
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)
                               delegate { returnVal = GetTextBoxText(control); });
            else
                return control.Text;
            return returnVal;
        }
        private void Peek()
        {
            try
            {
                if (string.IsNullOrEmpty(GetTextBoxText(peekLengthTextBox)) ||
                    Convert.ToUInt32(GetTextBoxText(peekLengthTextBox), 16) == 0)
                    throw new Exception("Invalid peek length!");
                if (string.IsNullOrEmpty(GetTextBoxText(peekPokeAddressTextBox2)) ||
                    Convert.ToUInt32(GetTextBoxText(peekPokeAddressTextBox2), 16) == 0)
                    throw new Exception("Address cannot be 0 or null");
                //convert peek result string values to byte

                byte[] retValue =
                    Functions.StringToByteArray(_rtm.Peek(GetTextBoxText(peekPokeAddressTextBox2),
                                                          GetTextBoxText(peekLengthTextBox),
                                                          GetTextBoxText(peekPokeAddressTextBox2),
                                                          GetTextBoxText(peekLengthTextBox)));
                var buffer = new DynamicByteProvider(retValue) { IsWriteByte = true }; //object initilizer

                _old = new byte[buffer.Bytes.Count];
                buffer.Bytes.CopyTo(_old);

                SetHexBoxByteProvider(buffer);
                SetHexBoxRefresh();

                MessageBox.Show("Peak Success");
            }
            catch (Exception)
            {


            }
        }
        private DynamicByteProvider GetHexBoxByteProvider()
        {
            //recursion
            var returnVal = new DynamicByteProvider(new byte[] { 0, 0, 0, 0 });
            if (hexBox.InvokeRequired)
                hexBox.Invoke((MethodInvoker)
                              delegate { returnVal = GetHexBoxByteProvider(); });
            else
                return (DynamicByteProvider)hexBox.ByteProvider;
            return returnVal;
        }
        private void Poke()
        {
            try
            {
                uint dumplength = (uint)hexBox.ByteProvider.Length / 2;
                _rtm.DumpOffset = Functions.Convert(GetTextBoxText(peekPokeAddressTextBox2)); //Set the dump offset
                _rtm.DumpLength = dumplength; //The length of data to dump

                DynamicByteProvider buffer = GetHexBoxByteProvider();
                if (fillCheckBox.Checked)
                {
                    for (int i = 0; i < dumplength; i++)
                    {
                        uint value = Convert.ToUInt32(peekPokeAddressTextBox2.Text, 16);
                        string address = string.Format((value + i).ToString("X8"));
                        _rtm.Poke(address, String.Format("{0,0:X2}", Convert.ToByte(fillValueTextBox.Text, 16)));

                    }
                    MessageBox.Show("Poke Success");
                }
                else
                {
                    for (int i = 0; i < buffer.Bytes.Count; i++)
                    {
                        if (buffer.Bytes[i] == _old[i]) continue;

                        uint value = Convert.ToUInt32(peekPokeAddressTextBox2.Text, 16);
                        string address = string.Format((value + i).ToString("X8"));
                        _rtm.Poke(address, String.Format("{0,0:X2}", buffer.Bytes[i]));
                        peekPokeFeedBackTextBox.Text = "Poke Success";
                    }
                }

            }
            catch (Exception)
            {

            }
        }
        private void ChangeNumericValue()
        {
            if (hexBox.ByteProvider == null) return;
            List<byte> buffer = hexBox.ByteProvider.Bytes;
            if (isSigned.Checked)
            {
                NumericInt8.Value = (buffer.Count - hexBox.SelectionStart) > 0
                                        ? Functions.ByteToSByte(hexBox.ByteProvider.ReadByte(hexBox.SelectionStart))
                                        : 0;
                NumericInt16.Value = (buffer.Count - hexBox.SelectionStart) > 1
                                         ? Functions.BytesToInt16(
                                             buffer.GetRange((int)hexBox.SelectionStart, 2).ToArray())
                                         : 0;
                NumericInt32.Value = (buffer.Count - hexBox.SelectionStart) > 3
                                         ? Functions.BytesToInt32(
                                             buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray())
                                         : 0;

                NumericFloatTextBox.Clear();
                float f = (buffer.Count - hexBox.SelectionStart) > 3
                              ? Functions.BytesToSingle(buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray())
                              : 0;
                NumericFloatTextBox.Text = f.ToString();
            }
            else
            {
                NumericInt8.Value = (buffer.Count - hexBox.SelectionStart) > 0
                                        ? buffer[(int)hexBox.SelectionStart]
                                        : 0;
                NumericInt16.Value = (buffer.Count - hexBox.SelectionStart) > 1
                                         ? Functions.BytesToUInt16(
                                             buffer.GetRange((int)hexBox.SelectionStart, 2).ToArray())
                                         : 0;
                NumericInt32.Value = (buffer.Count - hexBox.SelectionStart) > 3
                                         ? Functions.BytesToUInt32(
                                             buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray())
                                         : 0;

                NumericFloatTextBox.Clear();
                float f = (buffer.Count - hexBox.SelectionStart) > 3
                              ? Functions.BytesToSingle(buffer.GetRange((int)hexBox.SelectionStart, 4).ToArray())
                              : 0;
                NumericFloatTextBox.Text = f.ToString();
            }
            byte[] prev = Functions.HexToBytes(peekPokeAddressTextBox2.Text);
            int address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format((address + (int)hexBox.SelectionStart).ToString("X8"));
        }
        private void ChangedNumericValue(object numfield)
        {
            if (hexBox.SelectionStart >= hexBox.ByteProvider.Bytes.Count) return;
            if (numfield.GetType() == typeof(NumericUpDown))
            {
                var numeric = (NumericUpDown)numfield;
                switch (numeric.Name)
                {
                    case "NumericInt8":
                        if (isSigned.Checked)
                        {
                            Console.WriteLine(((sbyte)numeric.Value).ToString("X2"));
                            hexBox.ByteProvider.WriteByte(hexBox.SelectionStart,
                                                          Functions.HexToBytes(((sbyte)numeric.Value).ToString("X2"))[0
                                                              ]);
                        }
                        else
                        {
                            hexBox.ByteProvider.WriteByte(hexBox.SelectionStart,
                                                          Convert.ToByte((byte)numeric.Value));
                        }
                        break;

                    case "NumericInt16":
                        for (int i = 0; i < 2; i++)
                        {
                            hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i, isSigned.Checked
                                                                                         ? Functions.Int16ToBytes(
                                                                                             (short)numeric.Value)[i]
                                                                                         : Functions.UInt16ToBytes(
                                                                                             (ushort)numeric.Value)[i]);
                        }
                        break;

                    case "NumericInt32":
                        for (int i = 0; i < 4; i++)
                        {
                            hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i, isSigned.Checked
                                                                                         ? Functions.Int32ToBytes(
                                                                                             (int)numeric.Value)[i]
                                                                                         : Functions.UInt32ToBytes(
                                                                                             (uint)numeric.Value)[i]);
                        }
                        break;
                }
            }
            else
            {
                var textbox = (TextBox)numfield;
                for (int i = 0; i < 4; i++)
                {
                    hexBox.ByteProvider.WriteByte(hexBox.SelectionStart + i,
                                                  Functions.FloatToByteArray(Convert.ToSingle(textbox.Text))[i]);
                }
            }
            hexBox.Refresh();
        }
        private void IsSignedCheckedChanged(object sender, EventArgs e)
        {
            ChangeNumericMaxMin();
            ChangeNumericValue();
        }
        private void NumericIntKeyPress(object sender, KeyPressEventArgs e)
        {
            if (hexBox.ByteProvider != null)
            {
                ChangedNumericValue(sender);
            }
        }
        private void NumericValueChanged(object sender, EventArgs e)
        {
            if (hexBox.ByteProvider != null)
            {
                ChangedNumericValue(sender);
            }
        }
        private void NewPeek()
        {
            //Clean up
            peekPokeAddressTextBox2.Text = "C0000000";
            peekLengthTextBox.Text = "FF";
            SelAddress.Clear();
            peekPokeFeedBackTextBox.Clear();
            NumericInt8.Value = 0;
            NumericInt16.Value = 0;
            NumericInt32.Value = 0;
            NumericFloatTextBox.Text = "0";
            hexBox.ByteProvider = null;
            hexBox.Refresh();
        }
        private void freezeButton_Click(object sender, EventArgs e)
        {
            try
            {
                _rtm.StopCommand();
                unfreezeButton.Enabled = true;
                freezeButton.Enabled = false;
            }
            catch (Exception)
            {
                unfreezeButton.Enabled = false;
                freezeButton.Enabled = true;
            }
        }
        private void unfreezeButton_Click(object sender, EventArgs e)
        {
            try
            {
                _rtm.StartCommand();
                unfreezeButton.Enabled = false;
                freezeButton.Enabled = true;
            }
            catch (Exception)
            {
                unfreezeButton.Enabled = true;
                freezeButton.Enabled = false;
            }
        }
        private void hexBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                hexBox.CopyHex();
                e.SuppressKeyPress = true;
            }
        }
        private void hexBox_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeNumericValue(); //When you select an offset on the hexbox
            if (hexBox.ByteProvider == null) return;
            byte[] prev = Functions.HexToBytes(peekPokeAddressTextBox2.Text);
            int address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format((address + (int)hexBox.SelectionStart).ToString("X8"));
        }
        private void hexBox_SelectionStartChanged(object sender, EventArgs e)
        {
            ChangeNumericValue(); //When you select an offset on the hexbox
            if (hexBox.ByteProvider == null) return;
            byte[] prev = Functions.HexToBytes(peekPokeAddressTextBox2.Text);
            int address = Functions.BytesToInt32(prev);
            SelAddress.Text = string.Format((address + (int)hexBox.SelectionStart).ToString("X8"));
        }
        private void peekButton_Click_1(object sender, EventArgs e)
        {
            AutoComplete();
            Peek();
        }
        private void pokeButton_Click_1(object sender, EventArgs e)
        {
            AutoComplete();
            Poke();
        }
        private void newPeekButton_Click_1(object sender, EventArgs e)
        {
            NewPeek();
        }
        private void connectButton_Click_1(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(Connect);
        }
    }
}