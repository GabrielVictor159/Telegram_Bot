using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;
public interface ICreateEnvRequest
{
    Task Execute(CreateEnvRequest request);
}
