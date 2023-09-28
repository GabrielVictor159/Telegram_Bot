using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.Application.Backgorund.ManagementImages.Handlers;

public class RemoveImageHandler
{
    private readonly IImagesManagementServices imagesManagementServices;

    public RemoveImageHandler(IImagesManagementServices imagesManagementServices)
    {
        this.imagesManagementServices = imagesManagementServices;
    }
    public (bool,string) Process(string name)
    {
        var result = imagesManagementServices.DeleteImage(name);
        return (result, result==false?$"Image: ${Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")}/${name} not found":"");
    }
}
