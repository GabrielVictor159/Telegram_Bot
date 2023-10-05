using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnvByFilter;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Logs;

namespace Telegram.BOT.Application.UseCases.Ambient.EnvVariables.RemoveEnv
{
    public interface IRemoveEnvRequest
    {
        void Execute(RemoveEnvRequest request);
    }
}
