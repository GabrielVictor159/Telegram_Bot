using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Category.DeleteCategory;

namespace Telegram.BOT.WebMVC.UseCases.Category.DeleteCategory {
    public class CategoryController : Controller {

        private readonly IDeleteCategoryRequest deleteCategoryRequest;

        public CategoryController(IDeleteCategoryRequest deleteCategoryRequest) {
            this.deleteCategoryRequest = deleteCategoryRequest;
        }

        public IActionResult Delete(string itemid) {
            var deleteRequest = new Application.UseCases.Category.DeleteCategory.DeleteCategoryRequest() { Id = Guid.Parse(itemid) };
            
            deleteCategoryRequest.Execute(deleteRequest);
            if(!deleteRequest.IsError && deleteRequest.output != null) {
                return LocalRedirect("/Category");
            } else {
                return View("Error", (deleteRequest.ErrorMessage, "Index"));
            }
        }
    }
}
