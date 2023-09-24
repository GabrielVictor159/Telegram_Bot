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
        imagesManagementServices.DeleteImage(request.Product!.Image);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
