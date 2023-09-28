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

public class CreateProductGroupsHandler : Handler<CreateProductRequest>
{
    private readonly IProductGroupsRepository productGroupsRepository;
    private readonly IGroupsRepository groupsRepository;
    private readonly IProbabilityOperations probabilityOperations;
    public CreateProductGroupsHandler
        (IProductGroupsRepository productGroupsRepository,
        IGroupsRepository groupsRepository,
        IProbabilityOperations probabilityOperations)
    {
        this.productGroupsRepository = productGroupsRepository;
        this.groupsRepository = groupsRepository;
        this.probabilityOperations = probabilityOperations;
    }

    public override async Task ProcessRequest(CreateProductRequest request)
    {
        request.AddLog(LogType.Process, "Executing CreateProductGroupsHandler");
        var productGroups = new List<ProductGroups>();
        var groups = groupsRepository.GetByLeveinsthein(request.Product.Tags,0.25);
        groups.ForEach(group =>
        {
            var entity = new ProductGroups()
            {
                Id = Guid.NewGuid(),
                GroupId = group.Id,
                Percentage = probabilityOperations.CalculateNormalizedLevenshteinDistance(group.Tags, request.Product.Tags),
                ProductId = request.Product.Id
            };
            if (entity.IsValid)
            {
                productGroups.Add(entity);
            }
        });
        var groupProduct = new Groups() { CreateDate = DateTime.Now, Id = Guid.NewGuid(), Tags=request.Product.Tags };
        productGroups.Add(new() { Group = groupProduct, GroupId = groupProduct.Id, Percentage = 1, ProductId = request.Product.Id});
        productGroupsRepository.AddRange(productGroups);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
