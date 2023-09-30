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
public class FindImagesNotUseHandlerTest
{
    private readonly IImagesManagementServices imagesManagementServices;
    private readonly FindImagesNotUseHandler findImagesNotUseHandler;

    public FindImagesNotUseHandlerTest
        (IImagesManagementServices imagesManagementServices, 
        FindImagesNotUseHandler findImagesNotUseHandler)
    {
        this.imagesManagementServices = imagesManagementServices;
        this.findImagesNotUseHandler = findImagesNotUseHandler;
    }
    [Fact]
    public void ShouldSucess()
    {
        byte[] image = System.Text.Encoding.UTF8.GetBytes(MessageLogs.GabrielSymbol());
        imagesManagementServices.SaveImage(image);
        findImagesNotUseHandler.Process().Should().NotBeNullOrEmpty();
    }
}
