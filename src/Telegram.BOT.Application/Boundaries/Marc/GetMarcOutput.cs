using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Application.Boundaries.Marc;

public class GetMarcOutput
{
    public required List<Domain.Products.Marc> Marcs { get; init; }
}
