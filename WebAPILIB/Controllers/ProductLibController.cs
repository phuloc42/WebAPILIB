using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPILIB.Controllers
{
    public class ProductLibController : ApiController
    {
        ProductDB productDB = new ProductDB();
        [HttpGet]        
        public List<Product> GetAllProduct()
        {
            return productDB.GetAllProducts();
        }
        [HttpPost]
        public List<Product> InsertProduct(Product product)
        {
            productDB.InsertProduct(product);
            return productDB.GetAllProducts();
        }
        [HttpPut]
        public Product UpdateProduct(Product product)
        {
            var pro = productDB.GetAllProducts().Find(e => e.ProductID == product.ProductID);
            if(pro == null)
            {
                return null;
            }
            try
            {
                pro.ProductName = product.ProductName;
                pro.Price = product.Price;
                pro.Remarks = product.Remarks;
                pro.Qty = product.Qty;
                productDB.UpdateProduct(pro);
                return pro;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpDelete]
        public List<Product> DeleteProduct(int id)
        {
            productDB.DeleteProduct(id);
            return productDB.GetAllProducts();
        }
    }
}
