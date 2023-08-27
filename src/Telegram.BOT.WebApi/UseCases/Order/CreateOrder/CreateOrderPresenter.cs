using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Boundaries;
using Telegram.BOT.Application.Boundaries.Order;

namespace Telegram.BOT.WebApi.UseCases.Order.CreateOrder
{
    public class CreateOrderPresenter : IOutputPort<OrderOutput>
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

        public void Standard(OrderOutput output)
        {
            var response = new OrderResponse(output.id);
            ViewModel = new OkObjectResult(response);
        }
    }
}