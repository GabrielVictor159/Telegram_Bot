using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Services.Jobs.ManagementGroups.Handlers;

public class RemoveGroupsHandler
{
    private readonly IGroupsRepository groupsRepository;
    public RemoveGroupsHandler(IGroupsRepository groupsRepository)
    {
        this.groupsRepository = groupsRepository;
    }
    public (bool,string) Process(List<Groups> groups)
    {
        bool sucess = true;
        string message = "";
        groups.ForEach(group =>
        {
            if (!groupsRepository.Remove(group.Id))
            {
               sucess = false;
               message += $"group with Id {group.Id} could not be removed / ";
            }
        });
        return (sucess,message);
    }
}
