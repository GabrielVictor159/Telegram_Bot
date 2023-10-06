using ManagementServices.variables.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter.Handlers;

public class GetEnvVariablesHandler : Handler<GetEnvByFilterRequest>
{
    private readonly IEnvVariableRepository envVariableRepository;

    public GetEnvVariablesHandler(IEnvVariableRepository envVariableRepository)
    {
        this.envVariableRepository = envVariableRepository;
    }

    public override async Task ProcessRequest(GetEnvByFilterRequest request)
    {
        request.variablesFound = envVariableRepository.GetByFilter
            ((
            e => e.Key.ToLower().Contains(request.Key.ToLower()) &&
            e.Value.ToLower().Contains(request.Value.ToLower())
            ),request.Page,request.PageSize);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
