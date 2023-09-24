using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Boundaries.Category;

namespace Telegram.BOT.WebMVC.UseCases.Category.GetCategory
{
    public class CategoryPresenter : IOutputPort<GetCategoryOutput>
    {
        public IActionResult ViewModel { get; private set; } = new ObjectResult(new { StatusCode = 500 });

        public void Error(string message)
        {
            var problemdetails = new ProblemDetails()
            {
                Status = 500,
                Detail = message
            };
            ViewModel = new BadRequestObjectResult(problemdetails);
        }

        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }

        public void Standard(GetCategoryOutput output)
        {
            List<CategoryResponse> list = new();
            list = output.Categories
                    .Select(c => new CategoryResponse(c.Id,c.Name))
                    .ToList();
            this.ViewModel = new OkObjectResult(new GetCategoryResponse { CategoryResponse = list });
        }
    }
}
