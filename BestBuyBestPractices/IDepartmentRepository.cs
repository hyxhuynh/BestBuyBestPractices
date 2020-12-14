using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyBestPractices
{
    public interface IDepartmentRepository
    {
        // Method GetAllDepartments returns a collection that conforms to IEnumerable<Department>
        IEnumerable<Department> GetAllDepartments();
        void InsertDepartment(string deptName);
    }
}
