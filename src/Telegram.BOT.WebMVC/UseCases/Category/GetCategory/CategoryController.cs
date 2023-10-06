using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Telegram.BOT.Application.UseCases.Category.GetCategory;

namespace Telegram.BOT.WebMVC.UseCases.Category.GetCategory
{
    public class CategoryController : Controller
    {
        private readonly IGetCategoryRequest getCategoryRequest;
        public CategoryController(
           IGetCategoryRequest getCategoryRequest)
        {
            this.getCategoryRequest = getCategoryRequest;
        }
        public IActionResult Index()
        {
            var request =
                 new Application.UseCases.Category.GetCategory.GetCategoryRequest();
            getCategoryRequest.Execute(request);
            if (!request.IsError && request.output!=null)
            {
                var items = request.output.Categories
                .Select(c => new CategoryResponse(c.Id, c.Name))
                .ToList();
                return View("Index", new GetCategoryResponse { CategoryResponse = items });
                
            }
            else
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Search(GetCategoryRequest request)
        {
            var requestUseCase =
                new Application.UseCases.Category.GetCategory.GetCategoryRequest() { Name = request.name };
            getCategoryRequest.Execute(requestUseCase);
            if(!requestUseCase.IsError && requestUseCase.output != null)
            {
                var items = requestUseCase.output.Categories
                .Select(c => new CategoryResponse(c.Id, c.Name))
                .ToList();
                return View("Index", new GetCategoryResponse { CategoryResponse = items });
            }
            else
            {
                return View("Error");
            }
           
        }
     
    }
}
