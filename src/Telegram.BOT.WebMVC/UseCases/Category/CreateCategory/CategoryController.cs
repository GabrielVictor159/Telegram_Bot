using FluentValidator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using Telegram.BOT.Application.Boundaries.Category;
using Telegram.BOT.Application.UseCases.Category.CreateCategory;

namespace Telegram.BOT.WebMVC.UseCases.Category.CreateCategory
{
    public class CategoryController : Controller
    {
        private readonly CategoryPresenter presenter;
        private readonly ICreateCategoryRequest createCategoryRequest;
        public CategoryController(
           CategoryPresenter presenter,
           ICreateCategoryRequest createCategoryRequest)
        {
            this.presenter = presenter;
            this.createCategoryRequest = createCategoryRequest;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAction(CreateCategoryRequest request)
        {
            createCategoryRequest.Execute(
                new Application.UseCases.Category.CreateCategory.CreateCategoryRequest() { category = new Domain.Products.Category() { Id = Guid.NewGuid(), Name = request.name } });
            if(presenter.ViewModel is OkObjectResult okObjectResult && okObjectResult.Value is CreateCategoryResponse response)
            {
                return View("Create", "CreateCategory");
            }
            else
            {
                return View("Error");
            }
           
        }
     
    }
}
