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
            //ResetForm();
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
            try
            {
                lines = System.IO.File.ReadAllLines(path); // get file into string array
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
            int totalUnknown = topLocations["N/A"].count + topLocations["Not Tracked"].count;
            List<KeyValuePair<string, ValuePair>> sortedLocations = topLocations.ToList<KeyValuePair<string, ValuePair>>();
            sortedLocations.Sort((a, b) => b.Value.count.CompareTo(a.Value.count));
            for (int i = 0; i < sortedLocations.Count; i++)
            {
                string percentOfTotal = (((decimal)sortedLocations[i].Value.count / (decimal)data.Count) * 100).ToString("###.###") + "%";
                dgvAreas.Rows.Add(sortedLocations[i].Key, sortedLocations[i].Value, percentOfTotal);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Process.Start(path);
        }

    }
}
