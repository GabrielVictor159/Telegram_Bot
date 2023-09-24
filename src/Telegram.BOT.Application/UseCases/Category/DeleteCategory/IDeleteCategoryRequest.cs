using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Category.DeleteCategory;

public interface IDeleteCategoryRequest
{
    Task Execute(DeleteCategoryRequest request);
}
