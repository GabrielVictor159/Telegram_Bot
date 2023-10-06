using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Services.Jobs.ManagementGroups.Handlers;

public class FindGroupsNotUseHandler
{
    private readonly IGroupsRepository groupsRepository;
    private readonly IProductGroupsRepository productGroupsRepository;

    public FindGroupsNotUseHandler(IGroupsRepository groupsRepository, IProductGroupsRepository productGroupsRepository)
    {
        this.groupsRepository = groupsRepository;
        this.productGroupsRepository = productGroupsRepository;
    }

    public List<Domain.Products.Groups> Process()
    {
        var EndList = new List<Domain.Products.Groups>();
        groupsRepository.GetByFilter(e => true).ForEach(group =>
        {
            if (productGroupsRepository.GetByFilter(z => z.GroupId == group.Id).FirstOrDefault() == null)
            {
                EndList.Add(group);
            }
        });
        return EndList;
    }

}
