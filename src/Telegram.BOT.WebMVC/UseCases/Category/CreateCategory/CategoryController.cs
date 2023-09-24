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
        private readonly ICreateCategoryRequest createCategoryRequest;
        public CategoryController(
           ICreateCategoryRequest createCategoryRequest)
        {
            this.createCategoryRequest = createCategoryRequest;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAction(CreateCategoryRequest request)
        {
            var requestUseCase =
                new Application.UseCases.Category.CreateCategory.CreateCategoryRequest()
                {
                    category = new Domain.Products.Category() { Id = Guid.NewGuid(), Name = request.name }
                };
            createCategoryRequest.Execute(requestUseCase);
            if(!requestUseCase.IsError && requestUseCase.output!=null)
            {
                return View("Index");
            }
            else
            {
                return View("Error");
            }
           
        }
     
    }
}
