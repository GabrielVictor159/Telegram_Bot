﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Enums;

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
        request.AddLog(LogType.Process, "Executing SaveImageHandler");
        request.Product.Image = imagesManagementServices.SaveImage(request.Image);
        if (sucessor != null)
        {
            await sucessor.ProcessRequest(request);
        }
    }
}
