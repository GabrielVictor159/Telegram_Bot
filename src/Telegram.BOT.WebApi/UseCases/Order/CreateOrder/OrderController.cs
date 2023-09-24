using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Order.CreateOrder;

namespace Telegram.BOT.WebApi.UseCases.Order.CreateOrder
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderPresenter presenter;
        private readonly ICreateOrderRequest createOrderUseCase;
        public OrderController(CreateOrderPresenter presenter, ICreateOrderRequest createOrderUseCase)
        {
            this.presenter = presenter;
            this.createOrderUseCase = createOrderUseCase;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            await createOrderUseCase.Execute(new Application.UseCases.Order.CreateOrder.CreateOrderRequest(new Domain.Order.Order() 
            {
                Id = Guid.NewGuid(),
                OrderDate = request.OrderDate,
                TotalOrder = request.TotalOrder
            }));
            return presenter.ViewModel;
        }
    }
}