using System;
using System.Collections.Generic;

namespace refactor_this.Models
{
    public class ProductOptions
    {
        public List<ProductOption> Items { get; private set; }

        public ProductOptions()
        {
            Items = new List<ProductOption>();
        }

        public static ProductOptions GetAllProductOptions(Guid productId)
        {
            ProductOptions productOptions = new ProductOptions();

            productOptions.LoadProductOptions(productId);

            return productOptions;
        }

        public static ProductOption GetProductOptionsFromId(Guid productId, Guid id)
        {
            var productOptions = GetAllProductOptions(productId);

            foreach (var option in productOptions.Items)
                if (option.Id == id)
                    return option;

            return null;
        }

        public void LoadProductOptions(Guid productId)
        {
            string where = $"where productid = '{productId}'";
            var cmd = $"select id from productoption {where}";

            var rdr = Helpers.ExecuteQuery(cmd);
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                Items.Add(ProductOption.GetProductOptionFromId(id));
            }
        }
    }
}