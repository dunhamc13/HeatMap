using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmptyPackageSummary
{
    public partial class frmEmptyPackageSummary : Form
    {
        class ValuePair { public int count; public decimal cost; };
        string[] lines;
        string path = "";
        List<EmptyPackage> data;
        public frmEmptyPackageSummary()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ClearForm();
            openFileDialog1.ShowDialog();
            path = openFileDialog1.FileName;
            txtFilePath.Text = path;
            OpenFile(txtFilePath.Text);
            if (path.Length > 0)
            {
                btnView.Enabled = true;
            }
        }

        private void OpenFile(string path)
        {
            try { progressBar1.Value++; } catch { }
            try
            {
                lines = System.IO.File.ReadAllLines(path); // get file into string array
                if (chkColorCode.Checked)
                    progressBar1.Maximum = (int)Math.Ceiling(lines.Length * 2m);
                else
                    progressBar1.Maximum = (int)Math.Ceiling(lines.Length * 1.8m);
            }
            catch (ArgumentException ae)
            {
                //don't do anything, stop processing
                return;
            }
            data = new List<EmptyPackage>();
            for (int i = 1; i < lines.Length; i++)
            {
                data.Add(new EmptyPackage(lines[i]));
                try { progressBar1.Value++; } catch { }
            }
            AnalyzeData();
        }

        private void AnalyzeData()
        {
            DateTime earliest = data[0].DateTimeLogged;
            DateTime latest = data[0].DateTimeLogged;
            decimal totalDollars = 0m;

            Dictionary<string, ValuePair> topLocations = new Dictionary<string, ValuePair>();
            decimal percentWithoutLocation = 0m;
            Dictionary<string, int> topDepartments = new Dictionary<string, int>();
            for (int i = 0; i < data.Count; i++)
            {
                try { progressBar1.Value++; } catch { }

                EmptyPackage thisPackage = data[i];
                //check date
                if (thisPackage.DateTimeLogged < earliest)
                    earliest = thisPackage.DateTimeLogged;
                else if (thisPackage.DateTimeLogged > latest)
                    latest = thisPackage.DateTimeLogged;

                //accumulate the cost
                totalDollars += thisPackage.Retail;
                //grab this location 
                try
                {
                    if (thisPackage.AreaFound == "Fitting Room" && thisPackage.LocationFound == "N/A")
                    {
                        thisPackage.LocationFound = "Fitting Room";
                    }
                    //try to add to the count in the dictionary
                    topLocations[thisPackage.LocationFound].count++;
                    topLocations[thisPackage.LocationFound].cost += thisPackage.Retail;
                }
                catch (KeyNotFoundException knf)
                {
                    //if it's not there, put it in the dictionary
                    topLocations.Add(thisPackage.LocationFound, new ValuePair { count = 1, cost = thisPackage.Retail });
                }
                //same with department
                try
                {
                    topDepartments[thisPackage.Department]++;
                }
                catch (KeyNotFoundException knf)
                {
                    topDepartments.Add(thisPackage.Department, 1);
                }
            }
            //fill labels
            string summary = "";

            summary += "Date Range: " + earliest.ToString() + " - " + latest.ToString() + "\r\n";

            summary += "Total Packages: " + data.Count.ToString() + "\r\n\r\n";

            int totalUnknown = topLocations["N/A"].count + topLocations["Not Tracked"].count;
            topLocations.Remove("N/A");
            topLocations.Remove("Not Tracked");
            summary += "Unknown Locations: " + totalUnknown.ToString() + " packages, " + (((decimal)totalUnknown / (decimal)data.Count) * 100).ToString("###.##") + "%\r\n";

            int foundAtFittingRoom = topLocations["Fitting Room"].count;
            topLocations.Remove("Fitting Room");
            summary += "Fitting Room: " + foundAtFittingRoom.ToString() + " packages, " + (((decimal)foundAtFittingRoom / (decimal)data.Count) * 100).ToString("###.##") + "%";
            txtSummary.Text = summary;

            List<KeyValuePair<string, ValuePair>> sortedLocations = topLocations.ToList<KeyValuePair<string, ValuePair>>();
            sortedLocations.Sort((a, b) => b.Value.count.CompareTo(a.Value.count));
            for (int i = 0; i < sortedLocations.Count; i++)
            {
                try { progressBar1.Value++; } catch { }

                int thisCount = sortedLocations[i].Value.count;
                string percentOfTotal = (((decimal)sortedLocations[i].Value.count / (decimal)data.Count) * 100).ToString("###.##") + "%";
                int rowIndex = dgvAreas.Rows.Add(sortedLocations[i].Key, sortedLocations[i].Value.count, percentOfTotal);
                dgvAreas.Rows[rowIndex].Tag = thisCount;
            }
            ColorCode(chkColorCode.Checked);
        }

        private void ColorCode(bool usingColor)
        {
            if (usingColor)
            {
                try { progressBar1.Value++; } catch { }

                //get the count cap
                try
                {
                    int highestCount = int.Parse(dgvAreas.Rows[0].Tag.ToString());
                    //init lists to contain indices of rows
                    List<int> darkRed = new List<int>();
                    List<int> red = new List<int>();
                    List<int> orange = new List<int>();
                    List<int> yellow = new List<int>();
                    List<int> lightYellow = new List<int>();
                    List<int> remainder = new List<int>();
                    //check rows, add indices to corresponding lists
                    for (int i = 0; i < dgvAreas.Rows.Count; i++)
                    {
                        try
                        {
                            decimal x = decimal.Parse(dgvAreas.Rows[i].Tag.ToString());

                            if (x >= (highestCount * .9m))
                                darkRed.Add(i);
                            else if (x >= (highestCount * .7m))
                                red.Add(i);
                            else if (x >= (highestCount * .5m))
                                orange.Add(i);
                            else if (x >= (highestCount * .3m))
                                yellow.Add(i);
                            else if (x >= (highestCount * .1m))
                                lightYellow.Add(i);
                            else
                                remainder.Add(i);
                        }
                        catch { break; }
                    }
                    //check each grouping for blanks
                    for (int i = 0; i < 6; i++)
                    {
                        if (darkRed.Count == 0)
                        {
                            darkRed = red;
                            red = orange;
                            orange = yellow;
                            yellow = lightYellow;
                            lightYellow = remainder;
                        }
                        if (red.Count == 0)
                        {
                            red = orange;
                            orange = yellow;
                            yellow = lightYellow;
                            lightYellow = remainder;
                        }
                        if (orange.Count == 0)
                        {
                            orange = yellow;
                            yellow = lightYellow;
                            lightYellow = remainder;
                        }
                        if (yellow.Count == 0)
                        {
                            yellow = lightYellow;
                            lightYellow = remainder;
                        }
                        if (lightYellow.Count == 0)
                        {
                            lightYellow = remainder;
                        }
                    }
                    //apply color coding
                    for (int i = 0; i < darkRed.Count; i++)
                    {
                        dgvAreas.Rows[darkRed[i]].DefaultCellStyle.BackColor = Color.DarkRed;
                        dgvAreas.Rows[darkRed[i]].DefaultCellStyle.ForeColor = Color.White;
                    }
                    for (int i = 0; i < red.Count; i++)
                    {
                        dgvAreas.Rows[red[i]].DefaultCellStyle.BackColor = Color.Red;
                        dgvAreas.Rows[red[i]].DefaultCellStyle.ForeColor = Color.White;
                    }
                    for (int i = 0; i < orange.Count; i++)
                    {
                        dgvAreas.Rows[orange[i]].DefaultCellStyle.BackColor = Color.Orange;
                    }
                    for (int i = 0; i < yellow.Count; i++)
                    {
                        dgvAreas.Rows[yellow[i]].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    for (int i = 0; i < lightYellow.Count; i++)
                    {
                        dgvAreas.Rows[lightYellow[i]].DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
                catch { return; };
            }
            else
            {
                foreach (DataGridViewRow row in dgvAreas.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
            dgvAreas.ClearSelection();
            progressBar1.Value = progressBar1.Maximum;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Process.Start(path);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtFilePath.Clear();
            dgvAreas.Rows.Clear();
            txtSummary.Text = "Analyze a Report to receive a summary.";
            progressBar1.Value = 0;
        }

        private void chkColorCode_CheckedChanged(object sender, EventArgs e)
        {
            ColorCode(chkColorCode.Checked);
        }

        private void frmEmptyPackageSummary_DragDrop(object sender, DragEventArgs e)
        {
            ClearForm();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (System.IO.File.Exists(files[0]))
                {
                    this.txtFilePath.Text = files[0];
                    OpenFile(this.txtFilePath.Text);
                }
            }
        }

        private void frmEmptyPackageSummary_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
