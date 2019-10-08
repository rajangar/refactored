using System;
using System.Collections.Generic;

namespace refactor_this.Models
{
    public class Products
    {
        /// <summary>
        /// List of Product
        /// </summary>
        public List<Product> Items { get; private set; }

        /// <summary>
        /// Initializing the list
        /// </summary>
        public Products()
        {
            Items = new List<Product>();
        }

        /// <summary>
        /// To get all products list
        /// </summary>
        /// <returns>
        /// Products class's object
        /// </returns>
        public static Products GetAllProducts()
        {
            Products products = new Products();

            products.LoadProducts();

            return products;
        }

        /// <summary>
        /// Get all products by name
        /// </summary>
        /// <param name="name"> product name to search </param>
        /// <returns>
        /// Product class's object
        /// </returns>
        public static Products GetProductsByName(string name)
        {
            Products products = new Products();

            products.LoadProducts(name);

            return products;
        }

        /// <summary>
        /// Loading products from SQL query into the object
        /// </summary>
        /// <param name="name"> name if given </param>
        private void LoadProducts(string name="")
        {
            string where = "";
            if (name != "")
            {
                where = $"where lower(name) like '%{name.ToLower()}%'";
            }
            var cmd = $"select id from product {where}";

            var rdr = Helpers.ExecuteQuery(cmd);
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                Items.Add(Product.GetProductFromId(id));
            }
        }
    }
}