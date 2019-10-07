using System;
using System.Web.Http;
using refactor_this.Models;

namespace refactor_this.Controllers
{
    [RoutePrefix("products/{productId}/options/{id}")]
    public class ProductOptionController : ApiController
    {
        [Route]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            return ProductOptions.GetProductOptionsFromId(productId, id);
        }

        [Route]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            option.Update(id);
        }

        [Route]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            ProductOption.Delete(id);
        }
    }
}