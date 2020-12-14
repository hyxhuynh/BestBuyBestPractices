using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        #region Encapsulation
        private readonly IDbConnection _connection;
        // Constructor: create a new instance of the class 
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        #endregion

        // Create Method
        public void CreateProduct(string newName, double newPrice, int newCategoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES ( @name, @price, @categoryID );", 
                new { name = newName, price = newPrice, categoryID = newCategoryID });
        }

        // Read Method
        public IEnumerable<Product> GetAllProducts()
        {
            // Dapper starts here
            // Dapper extends ===> IDbConnection
            // Method: return IEnumerable of products
            return _connection.Query<Product>("SELECT * FROM products;");
        }

        // Update Method
        public void UpdateProductName(int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new { updatedName = updatedName, productID = productID });
        }

        // Delete Method
        public void DeleteProduct(int productID)
        {
            // relational database, must delete from all the tables or there will be error
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID});
            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });

        }


    }
}
