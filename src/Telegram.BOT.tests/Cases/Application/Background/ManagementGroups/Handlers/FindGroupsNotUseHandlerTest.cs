using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Backgrund.ManagementGroups.Handlers;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Background.ManagementGroups.Handlers;
[UseAutofacTestFramework]
public class FindGroupsNotUseHandlerTest
{
    private readonly IGroupsRepository groupsRepository;
    private readonly FindGroupsNotUseHandler findGroupsNotUseHandler;

    public FindGroupsNotUseHandlerTest
        (IGroupsRepository groupsRepository,
        FindGroupsNotUseHandler findGroupsNotUseHandler)
    {
        this.groupsRepository = groupsRepository;
        this.findGroupsNotUseHandler = findGroupsNotUseHandler;
    }
    [Fact]
    public void ShouldSucess()
    {
        groupsRepository.Add
            (
            GroupsBuilder.New()
            .WithTags("dasdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd")
            .Build());
        findGroupsNotUseHandler.Process().Should().NotBeNullOrEmpty();
    }
}
