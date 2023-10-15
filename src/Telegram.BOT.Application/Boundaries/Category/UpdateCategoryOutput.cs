using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Boundaries.Category
{
    public class UpdateCategoryOutput
    {
        public required Domain.Products.Category Category { get; init; }
    }
}
