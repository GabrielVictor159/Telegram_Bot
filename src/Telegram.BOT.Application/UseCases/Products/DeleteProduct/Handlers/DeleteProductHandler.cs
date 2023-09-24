using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Products.DeleteProduct.Handlers;

public class DeleteProductHandler : Handler<DeleteProductRequest>
{
    private readonly IProductRepository productRepository;
    private readonly INotificationService notificationService;
    public DeleteProductHandler(IProductRepository productRepository, INotificationService notificationService)
    {
        this.productRepository = productRepository;
        this.notificationService = notificationService;
    }

    public override async Task ProcessRequest(DeleteProductRequest request)
    {
        request.AddLog(LogType.Process, "Executing DeleteProductHandler");
        var sucess = productRepository.Delete(request.Product!.Id);
        if(!sucess)
        {
            notificationService.AddNotification("Delete Problem", "Unable to delete product");
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
