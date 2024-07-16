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
using XDevkit;
using XDRPC;

namespace TestUI.Forms
{
    public partial class ResidentEvil4 : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        // resident evil 4 stuff
        private bool extrahealthleon;
        private bool extrahealthashley;
        private bool camerafollow;
        private int ptasState = 0;
        private bool HugeEnemies;
        private bool ShrinkEnemies;
        public ResidentEvil4()
        {
            InitializeComponent();
            InitializeDictionaries();
        }

        private void ResidentEvil4_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConnectToConsole2())
                {
                    barButtonItem1.Caption = "Reconnect";
                }
                    InitializeDictionaries();
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
        private void RandomizeFirstByteAtItemOffsets(Dictionary<string, uint> itemSlotOffsets)
        {
            Random random = new Random();

            foreach (var entry in itemSlotOffsets)
            {
                byte randomByte = (byte)random.Next(256);
                xbCon.WriteBytes(entry.Value, new byte[] { randomByte });
            }
        }
        private void CheckHeldWeapons()
        {
            StringBuilder heldWeapons = new StringBuilder();
            foreach (var entry in itemSlotOffsets)
            {
                byte[] slotValue = xbCon.ReadBytes(entry.Value, 4);

                foreach (var weaponEntry in itemDictionary)
                {
                    if (weaponEntry.Value == slotValue[0])
                    {
                        heldWeapons.AppendLine($"Slot: {entry.Key}, Weapon: {weaponEntry.Key}");
                    }
                }
            }
            MessageBox.Show(heldWeapons.ToString());
        }
        private void ToggleFeature(ref bool featureFlag, SimpleButton button, string onText, string offText, uint address, byte[] onBytes, byte[] offBytes)
        {
            try
            {
                button.Text = featureFlag ? offText : onText;
                byte[] data = featureFlag ? offBytes : onBytes;
                xbCon.WriteBytes(address, data);
                featureFlag = !featureFlag;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        #region DictionaryStuff
        private void PopulateComboBoxEdit(ComboBoxEdit comboBoxEdit, IEnumerable<string> items)
        {
            comboBoxEdit.Properties.Items.Clear();
            comboBoxEdit.Properties.Items.AddRange(items.ToArray());
        }
        private void InitializeDictionaries()
        {
            PopulateComboBoxEdit(comboBoxEdit1, itemDictionary.Keys);
            PopulateComboBoxEdit(comboBoxEdit2, itemSlotOffsets.Keys);
            PopulateComboBoxEdit(comboBoxEdit3, ammoAndSmallItems.Keys);
            PopulateComboBoxEdit(comboBoxEdit4, giveItemSlots.Keys);
            PopulateComboBoxEdit(comboBoxEdit5, itemAmountOffsets.Keys);
        }

        Dictionary<string, uint> itemSlotOffsets = new Dictionary<string, uint>
        {
            {"1st Item Slot", 3281685295U},
            {"2nd Item Slot", 3281685323U},
            {"3rd Item Slot", 3281685309U},
            {"4th Item Slot", 3281685365U},
            {"5th Item Slot", 3281685379U},
            {"6th Item Slot", 3281685393U},
            {"7th Item Slot", 3281685407U},
            {"8th Item Slot", 3281685421U},
            {"9th Item Slot", 3281685435U},
            {"10th Item Slot", 3281685449U},
            {"11th Item Slot", 3281685491U},
            {"12th Item Slot", 3281685533U},
            {"13th Item Slot", 3281685547U},
            {"14th Item Slot", 3281685561U},
            {"15th Item Slot", 3281685603U},
            {"16th Item Slot", 3281685673U},
            {"17th Item Slot", 3281685701U},
            {"18th Item Slot", 3281685715U},
            {"19th Item Slot", 3281685729U},
            {"20th Item Slot", 3281685813U}
        };


        Dictionary<string, List<uint>> itemAmountOffsets = new Dictionary<string, List<uint>>
        {
            {"1st Item Amount", new List<uint> {3281685302U}},
            {"2nd Item Amount", new List<uint> {3281685324U}},
            {"3rd Item Amount", new List<uint> {3281685310U}},
            {"4th Item Amount", new List<uint> {3281685366U}},
            {"5th Item Amount", new List<uint> {3281685380U}},
            {"6th Item Amount", new List<uint> {3281685394U}},
            {"7th Item Amount", new List<uint> {3281685408U}},
            {"8th Item Amount", new List<uint> {3281685422U}},
            {"9th Item Amount", new List<uint> {3281685436U}},
            {"10th Item Amount", new List<uint> {3281685450U, 3281685456U}},
            {"11th Item Amount", new List<uint> {3281685492U}},
            {"12th Item Amount", new List<uint> {3281685534U}},
            {"13th Item Amount", new List<uint> {3281685548U}},
            {"14th Item Amount", new List<uint> {3281685562U}},
            {"15th Item Amount", new List<uint> {3281685604U}},
            {"16th Item Amount", new List<uint> {3281685674U}},
            {"17th Item Amount", new List<uint> {3281685702U}},
            {"18th Item Amount", new List<uint> {3281685716U}},
            {"19th Item Amount", new List<uint> {3281685730U}},
            {"20th Item Amount", new List<uint> {3281685814U}},
        };



        Dictionary<string, uint> giveItemSlots = new Dictionary<string, uint>
        {
            {"Give Item Slot 1", 3281685393U},
            {"Give Item Slot 2", 3281685421U},
            {"Give Item Slot 3", 3281685449U},
            {"Give Item Slot 4", 3281685491U},
            {"Give Item Slot 5", 3281685547U}
        };

        Dictionary<string, uint> enableItemSlots = new Dictionary<string, uint>
        {
            {"Give Item Slot 1", 3281685396U},
            {"Give Item Slot 2", 3281685424U},
            {"Give Item Slot 3", 3281685452U},
            {"Give Item Slot 4", 3281685494U},
            {"Give Item Slot 5", 3281685550U}
        };

        Dictionary<string, byte> itemDictionary = new Dictionary<string, byte>
        {
            {"Matilda", 0x03},
            {"Bow Gun", 0x10},
            {"Chicago Typewriter", 0x34},
            {"Combat Knife", 0x38},
            {"Custom TMP", 0x3E},
            {"Handcannon", 0x37},
            {"Handgun", 0x23},
            {"Handgun w/ Silencer", 0x24},
            {"Infinite Launcher", 0x6D},
            {"Mine Thrower", 0x36},
            {"P.R.L 412", 0x41},
            {"Red9", 0x25},
            {"Red9 with Stock", 0x26},
            {"Rifle (semi auto) with Infrared Scope", 0x51},
            {"Rocket Launcher Special", 0x17},
            {"Shotgun", 0x47},
            {"Killer7", 0x2A},
            {"Killer7 w/ Silencer", 0x2B},
            {"Punisher", 0x40},
            {"Krauser's Bow", 0x52},
            {"Krauser's Knife", 0x0D},
            {"Hand Grenade", 0x01},
            {"Flash Grenade", 0x0E},
            {"Incendiary Grenade", 0x02},
            {"Arrows", 0x72},
            {"Bowgun Bolts", 0x11},
            {"Chicago Typewriter Ammo", 0x6A},
            {"Handgun Ammo", 0x04},
            {"Handcannon Ammo", 0x1A},
            {"Shotgun Ammo", 0x18},
            {"Rifle Ammo", 0x07},
            {"Rifle Ammo (Infrared)", 0xA0},
            {"TMP Ammo", 0x20},
            {"First Aid", 0x05},
            {"Green Herb", 0x06},
            {"Mixed Herbs (G+R)", 0x14},
            {"Mixed Herbs (G+R+Y)", 0x15},
            {"Mixed Herbs (R+Y)", 0xA8},
            {"Activation Key (blue)", 0x31},
            {"Activation Key (red)", 0x33},
            {"Black Bass", 0x95},
            {"Black Bass (L)", 0x97},
            {"Blacktail", 0x27},
            {"Blacktail with Silencer", 0x28},
            {"Bonus Time", 0x73},
            {"Brass Pocket Watch", 0x59},
            {"Camp Key", 0x8C},
            {"Castle Gate Key", 0xA7},
            {"Elegant Headdress", 0x5A},
            {"Emergency Lock Key Card", 0x74},
            {"Emerald", 0xA1},
            {"Freezer Key Card", 0x84},
            {"Gallery Key", 0xA3},
            {"Gold Chicken Egg", 0x0A},
            {"Gold Sword", 0x80},
            {"Green Catseye", 0x5F},
            {"Hexagonal Emblem", 0xA6},
            {"JetSki Key", 0x88},
            {"Key to the Mine", 0x7B},
            {"Lift Activation Key", 0x8E},
            {"Pearl Pendant", 0x58},
            {"Piece of the Holy Beast, Eagle", 0x87},
            {"Piece of the Holy Beast, Panther", 0x85},
            {"Piece of the Holy Beast, Serpent", 0x86},
            {"Plaga Sample", 0x0C},
            {"Platinum Sword", 0xC4},
            {"Prison Key", 0xC3},
            {"Ruby", 0x77},
            {"Serpent Ornament", 0x39},
            {"Waste Disposal Key", 0x92},
            {"Yellow Catseye", 0x61}
        };

        Dictionary<string, byte> ammoAndSmallItems = new Dictionary<string, byte>
        {
            {"Hand Grenade", 0x01},
            {"Flash Grenade", 0x0E},
            {"Incendiary Grenade", 0x02},
            {"Arrows", 0x72},
            {"Bowgun Bolts", 0x11},
            {"Chicago Typewriter Ammo", 0x6A},
            {"Handgun Ammo", 0x04},
            {"Handcannon Ammo", 0x1A},
            {"Shotgun Ammo", 0x18},
            {"Rifle Ammo", 0x07},
            {"Rifle Ammo (Infrared)", 0xA0},
            {"TMP Ammo", 0x20},
            {"First Aid", 0x05},
            {"Mixed Herbs (G+R)", 0x14},
            {"Mixed Herbs (G+R+Y)", 0x15},
            {"Mixed Herbs (R+Y)", 0xA8},
            {"Plaga Sample", 0x0C}
        };
        #endregion

        #region ButtonClicks
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (itemSlotOffsets.ContainsKey(comboBoxEdit2.Text) && itemDictionary.ContainsKey(comboBoxEdit1.Text))
            {
                uint selectedSlotOffset = itemSlotOffsets[comboBoxEdit2.Text];
                byte selectedValue = itemDictionary[comboBoxEdit1.Text];
                xbCon.WriteBytes(selectedSlotOffset, new byte[] { selectedValue });
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (giveItemSlots.ContainsKey(comboBoxEdit4.Text) && ammoAndSmallItems.ContainsKey(comboBoxEdit3.Text))
            {
                uint selectedSlotOffset = giveItemSlots[comboBoxEdit4.Text];
                byte selectedValue = itemDictionary[comboBoxEdit3.Text];
                xbCon.WriteBytes(selectedSlotOffset, new byte[] { selectedValue });
            }
            if (enableItemSlots.ContainsKey(comboBoxEdit4.Text))
            {
                uint selectedSlotOffset = enableItemSlots[comboBoxEdit4.Text];
                xbCon.WriteBytes(selectedSlotOffset, new byte[] { 0x01 });
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string selectedSlot = comboBoxEdit5.Text;
            ushort selectedAmount = (ushort)numericUpDown1.Value;

            if (itemAmountOffsets.ContainsKey(selectedSlot))
            {
                List<uint> selectedAmountOffsets = itemAmountOffsets[selectedSlot];

                foreach (var selectedAmountOffset in selectedAmountOffsets)
                {
                    byte[] amountBytes = BitConverter.GetBytes(selectedAmount);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(amountBytes);
                    }
                    xbCon.WriteBytes(selectedAmountOffset, amountBytes);
                }
            }
            else
            {
                // Handle the case when the selected slot is not found
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string message = "Replace: replaces any gun currently in your inventory in the selected item slot.\n\nGive: gives you any small item listed, puts it in the discarded items inventory, have to move it into inventory from discarded side.\n\nSet: sets the amount you want for the selected item slot (max 999)\n\nPlease use 'Weapon Check' to see what guns are currently in which item slot.";
            XtraMessageBox.Show(message, "RE4 Modding Tool Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            foreach (var amountOffsets in itemAmountOffsets.Values)
            {
                foreach (var amountOffset in amountOffsets)
                {
                    byte[] amountBytes = new byte[] { 0x03, 0xE7 };
                    xbCon.WriteBytes(amountOffset, amountBytes);
                }
            }
            MessageBox.Show("Max Amount for ALL slots has been set!", ":)");
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                RandomizeFirstByteAtItemOffsets(itemSlotOffsets);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            try
            {
                CheckHeldWeapons();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            ToggleFeature(
                ref extrahealthleon,
                simpleButton8,
                "Extra Health Leon: ON",
                "Extra Health Leon: OFF",
                3261454420U,
                new byte[] { 0x7F },
                new byte[] { 0x06 }
            );
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            ToggleFeature(
                ref extrahealthashley,
                simpleButton9,
                "Extra Health Ashley: ON",
                "Extra Health Ashley: OFF",
                3261454424U,
                new byte[] { 0x7F },
                new byte[] { 0x05 }
            );
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            ToggleFeature(
                ref ShrinkEnemies,
                simpleButton10,
                "Shrink Enemies: ON",
                "Shrink Enemies: OFF",
                0x82000A08,
                new byte[] { 0x3F, 0x19, 0x99, 0x9A },
                new byte[] { 0x3F, 0x66, 0x66, 0x66 }
            );
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            ToggleFeature(
                ref HugeEnemies,
                simpleButton12,
                "Huge Enemies: ON",
                "Huge Enemies: OFF",
                0x82000A08,
                new byte[] { 0x3F, 0x7D, 0x70, 0xA4 },
                new byte[] { 0x3F, 0x66, 0x66, 0x66 }
            );
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            try
            {
                switch (ptasState)
                {
                    case 0:
                        simpleButton11.Text = "PTAS: 99999999";
                        xbCon.WriteBytes(3261454408U, new byte[] { 0x0F, 0xFF, 0xFF, 0xFF });
                        ptasState = 1;
                        break;
                    case 1:
                        simpleButton11.Text = "PTAS: 1000";
                        xbCon.WriteBytes(3261454408U, new byte[] { 0x00, 0x00, 0x03, 0xE8 });
                        ptasState = 2;
                        break;
                    case 2:
                        simpleButton11.Text = "PTAS: bugged";
                        xbCon.WriteBytes(3261454408U, new byte[] { 0xAA, 0xAA, 0xFF, 0xFF });
                        ptasState = 0;
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            ToggleFeature(
                ref camerafollow,
                simpleButton13,
                "Camera Follow: ON",
                "Camera Follow: OFF",
                3261454526U,
                new byte[] { 0x01 },
                new byte[] { 0x02 }
            );
        }

        #endregion
    }
}