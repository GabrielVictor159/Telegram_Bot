using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.DeleteProduct;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.WebMVC.UseCases.Products.DeleteProducts
{
    public class ProductsController : Controller
    {

        private readonly IDeleteProductRequest deleteProductRequest;

        public ProductsController(IDeleteProductRequest deleteProductRequest) {
            this.deleteProductRequest = deleteProductRequest;
        }

        public IActionResult Delete(string itemid) {
            var deleteRequest = new Application.UseCases.Products.DeleteProduct.DeleteProductRequest(){ Id = Guid.Parse(itemid) };
            deleteProductRequest.Execute(deleteRequest);
            if (!deleteRequest.IsError && deleteRequest.output != null) {
                return LocalRedirect($"{Environment.GetEnvironmentVariable("URL_PREFIX")??""}/Products");
            } else {
                return View("Error", (deleteRequest.ErrorMessage, "Index"));
            }
        }
    }
}
