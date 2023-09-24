using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Category.GetCategory.Handlers;

public class GetCategoryHandler : Handler<GetCategoryRequest>
{
    private readonly ICategoryRepository categoryRepository;
    public GetCategoryHandler(ICategoryRepository categoryRepository)
    { 
        this.categoryRepository = categoryRepository; 
    }
    public override async Task ProcessRequest(GetCategoryRequest request)
    {
        request.AddLog(LogType.Process, "Executing GetCategoryHandler");
        request.Categories.AddRange(categoryRepository.GetByFilter(e =>e.Name.ToLower().Contains(request.Name.ToLower()),request.page,request.pageSize));
        if (sucessor != null)
            await sucessor.ProcessRequest(request);
    }
}
