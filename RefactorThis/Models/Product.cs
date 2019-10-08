using System;

namespace refactor_this.Models
{
    /// <summary>
    /// Product class
    /// </summary>
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        /// <summary>
        /// Static function to get the product by id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>
        /// Product object
        /// </returns>
        public static Product GetProductFromId(Guid id)
        {
            var cmd = $"select * from product where id = '{id}'";
            
            var rdr = Helpers.ExecuteQuery(cmd);
            if (!rdr.Read())
                return null;

            Product product = new Product();
            product.Id = Guid.Parse(rdr["Id"].ToString());
            product.Name = rdr["Name"].ToString();
            product.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
            product.Price = decimal.Parse(rdr["Price"].ToString());
            product.DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString());

            return product;
        }

        /// <summary>
        /// To create a new product entry
        /// </summary>
        /// <returns>
        /// Status that how many rows created or -1 if error
        /// </returns>
        public int Create()
        {
            var id = Guid.NewGuid();
            
            var cmd = $"insert into product (id, name, description, price, deliveryprice) " +
                $"values ('{id}', '{Name}', '{Description}', {Price}, {DeliveryPrice})";

            return Helpers.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// To update a product entry by product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>
        /// Status that how many rows updated or -1 if error
        /// </returns>
        public int Update(Guid id)
        {
            var cmd = $"update product set name = '{Name}', description = '{Description}', price = {Price}, " +
                $"deliveryprice = {DeliveryPrice} where id = '{id}'";

            return Helpers.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// To delete a product entry by product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>
        /// Status that how many rows deleted or -1 if error
        /// </returns>
        public static int Delete(Guid id)
        {
            foreach (var option in ProductOptions.GetAllProductOptions(id).Items)
                ProductOption.Delete(option.Id);

            var cmd = $"delete from product where id = '{id}'";

            return Helpers.ExecuteNonQuery(cmd);
        }
    }
}