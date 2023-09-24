using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Products.DeleteProduct.Handlers;

public class GetProductHandler : Handler<DeleteProductRequest>
{
    public readonly IProductRepository productRepository;
    public readonly INotificationService notificationService;
    public GetProductHandler( IProductRepository productRepository, INotificationService notificationService)
    {
        this.productRepository = productRepository;
        this.notificationService = notificationService;
    }

    public override async Task ProcessRequest(DeleteProductRequest request)
    {
        request.AddLog(LogType.Process, "Executing GetProductHandler");
        request.Product = productRepository.GetByFilter((e => e.Id == request.Id), 1, 10).FirstOrDefault();
        if (request.Product == null) 
        {
            notificationService.AddNotification("Invalid Id", "There is no Product with this Past Id");
            return;
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
