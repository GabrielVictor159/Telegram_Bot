using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Marc.GetMarc.Handlers;

public class GetMarcHandler : Handler<GetMarcRequest>
{
    private readonly IMarcRepository MarcRepository;
    public GetMarcHandler(IMarcRepository MarcRepository)
    { 
        this.MarcRepository = MarcRepository; 
    }
    public override async Task ProcessRequest(GetMarcRequest request)
    {
        request.AddLog(LogType.Process, "Executing GetMarcHandler");
        request.Marcs.AddRange(MarcRepository.GetByFilter((e =>
        e.Name.ToLower().Contains(request.Name.ToLower()) &&
        request.CategoryId != Guid.Empty ?
        e.CategoryId == request.CategoryId : true
        ),request.page,request.pageSize));
        if (sucessor != null)
            await sucessor.ProcessRequest(request);
    }
}
