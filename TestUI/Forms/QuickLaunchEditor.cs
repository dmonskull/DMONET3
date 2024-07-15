using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using System.IO;
using System.Reflection;
using DevExpress.ClipboardSource.SpreadsheetML;
using System.Xml;
using DevExpress.XtraGrid.Views.Grid;

namespace DMONET3.Forms
{
    public partial class QuickLaunchEditor : DevExpress.XtraEditors.XtraForm
    {
        private string filePath;
        private FileIniDataParser parser;
        private IniData data;
        private DataTable gamesTable;
        public QuickLaunchEditor()
        {
            InitializeComponent();
            filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "settings.ini");
            parser = new FileIniDataParser();
            LoadGameNames();
        }

        private void QuickLaunchEditor_Load(object sender, EventArgs e)
        {

        }
        private void LoadGameNames()
        {
            gamesTable = new DataTable();
            gamesTable.Columns.Add("Game ID");
            gamesTable.Columns.Add("Game Name");

            try
            {
                data = parser.ReadFile(filePath);
                if (data.Sections.ContainsSection("games"))
                {
                    foreach (KeyData key in data["games"])
                    {
                        DataRow row = gamesTable.NewRow();
                        row["Game ID"] = key.KeyName;
                        row["Game Name"] = key.Value;
                        gamesTable.Rows.Add(row);
                    }
                    gridControl1.DataSource = gamesTable;
                    ConfigureGridView();
                }
                else
                {
                    XtraMessageBox.Show("No games found in the INI file.");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"An error occurred while reading the INI file: {ex.Message}");
            }
        }
        private void SaveGameNames()
        {
            try
            {
                if (data == null)
                {
                    data = new IniData();
                }

                if (!data.Sections.ContainsSection("games"))
                {
                    data.Sections.AddSection("games");
                }

                foreach (DataRow row in gamesTable.Rows)
                {
                    string gameId = row["Game ID"].ToString();
                    string gameName = row["Game Name"].ToString();
                    data["games"][gameId] = gameName;
                }

                parser.WriteFile(filePath, data);
                XtraMessageBox.Show("Game names saved successfully.");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"An error occurred while saving the INI file: {ex.Message}");
            }
        }
        private void ConfigureGridView()
        {
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView != null)
            {
                gridView.Columns["Game ID"].OptionsColumn.AllowEdit = false;
                gridView.Columns["Game ID"].OptionsColumn.ReadOnly = true;
                gridView.Columns["Game Name"].OptionsColumn.AllowEdit = true;
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SaveGameNames();
        }
    }
}