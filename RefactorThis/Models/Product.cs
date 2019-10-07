using System;

namespace refactor_this.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

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

        public void Create()
        {
            var id = Guid.NewGuid();
            if (GetProductFromId(id) != null)
                return;

            var cmd = $"insert into product (id, name, description, price, deliveryprice) " +
                $"values ('{id}', '{Name}', '{Description}', {Price}, {DeliveryPrice})";

            Helpers.ExecuteQuery(cmd, true);
        }

        public void Update(Guid id)
        {
            if (GetProductFromId(id) == null)
                return;

            var cmd = $"update product set name = '{Name}', description = '{Description}', price = {Price}, " +
                $"deliveryprice = {DeliveryPrice} where id = '{id}'";

            Helpers.ExecuteQuery(cmd, true);
        }

        public static void Delete(Guid id)
        {
            foreach (var option in ProductOptions.GetAllProductOptions(id).Items)
                ProductOption.Delete(option.Id);

            if (GetProductFromId(id) == null)
                return;

            var cmd = $"delete from product where id = '{id}'";

            Helpers.ExecuteQuery(cmd, true);
        }
    }
}