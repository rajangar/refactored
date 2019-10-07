using System;

namespace refactor_this.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static ProductOption GetProductOptionFromId(Guid id)
        {
            var cmd = $"select * from productoption where id = '{id}'";
            var rdr = Helpers.ExecuteQuery(cmd);
            if (!rdr.Read())
                return null;

            ProductOption productOption = new ProductOption();
            productOption.Id = Guid.Parse(rdr["Id"].ToString());
            productOption.ProductId = Guid.Parse(rdr["ProductId"].ToString());
            productOption.Name = rdr["Name"].ToString();
            productOption.Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();

            return productOption;
        }

        public void Create()
        {
            var id = Guid.NewGuid();
            if (GetProductOptionFromId(id) != null)
                return;

            var cmd = $"insert into productoption (id, productid, name, description) " +
                $"values ('{id}', '{ProductId}', '{Name}', '{Description}')";

            Helpers.ExecuteQuery(cmd, true);
        }

        public void Update(Guid id)
        {
            if (GetProductOptionFromId(id) == null)
                return;

            var cmd = $"update productoption set name = '{Name}', description = '{Description}' where id = '{id}'";

            Helpers.ExecuteQuery(cmd, true);
        }

        public static void Delete(Guid id)
        {
            if (GetProductOptionFromId(id) == null)
                return;

            var cmd = $"delete from productoption where id = '{id}'";

            Helpers.ExecuteQuery(cmd, true);
        }
    }
}