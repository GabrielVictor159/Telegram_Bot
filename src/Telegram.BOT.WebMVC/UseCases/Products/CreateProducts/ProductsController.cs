using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases.Category.CreateCategory;
using Telegram.BOT.Application.UseCases.Marc.GetMarc;
using Telegram.BOT.Application.UseCases.Products.CreateProduct;
using Telegram.BOT.Domain.Products;
using Telegram.BOT.WebMVC.Helpers;

namespace Telegram.BOT.WebMVC.UseCases.Products.CreateProducts
{
    public class ProductsController : Controller
    {

        private readonly ICreateProductRequest createProductRequest;
        private readonly IGetMarcRequest getMarcRequest;

        public ProductsController(ICreateProductRequest createCategoryRequest, IGetMarcRequest getMarcRequest) {
            this.createProductRequest = createCategoryRequest;
            this.getMarcRequest = getMarcRequest;
        }

        public IActionResult Create() 
        {
            var requestMarcs = new Application.UseCases.Marc.GetMarc.GetMarcRequest();
            getMarcRequest.Execute(requestMarcs);
            if(!requestMarcs.IsError && requestMarcs.output != null) {
                ViewData["itens"] = requestMarcs.Marcs;
                return View();
            } else {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult CreateAction(CreateProductRequest request) 
        {
            var createRequest = new Application.UseCases.Products.CreateProduct.CreateProductRequest() {
                Product = new Product() {
                    Id = Guid.NewGuid(),
                    Tags = request.Tags,
                    Name = request.Name,
                    Price = request.Price,
                    Description = request.Description,
                    MarcId = request.MarcId,
                    CreateDate = DateTime.Now
                },
                Image = RequestExtensions.ConvertIFormFileToByteArray(request.Image)
            };

            createProductRequest.Execute(createRequest);
            if(!createRequest.IsError && createRequest.output != null) {
                return LocalRedirect("/Products");
            }else {
                return View("Error", (createRequest.ErrorMessage, nameof(Create)));
            }
        }
    }
}
