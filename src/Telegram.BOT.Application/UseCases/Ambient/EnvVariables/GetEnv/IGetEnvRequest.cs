using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv;

public interface IGetEnvRequest
{
    Task Execute(GetEnvRequest request);
}
