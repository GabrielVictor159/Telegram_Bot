using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Application.Boundaries.Products
{
    public class ProductOutput
    {
        public required Product product { get; init; }
    }
}
