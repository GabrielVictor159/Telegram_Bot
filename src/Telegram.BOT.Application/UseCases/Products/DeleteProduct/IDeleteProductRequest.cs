using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Products.GetProduct;

namespace Telegram.BOT.Application.UseCases.Products.DeleteProduct;

public interface IDeleteProductRequest
{
    Task Execute(DeleteProductRequest request);
}
