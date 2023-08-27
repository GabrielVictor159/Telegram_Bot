using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Order.GetOrder;

namespace Telegram.BOT.WebApi.UseCases.Order.GetOrder
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly GetOrderPresenter presenter;
        private readonly IGetOrderRequest getOrderRequest;
        public OrderController(
         GetOrderPresenter presenter,
         IGetOrderRequest getOrderRequest)
        {
            this.presenter = presenter;
            this.getOrderRequest = getOrderRequest;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("GetOrder")]
        public async Task<IActionResult> GetOrder([FromBody] GetOrderRequest orderRequest)
        {
            Expression<Func<Domain.Order.Order, bool>> func = p => p.Id.ToString().ToLower().Contains(orderRequest.Id.ToLower()) &&
            p.OrderDate.ToString().ToLower().Contains(orderRequest.OrderDate.ToLower()) &&
            p.TotalOrder <= orderRequest.MaximalOrder &&
            p.TotalOrder >= orderRequest.MinimalOrder;
            await getOrderRequest.Execute(new Application.UseCases.Order.GetOrder.GetOrderRequest(func));
            return presenter.ViewModel;
        }
    }

}