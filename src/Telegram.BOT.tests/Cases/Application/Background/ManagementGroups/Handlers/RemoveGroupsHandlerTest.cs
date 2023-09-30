using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Backgrund.ManagementGroups.Handlers;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Products;
using Telegram.BOT.tests.Builder.Domain.Product;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Background.ManagementGroups.Handlers;
[UseAutofacTestFramework]
public class RemoveGroupsHandlerTest
{
    private readonly IGroupsRepository groupsRepository;
    private readonly RemoveGroupsHandler removeGroupsHandler;

    public RemoveGroupsHandlerTest(IGroupsRepository groupsRepository, RemoveGroupsHandler removeGroupsHandler)
    {
        this.groupsRepository = groupsRepository;
        this.removeGroupsHandler = removeGroupsHandler;
    }
    [Fact]
    public void ShouldSucess()
    {
        var list = new List<Groups>() { GroupsBuilder.New().Build()};
        groupsRepository.AddRange(list);
        removeGroupsHandler.Process(list).Item1.Should().BeTrue();
    }

    [Fact]
    public void ShouldFailure()
    {
        var list = new List<Groups>() { GroupsBuilder.New().Build() };
        removeGroupsHandler.Process(list).Item1.Should().BeFalse();
    }
}
