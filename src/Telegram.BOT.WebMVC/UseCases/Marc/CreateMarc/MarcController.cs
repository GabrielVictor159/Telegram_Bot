using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Category.GetCategory;
using Telegram.BOT.Application.UseCases.Marc.CreateMarc;
using Telegram.BOT.WebMVC.UseCases.Category.GetCategory;

namespace Telegram.BOT.WebMVC.UseCases.Marc.CreateMarc
{
    public class MarcController : Controller
    {
        private readonly ICreateMarcRequest createMarcRquest;
        private readonly IGetCategoryRequest getCategoryRequest;

        public MarcController(ICreateMarcRequest createMarcRequest, IGetCategoryRequest getCategoryRequest) {
            this.createMarcRquest = createMarcRequest;
            this.getCategoryRequest = getCategoryRequest;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var request = new Application.UseCases.Category.GetCategory.GetCategoryRequest() { Name = "" };
            getCategoryRequest.Execute(request);
            if (!request.IsError && request.output != null) {
                var items = request.output.Categories
                .Select(c => new CategoryResponse(c.Id, c.Name))
                .ToList();

                ViewData["items"] = items;

                return View();

            } else {
                return View("Error", (request.ErrorMessage, "Index"));
            }
        }

        [HttpPost]
        public IActionResult CreateAction(CreateMarcRequest request) {
            
            var createRequest = new Application.UseCases.Marc.CreateMarc.CreateMarcRequest() {
                marc = new Domain.Products.Marc() {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    CategoryId = request.CategoryId
                }
            };
            createMarcRquest.Execute(createRequest);
            if (!createRequest.IsError && createRequest.output != null) {
                return LocalRedirect("/Marc");
            } else {
                return View("Error", (createRequest.ErrorMessage, nameof(Create)));
            }
        }
    }
}
