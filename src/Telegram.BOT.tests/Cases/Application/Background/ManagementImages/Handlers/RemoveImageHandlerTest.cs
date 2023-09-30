using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Background.ManagementImages.Handlers;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Domain.Logs;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace Telegram.BOT.tests.Cases.Application.Background.ManagementImages.Handlers;
[UseAutofacTestFramework]
public class RemoveImageHandlerTest
{
    private readonly IImagesManagementServices imagesManagementServices;
    private readonly RemoveImageHandler removeImageHandler;

    public RemoveImageHandlerTest
        (IImagesManagementServices imagesManagementServices, 
        RemoveImageHandler removeImageHandler)
    {
        this.imagesManagementServices = imagesManagementServices;
        this.removeImageHandler = removeImageHandler;
    }
    [Fact]
    public void ShouldSucess()
    {
        byte[] image = System.Text.Encoding.UTF8.GetBytes(MessageLogs.GabrielSymbol());
        string nameImage = imagesManagementServices.SaveImage(image);
        removeImageHandler.Process(nameImage);
        var imagePath = Path.Combine(Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")!, nameImage);
        File.Exists(imagePath).Should().BeFalse();
    }
}
