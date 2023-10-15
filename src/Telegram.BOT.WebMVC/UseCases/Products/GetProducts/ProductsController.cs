using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Marc.GetMarc;
using Telegram.BOT.Application.UseCases.Products.GetProduct;
using Telegram.BOT.Domain.Products;
using Telegram.BOT.WebMVC.UseCases.Marc.GetMarc;

namespace Telegram.BOT.WebMVC.UseCases.Products.GetProducts
{
    public class ProductsController : Controller
    {

        private readonly IGetProductRequest getProductRequest;

        public ProductsController(IGetProductRequest getProductRequest) {
            this.getProductRequest = getProductRequest;
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

        public IActionResult Search(GetProductRequest request) {
            var searchRequest = new Application.UseCases.Products.GetProduct.GetProductRequest();
            getProductRequest.Execute(searchRequest);

            if (!searchRequest.IsError && searchRequest.output != null) {
                var products = searchRequest.Products
                .Select(c => new ProductResponse(c.Id, c.Name, c.Description, c.Image, c.Tags, c.CreateDate, c.Price, c.Marc!, c.MarcId))
                .Where(c => c.name.Contains(request.Name ?? "") && c.tags.Contains(request.Tags ?? "") && c.description.Contains(request.Description ?? ""))
                .Where(c => {
                    if(request.filterEndDate == DateTime.MinValue) {
                        return DateTime.Compare(c.CreateDate, request.filterStartDate) > 0 && DateTime.Compare(c.CreateDate, DateTime.MaxValue) < 0;
                    } else {
                        return DateTime.Compare(c.CreateDate, request.filterStartDate) > 0 && DateTime.Compare(c.CreateDate, request.filterEndDate) < 0;
                    }
                })
                .ToList();
                return View("Index", new GetProductResponse() { Products = products });
            } else {
                return View("Error");
            }
        }

    }
}
