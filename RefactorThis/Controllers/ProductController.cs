using System;
using System.Net;
using System.Web.Http;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    /// <summary>
    /// Route class for ProductController on the basis of Product Id
    /// </summary>
    [RoutePrefix("products/{id}")]
    public class ProductController : ApiController
    {
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>
        /// Product object
        /// </returns>
        [Route]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = Product.GetProductFromId(id);
            if (product != null)
                return product;
            else
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// PUT api to update product's details
        /// </summary>
        /// <param name="id">product id from route</param>
        /// <param name="product">JSON to product object</param>
        /// <returns>
        /// StatusCode for updated or not
        /// </returns>
        [Route]
        [HttpPut]
        public StatusCode Update(Guid id, Product product)
        {
            return Helpers.ResponseMaker(product.Update(id));
        }

        /// <summary>
        /// Delete row on the basis of Product Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>
        /// StatusCode for Deleted or not
        /// </returns>
        [Route]
        [HttpDelete]
        public StatusCode Delete(Guid id)
        {
            return Helpers.ResponseMaker(Product.Delete(id));
        }
    }
}