using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.UseCases.Order.RemoveOrder;

namespace Telegram.BOT.WebApi.UseCases.Order.RemoveOrder
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly RemoveOrderPresenter presenter;
        private readonly IRemoveOrderRequest removeOrderRequest;
        public OrderController(
         RemoveOrderPresenter presenter,
         IRemoveOrderRequest removeOrderRequest)
        {
            this.presenter = presenter;
            this.removeOrderRequest = removeOrderRequest;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("RemoveOrder")]
        public async Task<IActionResult> GetOrder([FromBody] RemoveOrderRequest orderRequest)
        {
            await removeOrderRequest.Execute(new Application.UseCases.Order.RemoveOrder.RemoveOrderRequest(orderRequest.id));
            return presenter.ViewModel;
        }
    }
}