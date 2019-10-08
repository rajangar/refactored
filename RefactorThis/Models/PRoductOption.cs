using System;

namespace refactor_this.Models
{
    /// <summary>
    /// Product Option class
    /// </summary>
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Static searching product option by option id
        /// </summary>
        /// <param name="id">option id</param>
        /// <returns>
        /// ProductOption object
        /// </returns>
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

        /// <summary>
        /// To create a product option row
        /// </summary>
        /// <returns>
        /// status that how many rows created or -1 if error
        /// </returns>
        public int Create()
        {
            var id = Guid.NewGuid();
            
            var cmd = $"insert into productoption (id, productid, name, description) " +
                $"values ('{id}', '{ProductId}', '{Name}', '{Description}')";

            return Helpers.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// To update product option row by option id
        /// </summary>
        /// <param name="id">option id</param>
        /// <returns>
        /// status that how many rows updated or -1 if error
        /// </returns>
        public int Update(Guid id)
        {
            var cmd = $"update productoption set name = '{Name}', description = '{Description}' where id = '{id}'";

            return Helpers.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// To delete a row by option id
        /// </summary>
        /// <param name="id">option id</param>
        /// <returns>
        /// status that how many rows deleted or -1 if error
        /// </returns>
        public static int Delete(Guid id)
        {
            var cmd = $"delete from productoption where id = '{id}'";

            return Helpers.ExecuteNonQuery(cmd);
        }
    }
}