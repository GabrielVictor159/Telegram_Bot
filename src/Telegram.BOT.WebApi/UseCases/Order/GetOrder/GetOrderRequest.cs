using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Telegram.BOT.WebApi.UseCases.Order.GetOrder
{
    [DataContract(Name = "GetOrderRequest")]
    public class GetOrderRequest
    {
        public string Id { get; set; } = "";
        public string OrderDate { get; set; } = "";
        public decimal MaximalOrder { get; set; } = decimal.MaxValue;
        public decimal MinimalOrder { get; set; } = decimal.MinValue;
    }
}