using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Category.GetCategory;
using Telegram.BOT.Application.UseCases.Marc.GetMarc;
using Telegram.BOT.Application.UseCases.Marc.UpdateMarc;
using Telegram.BOT.WebMVC.UseCases.Category.GetCategory;

namespace Telegram.BOT.WebMVC.UseCases.Marc.UpdateMarc {
    public class MarcController : Controller {
        private readonly IGetMarcRequest getMarcRequest;
        private readonly IGetCategoryRequest getCategoryRequest;
        private readonly IUpdateMarcRequest updateMarcRequest;
        private List<CategoryResponse> categories;

        public MarcController(IGetMarcRequest getMarcRequest, IUpdateMarcRequest updateMarcRequest, IGetCategoryRequest getCategoryRequest) {
            this.getMarcRequest = getMarcRequest;
            this.updateMarcRequest = updateMarcRequest;
            this.getCategoryRequest = getCategoryRequest;
            var requestCategories = new Application.UseCases.Category.GetCategory.GetCategoryRequest() { pageSize = int.MaxValue };
            getCategoryRequest.Execute(requestCategories);
            if (requestCategories.output != null)
                this.categories = requestCategories.output.Categories
                    .Select(c => new CategoryResponse(c.Id, c.Name))
                    .ToList();
        }

        public IActionResult Edit(string itemid) {
            var getRequest = new Application.UseCases.Marc.GetMarc.GetMarcRequest() { Name = itemid };
            getMarcRequest.Execute(getRequest);
            if (!getRequest.IsError && getRequest.output != null) {
                return View(new UpdateMarcRequest {
                    marc = getRequest.output.Marcs.FirstOrDefault(),
                    categories = this.categories
                });
            } else {
                return View("Error", (getRequest.ErrorMessage, "Index"));
            }
        }

        [HttpPost]
        public ActionResult EditAction(Telegram.BOT.Domain.Products.Marc marc) {
            var updateRequest = new Application.UseCases.Marc.UpdateMarc.UpdateMarcRequest() { Marc = marc };
            updateMarcRequest.Execute(updateRequest);
            if (!updateRequest.IsError && updateRequest.output != null) {
                return LocalRedirect($"{Environment.GetEnvironmentVariable("URL_PREFIX") ?? ""}/Marc");
            } else {
                return View("Error", (updateRequest.ErrorMessage, "Index"));
            }
        }
    }
}

