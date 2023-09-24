using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Marc.CreateMarc;

public interface ICreateMarcRequest
{
    Task Execute(CreateMarcRequest request);
}
