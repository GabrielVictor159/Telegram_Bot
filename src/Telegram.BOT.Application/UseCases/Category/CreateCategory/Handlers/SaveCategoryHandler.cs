using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Category.CreateCategory.Handlers;

public class SaveCategoryHandler : Handler<CreateCategoryRequest>
{
    private readonly ICategoryRepository categoryRepository;
    public SaveCategoryHandler(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }

    public override async Task ProcessRequest(CreateCategoryRequest request)
    {
        request.AddLog(LogType.Process, "Executing SaveCategoryHandler");
        categoryRepository.Add(request.category);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
