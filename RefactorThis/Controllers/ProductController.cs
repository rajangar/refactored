using System;
using System.Net;
using System.Web.Http;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    [RoutePrefix("products/{id}")]
    public class ProductController : ApiController
    {
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

        [Route]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            product.Update(id);
        }

        [Route]
        [HttpDelete]
        public void Delete(Guid id)
        {
            Product.Delete(id);
        }
    }
}