using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Products.CreateProduct;
using Telegram.BOT.Application.UseCases.Products.DeleteProduct;
using Telegram.BOT.Application.UseCases.Products.GetProduct;
using Telegram.BOT.Domain.Products;
using Telegram.BOT.WebMVC.UseCases.Marc.GetMarc;

namespace Telegram.BOT.WebMVC.UseCases.Products.GetProducts
{
    public class ProductsController : Controller
    {

        private readonly IGetProductRequest getProductRequest;
        private readonly ICreateProductRequest createProductRequest;
        private readonly IDeleteProductRequest deleteProductRequest;

        public ProductsController(IGetProductRequest getProductRequest, ICreateProductRequest createProductRequest, IDeleteProductRequest deleteProductRequest) {
            this.getProductRequest = getProductRequest;
            this.createProductRequest = createProductRequest;
            this.deleteProductRequest = deleteProductRequest;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var request = new Application.UseCases.Products.GetProduct.GetProductRequest();
            getProductRequest.Execute(request);

            if (!request.IsError && request.output != null) {
                var products = request.Products
                .Select(c => new ProductResponse(c.Id, c.Name, c.Description, c.Image, c.Tags, c.CreateDate, c.Price, c.Marc!, c.MarcId))
                .ToList();
                return View("Index", new GetProductResponse() { Products = products });
            } else {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Search(GetProductRequest request) {
            throw new NotImplementedException();
        }

    }
}
