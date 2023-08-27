using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.WebApi.UseCases.Order.RemoveOrder;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Boundaries;

namespace Telegram.BOT.WebApi.UseCases.Order.RemoveOrder
{
    public class RemoveOrderPresenter : IOutputPort<string>
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

        public void Standard(string output)
        {
            var response = new OrderResponse(output);
            ViewModel = new OkObjectResult(response);
        }


    }
}