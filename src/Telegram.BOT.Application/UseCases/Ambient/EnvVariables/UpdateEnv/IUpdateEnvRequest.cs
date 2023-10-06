using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.RemoveEnv;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.UpdateEnv
{
    public interface IUpdateEnvRequest
    {
        void Execute(UpdateEnvRequest request);
    }
}
