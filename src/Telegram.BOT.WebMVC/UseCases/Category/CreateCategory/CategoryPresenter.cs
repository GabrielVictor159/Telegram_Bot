using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Boundaries.Category;

namespace Telegram.BOT.WebMVC.UseCases.Category.CreateCategory
{
    public class CategoryPresenter : IOutputPort<SaveCategoryOutput>
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

        public void Standard(SaveCategoryOutput output)
        {
            this.ViewModel = new OkObjectResult(new CreateCategoryResponse() { Category=new(output.Category.Id,output.Category.Name)});
        }
    }
}
