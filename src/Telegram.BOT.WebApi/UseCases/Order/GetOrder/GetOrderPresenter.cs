using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Boundaries;

namespace Telegram.BOT.WebApi.UseCases.Order.GetOrder
{
    public class GetOrderPresenter : IOutputPort<List<Domain.Order.Order>>
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

        public void Standard(List<Domain.Order.Order> output)
        {
            var response = new OrderResponse(output);
            ViewModel = new OkObjectResult(response);
        }


    }
}