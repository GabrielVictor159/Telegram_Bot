using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

namespace Telegram.BOT.Application.UseCases.Products.DeleteProduct.Handlers;

public class DeleteImageProductHandler : Handler<DeleteProductRequest>
{
    private readonly IImagesManagementServices imagesManagementServices;
    public DeleteImageProductHandler(IImagesManagementServices imagesManagementServices)
    {
        this.imagesManagementServices = imagesManagementServices;
    }

    public override async Task ProcessRequest(DeleteProductRequest request)
    {
        request.AddLog(LogType.Process, "Executing DeleteImageProductHandler");
        var result = imagesManagementServices.DeleteImage(request.Product!.Image);
        if(result == false)
        {
            request.AddLog(LogType.Error, $"Image: ${request.Product.Image} not found");
        }
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
