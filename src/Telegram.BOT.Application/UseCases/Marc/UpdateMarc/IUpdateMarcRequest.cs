using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Marc.UpdateMarc
{
    public interface IUpdateMarcRequest
    {
        Task Execute(UpdateMarcRequest request);
    }
}
