using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.UseCases.Message.CreateMessage.Handlers
{
    public class ValidateDomainHandler : Handler<CreateMessageRequest>
    {
        public override async Task ProcessRequest(CreateMessageRequest request)
        {
            if(!request.Message.IsValid)
            {
                request.ErrorMessage = request.Message.ValidationResult!.ToString();
                request.IsError = true;
                return;
            }
            if (sucessor != null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
