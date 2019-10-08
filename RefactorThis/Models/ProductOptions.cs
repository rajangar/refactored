using System;
using System.Collections.Generic;

namespace refactor_this.Models
{
    /// <summary>
    /// Product Options
    /// </summary>
    public class ProductOptions
    {

        /// <summary>
        /// List to contain all Product options
        /// </summary>
        public List<ProductOption> Items { get; private set; }

        /// <summary>
        /// Initialize list
        /// </summary>
        public ProductOptions()
        {
            Items = new List<ProductOption>();
        }

        /// <summary>
        /// To get all the product options from productid
        /// </summary>
        /// <param name="productId">product id</param>
        /// <returns>
        /// ProductOptions object
        /// </returns>
        public static ProductOptions GetAllProductOptions(Guid productId)
        {
            ProductOptions productOptions = new ProductOptions();

            productOptions.LoadProductOptions(productId);

            return productOptions;
        }

        /// <summary>
        /// Get Product option on the basis of product id and id
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="id">Option id</param>
        /// <returns>
        /// ProductOption found
        /// </returns>
        public static ProductOption GetProductOptionsFromId(Guid productId, Guid id)
        {
            var productOptions = GetAllProductOptions(productId);

            foreach (var option in productOptions.Items)
                if (option.Id == id)
                    return option;

            return null;
        }
        
        /// <summary>
        /// Load product options in object from SQL query
        /// </summary>
        /// <param name="productId">productid to search</param>
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