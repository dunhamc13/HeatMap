using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmptyPackageSummary
{
    class EmptyPackage
    {
        private string _store = "";
        private string _department = "";
        private string _class = "";
        private string _item = "";
        private string _description = "";
        private decimal _retail = 0;
        private string _areaFound = "";
        private string _locationFound = "";
        private DateTime _dateTimeLogged;
        private string _pos = "";
        private string _transaction = "";
        private string _loggedBy = "";

        public EmptyPackage(string line)
        {
            //https://forums.asp.net/t/1247607.aspx?Reading+CSV+with+comma+placed+within+double+quotes+
            // extract the fields
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            String[] thisRow = CSVParser.Split(line);
            // clean up the fields (remove " and leading spaces)
            for (int i = 0; i < thisRow.Length; i++)
            {
                thisRow[i] = thisRow[i].TrimStart(' ', '"');
                thisRow[i] = thisRow[i].TrimEnd('"');
            }
            this.Store = thisRow[0];
            this.Department = thisRow[1];
            this.Class = thisRow[2];
            this.Item = thisRow[3].Substring(0, 4);
            this.Description = thisRow[3].Substring(6);
            this.Retail = decimal.Parse(thisRow[4], System.Globalization.NumberStyles.Currency);
            this.AreaFound = thisRow[5];
            this.LocationFound = thisRow[6];
            this.DateTimeLogged = DateTime.Parse(thisRow[7] + " " + thisRow[8]);
            this.Pos = thisRow[9];
            this.Transaction = thisRow[10];
            this.LoggedBy = thisRow[11];
        }

        public string Store { get => _store; set => _store = value; }
        public string Department { get => _department; set => _department = value; }
        public string Class { get => _class; set => _class = value; }
        public string Item { get => _item; set => _item = value; }
        public string Description { get => _description; set => _description = value; }
        public decimal Retail { get => _retail; set => _retail = value; }
        public string AreaFound { get => _areaFound; set => _areaFound = value; }
        public string LocationFound { get => _locationFound; set => _locationFound = value; }
        public DateTime DateTimeLogged { get => _dateTimeLogged; set => _dateTimeLogged = value; }
        public string Pos { get => _pos; set => _pos = value; }
        public string Transaction { get => _transaction; set => _transaction = value; }
        public string LoggedBy { get => _loggedBy; set => _loggedBy = value; }
    }
}
