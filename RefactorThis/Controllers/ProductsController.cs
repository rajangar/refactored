using System.Web.Http;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        [Route]
        [HttpGet]
        public Products GetAll()
        {
            return Products.GetAllProducts();
        }

        [Route]
        [HttpGet]
        public Products SearchByName(string name)
        {
            return Products.GetProductsByName(name);
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            product.Create();
        }
    }
}
