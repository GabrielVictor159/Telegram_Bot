using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Boundaries.Ambient.EnvVariables;

public class GetEnvOutput
{
    public required string Key { get; init; }
    public required string Value { get; init; }
}
