using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Marc.CreateMarc.Handlers;

public class SaveMarcHandler : Handler<CreateMarcRequest>
{
    private readonly IMarcRepository marcRepository;
    public SaveMarcHandler(IMarcRepository marcRepository)
    {
        this.marcRepository = marcRepository;
    }

    public override async Task ProcessRequest(CreateMarcRequest request)
    {
        request.AddLog(LogType.Process, "Executing SaveMarcHandler");
        marcRepository.Add(request.marc);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
