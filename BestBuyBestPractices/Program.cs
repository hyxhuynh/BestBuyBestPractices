using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder() 
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            // Console.WriteLine(connString);
            #endregion

            IDbConnection conn = new MySqlConnection(connString);

            DapperDepartmentRepository departmentRepo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hello user, here are the current departments: ");
            Console.WriteLine("Please press Enter. . .");
            Console.ReadLine();

            var departments = departmentRepo.GetAllDepartments();
            Print(departments);

            Console.WriteLine("Do you want to add a department?");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of the new department?");
                userResponse = Console.ReadLine();

                departmentRepo.InsertDepartment(userResponse);
                Print(departmentRepo.GetAllDepartments());
            }

           // PRODUCTS
            Console.WriteLine("Hello user, here are the current products: ");
            Console.WriteLine("Please press Enter. . .");
            Console.ReadLine();
            DapperProductRepository productRepo = new DapperProductRepository(conn);

            // Create, Update, and Delete Methods
            // productRepo.CreateProduct("newProduct", 20, 1);
            // productRepo.UpdateProductName(942, "newStuff");
            // productRepo.DeleteProduct(942);

            // Read Method
            // GetAllProducts belongs to the instance of the DapperProductRepository class
            var products = productRepo.GetAllProducts();         
            PrintProducts(products);

            Console.WriteLine("Have a great day!");

        }

        private static void Print(IEnumerable<Department> departments )
        {
            foreach (var department in departments)
            {
                Console.WriteLine($"Department ID: {department.DepartmentID} Name: {department.Name}");
            }
        }

        private static void PrintProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.ProductID} Name: {product.Name}");
            }
        }

    }
}
