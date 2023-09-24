using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Order.CreateOrder;

namespace Telegram.BOT.Application.UseCases.Products.CreateProduct;

public interface ICreateProductRequest
{
     Task Execute(CreateProductRequest request);
}
