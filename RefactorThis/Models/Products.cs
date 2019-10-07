using System;
using System.Collections.Generic;

namespace refactor_this.Models
{
    public class Products
    {
        public List<Product> Items { get; private set; }

        public Products()
        {
            Items = new List<Product>();
        }

        public static Products GetAllProducts()
        {
            Products products = new Products();

            products.LoadProducts();

            return products;
        }

        public static Products GetProductsByName(string name)
        {
            Products products = new Products();

            products.LoadProducts(name);

            return products;
        }

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