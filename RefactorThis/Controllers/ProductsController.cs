using System.Web.Http;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    /// <summary>
    /// Controller for Products REST APIs
    /// </summary>
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        /// <summary>
        /// To get all the products list
        /// </summary>
        /// <returns>
        /// Products class's object which will convert into JSON
        /// </returns>
        [Route]
        [HttpGet]
        public Products GetAll()
        {
            return Products.GetAllProducts();
        }

        /// <summary>
        /// To get the products list by name
        /// </summary>
        /// <param name="name">nae to search</param>
        /// <returns>
        /// Products class's object which will convert into JSON
        /// </returns>
        [Route]
        [HttpGet]
        public Products SearchByName(string name)
        {
            return Products.GetProductsByName(name);
        }

        /// <summary>
        /// To create new product in database
        /// </summary>
        /// <param name="product"> Product object</param>
        /// <returns>
        /// StatusCode which will convert into JSON
        /// </returns>
        [Route]
        [HttpPost]
        public StatusCode Create(Product product)
        {
            return Helpers.ResponseMaker(product.Create());
        }
    }
}
