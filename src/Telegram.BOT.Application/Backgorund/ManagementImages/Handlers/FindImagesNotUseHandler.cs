using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Application.Backgorund.ManagementImages.Handlers;

public class FindImagesNotUseHandler
{
    private readonly IProductRepository productRepository;
    public FindImagesNotUseHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    public List<string> Process()
    {
        var list = new List<string>();
        string[] listAll = Directory.GetFiles(Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")!, "*.png");
        foreach (string file in listAll )
        {
            var product = productRepository.GetByFilter(e => e.Image == file,1,10);
            if (product.Count==0)
            {
                list.Add(file);
            }
        }
        return list;
    }
}
