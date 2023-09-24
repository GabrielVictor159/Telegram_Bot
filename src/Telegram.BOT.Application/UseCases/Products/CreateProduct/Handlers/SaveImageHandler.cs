using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.Application.UseCases.Products.CreateProduct.Handlers;

public class SaveImageHandler : Handler<CreateProductRequest>
{
    private readonly IImagesManagementServices imagesManagementServices;
    public SaveImageHandler(IImagesManagementServices imagesManagementServices)
    {
        this.imagesManagementServices = imagesManagementServices;
    }

    public override async Task ProcessRequest(CreateProductRequest request)
    {
        request.Product.Image = imagesManagementServices.SaveImage(request.Image);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
