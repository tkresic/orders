using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders
{
    static class Database
    {
        private static string _connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Orders.accdb;";

        public static string ConnString
        {
            get { return _connString; }
        }
    }
}
