using ManagementServices.variables.Application.Interfaces;
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
        private readonly IEnvVariableRepository envVariableRepository;

        public GetGroupsHandler(IGroupsRepository groupsRepository,
            IEnvVariableRepository envVariableRepository)
        {
            this.groupsRepository = groupsRepository;
            this.envVariableRepository = envVariableRepository;
        }

        public override async Task ProcessRequest(GetByLeveinstheimRequest request)
        {
            var precison = envVariableRepository.Get("PRODUCT_PRECISION_SEARCH");
            request.AddLog(Domain.Enums.LogType.Process, "GetGroupsHandler");
            var entities = groupsRepository.GetByLeveinsthein(request.inputString, precison==null?0.50:double.Parse(precison.Value));
            request.Groups = entities;
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
