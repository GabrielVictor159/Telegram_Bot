using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Application.UseCases.Products.GetByLeveinstheim.Handlers
{
    public class GetGroupsHandler : Handler<GetByLeveinstheimRequest>
    {
        private readonly IGroupsRepository groupsRepository;

        public GetGroupsHandler(IGroupsRepository groupsRepository)
        {
            this.groupsRepository = groupsRepository;
        }

        public override async Task ProcessRequest(GetByLeveinstheimRequest request)
        {
            request.AddLog(Domain.Enums.LogType.Process, "GetGroupsHandler");
            var entities = groupsRepository.GetByLeveinsthein(request.inputString, 0.70);
            request.Groups = entities;
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
