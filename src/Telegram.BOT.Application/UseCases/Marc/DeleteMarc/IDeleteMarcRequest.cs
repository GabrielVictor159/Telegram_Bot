using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Marc.DeleteMarc;

public interface IDeleteMarcRequest
{
    Task Execute(DeleteMarcRequest request);
}
