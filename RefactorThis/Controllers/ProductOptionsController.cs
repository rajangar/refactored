using System;
using System.Web.Http;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    [RoutePrefix("products/{productId}/options")]
    public class ProductOptionsController : ApiController
    {
        [Route]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            return ProductOptions.GetAllProductOptions(productId);
        }

        [Route]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            option.Create();
        }
    }
}