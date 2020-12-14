using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace BestBuyBestPractices
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        /// <summary>
        /// whenever we create a new instance of the DapperDepartmentRepository, we will pass in our connection string as a parameter and set that connection string in our private readonly variable _connection
        /// The benefit of having _connection private and readonly is that you can't inadvertently change it from another part of the DepartmentRepository class after it is initialized. The readonly modifier ensures the field can only be given a value during its initialization or in its class constructor.
        /// </summary>
        private readonly IDbConnection _connection;
        public DapperDepartmentRepository(IDbConnection connection)
        {
            // Constructor
            _connection = connection;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            // Query: SELECT  
            // depos is an IEnumerable of Department
            var depos = _connection.Query<Department>("SELECT * FROM departments");
            return depos;
        }


        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
            new { departmentName = newDepartmentName });
        }
    }
}
