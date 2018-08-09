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
        string[] lines;
        string path = "";
        string[][] data;
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
            data = new string[lines.Length - 1][];
            for (int i = 1; i < lines.Length; i++)
            {
                //https://forums.asp.net/t/1247607.aspx?Reading+CSV+with+comma+placed+within+double+quotes+
                // extract the fields
                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                String[] thisRow = CSVParser.Split(lines[i]);
                // clean up the fields (remove " and leading spaces)
                for (int j = 0; j < thisRow.Length; j++)
                {
                    thisRow[j] = thisRow[j].TrimStart(' ', '"');
                    thisRow[j] = thisRow[j].TrimEnd('"');
                }
                data[i - 1] = thisRow;
            }
            AnalyzeData();
        }

        private void AnalyzeData()
        {
            DateTime earliest = DateTime.Parse(data[0][7]);
            DateTime latest = DateTime.Parse(data[0][7]);
            decimal totalDollars = 0m;
            Dictionary<string, int> topLocations = new Dictionary<string, int>();
            decimal percentWithoutLocation = 0m;
            Dictionary<string, int> topDepartments = new Dictionary<string, int>();
            for (int i = 0; i < data.Length; i++)
            {
                string thisLocation = data[i][6];
                string thisDepartment = data[i][1];
                decimal thisCost = decimal.Parse(data[i][4], System.Globalization.NumberStyles.Currency);
                DateTime thisDate = DateTime.Parse(data[0][7]);

                //check date
                if (thisDate < earliest)
                    earliest = thisDate;
                else if (thisDate > latest)
                    latest = thisDate; 

                //accumulate the cost
                totalDollars += thisCost;
                //grab this location 
                try
                {
                    //try to add to the count in the dictionary
                    topLocations[thisLocation] += 1;
                }
                catch (KeyNotFoundException knf)
                {
                    //if it's not there, put it in the dictionary
                    topLocations.Add(thisLocation, 1);
                }
                //same with department
                try
                {
                    topDepartments[thisDepartment] += 1;
                }
                catch (KeyNotFoundException knf)
                {
                    topDepartments.Add(thisDepartment, 1);
                }
            }
            List<KeyValuePair<string, int>> sortedLocations = topLocations.ToList<KeyValuePair<string, int>>();
            sortedLocations.Sort((a, b) => b.Value.CompareTo(a.Value));
            for (int i = 0; i < 20; i++)
            {
                dgvAreas.Rows.Add(sortedLocations[i].Key, sortedLocations[i].Value);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Process.Start(path);
        }

    }
}
