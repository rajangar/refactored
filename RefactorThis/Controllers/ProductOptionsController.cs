using System;
using System.Web.Http;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    /// <summary>
    /// Route class for options
    /// </summary>
    [RoutePrefix("products/{productId}/options")]
    public class ProductOptionsController : ApiController
    {
        /// <summary>
        /// Get all product options
        /// </summary>
        /// <param name="productId">productid from the route</param>
        /// <returns>
        /// ProductOptions object
        /// </returns>
        [Route]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            return ProductOptions.GetAllProductOptions(productId);
        }

        /// <summary>
        /// To create new product option in database
        /// </summary>
        /// <param name="productId">productid from the route</param>
        /// <param name="option">Complete option converted from json string</param>
        /// <returns>
        /// StatusCode which will convert into JSON
        /// </returns>
        [Route]
        [HttpPost]
        public StatusCode CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            return Helpers.ResponseMaker(option.Create());
        }
    }
}