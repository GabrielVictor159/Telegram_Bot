using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Telegram.BOT.Application.UseCases.Category.GetCategory;
using Telegram.BOT.Application.UseCases.Category.UpdateCategory;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.WebMVC.UseCases.Category.UpdateCategory {
    public class CategoryController : Controller {

        private readonly IGetCategoryRequest getCategoryRequest;
        private readonly IUpdateCategoryRequest updateCategoryRequest;

        public CategoryController(IGetCategoryRequest getCategoryRequest, IUpdateCategoryRequest updateCategoryRequest) {
            this.getCategoryRequest = getCategoryRequest;
            this.updateCategoryRequest = updateCategoryRequest;
        }

        public IActionResult Edit(string itemid) {
            var getRequest = new Application.UseCases.Category.GetCategory.GetCategoryRequest() { Name = itemid };
            getCategoryRequest.Execute(getRequest);
            if (!getRequest.IsError && getRequest.output != null) {
                return View(getRequest.output.Categories.FirstOrDefault());
            } else {
                return View("Error", (getRequest.ErrorMessage, "Index"));
            }
        }

        [HttpPost]
        public ActionResult EditAction(Telegram.BOT.Domain.Products.Category categoria) {
            var updateRequest = new Application.UseCases.Category.UpdateCategory.UpdateCategoryRequest() { Category = categoria };
            updateCategoryRequest.Execute(updateRequest);
            if (!updateRequest.IsError && updateRequest.output != null) {
                return LocalRedirect($"{Environment.GetEnvironmentVariable("URL_PREFIX") ?? ""}/Category");
            } else {
                return View("Error", (updateRequest.ErrorMessage, "Index"));
            }
        }
    }
}
