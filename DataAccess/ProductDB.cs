using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDB
    {
        Connection connection = new Connection();
       //Get all product
        public List<Product> GetAllProducts()
        {
            connection.connection();
            List<Product> ProductList = new List<Product>();

            SqlCommand com = new SqlCommand("sp_GetAllProducts", connection.con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            connection.con.Open();
            da.Fill(dt);
            connection.con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(
                    new Product
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToInt32(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()
                    }
                    );
            }
            return ProductList;
        }
        //Insert Products
        public bool InsertProduct(Product product)
        {
            connection.connection();
           
            
                SqlCommand command = new SqlCommand("sp_InsertProduct", connection.con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                connection.con.Open();
                int id = command.ExecuteNonQuery();
                connection.con.Close();
            
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Update Products
        public bool UpdateProduct(Product product)
        {

            connection.connection();
                SqlCommand command = new SqlCommand("sp_UpdateProduct", connection.con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", product.ProductID);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Qty", product.Qty);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                connection.con.Open();
                int i = command.ExecuteNonQuery();
                connection.con.Close();
            
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Delete product
        public string DeleteProduct(int productid)
        {

            connection.connection();
                SqlCommand command = new SqlCommand("sp_DeleteProduct", connection.con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@productid", productid);
                command.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.con.Open();
                command.ExecuteNonQuery();
                string result = command.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                connection.con.Close();
            
            return result;
        }
    }

}
