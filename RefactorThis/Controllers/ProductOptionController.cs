using System;
using System.Web.Http;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    /// <summary>
    /// Route class for product option
    /// </summary>
    [RoutePrefix("products/{productId}/options/{id}")]
    public class ProductOptionController : ApiController
    {
        /// <summary>
        /// Get option by productid and optionid
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="id">Option Id</param>
        /// <returns>
        /// ProductOption object
        /// </returns>
        [Route]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            return ProductOptions.GetProductOptionsFromId(productId, id);
        }

        /// <summary>
        /// Update Product option by id
        /// </summary>
        /// <param name="id">option id</param>
        /// <param name="option">JSON to ProductOption object</param>
        /// <returns>
        /// StatusCode object which tells if upated or not
        /// </returns>
        [Route]
        [HttpPut]
        public StatusCode UpdateOption(Guid id, ProductOption option)
        {
            return Helpers.ResponseMaker(option.Update(id));
        }

        /// <summary>
        /// To delete a product option by id
        /// </summary>
        /// <param name="id">option id</param>
        /// <returns>
        /// StatusCode object which tells if deleted or not
        /// </returns>
        [Route]
        [HttpDelete]
        public StatusCode DeleteOption(Guid id)
        {
            return Helpers.ResponseMaker(ProductOption.Delete(id));
        }
    }
}