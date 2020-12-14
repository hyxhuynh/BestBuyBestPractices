using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyBestPractices
{
    // property names must match the MySQL Database
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
    }
}
