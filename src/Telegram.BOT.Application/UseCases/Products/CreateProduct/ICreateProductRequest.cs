using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Products.CreateProduct;

public interface ICreateProductRequest
{
     Task Execute(CreateProductRequest request);
}
