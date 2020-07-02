using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemoApp.Models
{
    public class ProductDataAccessLayer
    {
        //string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MyTestData;Data Source=DIR\\supriya.khartode";

        string connectionString = "Data Source=localhost;Initial Catalog=MyTestData;Integrated Security=SSPI;";

        //To View all product details  
        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> lstemployee = new List<Product>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Product product = new Product();

                    product.ID = Convert.ToInt32(rdr["ProductId"]);
                    product.Name = rdr["Name"].ToString();
                    product.Description = rdr["Description"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);
                    product.CategoryName = rdr["CategoryId"].ToString();

                    lstemployee.Add(product);
                }
                con.Close();
            }
            return lstemployee;
        }

        //To Add new product record  
        public void AddProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryName);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a particluar product
        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", product.ID);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Gender", product.Description);
                cmd.Parameters.AddWithValue("@Department", product.Price);
                cmd.Parameters.AddWithValue("@City", product.CategoryName);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular product
        public Product GetProductData(int? id)
        {
            Product product = new Product();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblProduct WHERE ProductId= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    product.ID = Convert.ToInt32(rdr["EmployeeID"]);
                    product.Name = rdr["Name"].ToString();
                    product.Description = rdr["Description"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"]);
                    product.CategoryName = rdr["CategoryId"].ToString();
                }
            }
            return product;
        }

        //To Delete the record on a particular employee
        //public void DeleteEmployee(int? id)
        //{

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@EmpId", id);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //}
    }
}
