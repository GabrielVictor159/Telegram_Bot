using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Boundaries.Products
{
    public class GetByLeveinstheimOutput
    {
        public required List<Domain.Products.Product> Products { get; init; }
    }
}
