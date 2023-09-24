using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Telegram.BOT.Application.UseCases.Category.GetCategory;

namespace Telegram.BOT.WebMVC.UseCases.Category.GetCategory
{
    public class CategoryController : Controller
    {
        private readonly CategoryPresenter presenter;
        private readonly IGetCategoryRequest getCategoryRequest;
        public CategoryController(
           CategoryPresenter presenter,
           IGetCategoryRequest getCategoryRequest)
        {
            this.presenter = presenter;
            this.getCategoryRequest = getCategoryRequest;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Search(GetCategoryRequest request)
        {
            getCategoryRequest.Execute(
                new Application.UseCases.Category.GetCategory.GetCategoryRequest() { Name = request.name});
            if(presenter.ViewModel is OkObjectResult okObjectResult && okObjectResult.Value is GetCategoryResponse response)
            {
                return View("Index", response);
            }
            else
            {
                return View("Error");
            }
           
        }
     
    }
}
