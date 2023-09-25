using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Category.GetCategory;
using Telegram.BOT.Application.UseCases.Marc.GetMarc;
using Telegram.BOT.WebMVC.UseCases.Category.GetCategory;

namespace Telegram.BOT.WebMVC.UseCases.Marc.GetMarc
{
    public class MarcController : Controller
    {
        private readonly IGetMarcRequest getMarcRequest;
        private readonly IGetCategoryRequest getCategoryRequest;
        private List<CategoryResponse> categories = new();
        public MarcController(
           IGetMarcRequest getMarcRequest,
           IGetCategoryRequest getCategoryRequest)
        {
            this.getMarcRequest = getMarcRequest;
            this.getCategoryRequest = getCategoryRequest;
            var requestCategories = new Application.UseCases.Category.GetCategory.GetCategoryRequest() { pageSize = int.MaxValue };
            getCategoryRequest.Execute(requestCategories);
            if(requestCategories.output!=null)
            this.categories = requestCategories.output.Categories
                .Select(c => new CategoryResponse(c.Id, c.Name))
                .ToList();
        }
        public IActionResult Index()
        {
            var requestMarc = new Application.UseCases.Marc.GetMarc.GetMarcRequest();
            getMarcRequest.Execute(requestMarc);
            if (!requestMarc.IsError && requestMarc.output!=null)
            {
                var itemsMarc = requestMarc.output.Marcs
                .Select(c => new MarcResponse(c.Id, c.Name, c.Category!, c.CategoryId))
                .ToList();
                return View("Index", new GetMarcResponse() { Categories= categories, Marcs = itemsMarc});
            }
            else
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Search(GetMarcRequest request)
        {
            var requestMarc = new Application.UseCases.Marc.GetMarc.GetMarcRequest() { Name = request.Name, CategoryId = request.CategoryId};
            getMarcRequest.Execute(requestMarc);
            if (!requestMarc.IsError && requestMarc.output != null)
            {
                var itemsMarc = requestMarc.output.Marcs
                .Select(c => new MarcResponse(c.Id, c.Name, c.Category!, c.CategoryId))
                .ToList();
                return View("Index", new GetMarcResponse() { Categories = categories, Marcs = itemsMarc });
            }
            else
            {
                return View("Error");
            }

        }

        
    }
}
