using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;

public class CreateGroupsHandler : Handler<CreateProductRequest>
{
    private readonly IGroupsRepository groupsRepository;
    private readonly IProbabilityOperations probabilityOperations;
    public CreateGroupsHandler
        (IGroupsRepository groupsRepository, 
        IProbabilityOperations probabilityOperations)
    {
        this.groupsRepository = groupsRepository;
        this.probabilityOperations = probabilityOperations;
    }

    public override async Task ProcessRequest(CreateProductRequest request)
    {
        request.AddLog(LogType.Process, "Executing CreateGroupsHandler");
        var newGroup = new Domain.Products.Groups()
        {
            Id = Guid.NewGuid(),
            Tags = request.Product.Tags,
            CreateDate = DateTime.Now
        };
        request.groups100.Add(newGroup);
        var searchGroups75 = groupsRepository.GetByFilter
            (e =>
            probabilityOperations.CalculateNormalizedLevenshteinDistance(e.Tags, request.Product.Tags) >= 0.75
            );
        request.groups75.AddRange(searchGroups75);
        var searchGroups50 = groupsRepository.GetByFilter
            (e =>
            probabilityOperations.CalculateNormalizedLevenshteinDistance(e.Tags, request.Product.Tags) >= 0.50
            );
        request.groups50.AddRange(searchGroups50);
        var searchGroups25 = groupsRepository.GetByFilter
            (e =>
            probabilityOperations.CalculateNormalizedLevenshteinDistance(e.Tags, request.Product.Tags) >= 0.25
            );
        request.groups25.AddRange(searchGroups25);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
