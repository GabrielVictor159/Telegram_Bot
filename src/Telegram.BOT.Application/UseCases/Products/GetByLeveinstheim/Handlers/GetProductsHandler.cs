using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;

namespace Telegram.BOT.Application.UseCases.Products.GetByLeveinstheim.Handlers
{
    public class GetProductsHandler : Handler<GetByLeveinstheimRequest>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductGroupsRepository productGroupsRepository;

        public GetProductsHandler(IProductRepository productRepository, IProductGroupsRepository productGroupsRepository)
        {
            this.productRepository = productRepository;
            this.productGroupsRepository = productGroupsRepository;
        }

        public override async Task ProcessRequest(GetByLeveinstheimRequest request)
        {
            request.AddLog(Domain.Enums.LogType.Process, "GetProductsHandler");
            var products = new List<Domain.Products.Product>();
            foreach(var group in request.Groups)
            {
                var productGroups = productGroupsRepository.GetByFilter(e=>e.GroupId == group.Id).OrderByDescending(e=>e.Percentage).ToList();
                foreach(var productGroup in  productGroups)
                {
                    var product = productRepository.GetByFilter(e => e.Id == productGroup.ProductId, 1,10).First();
                    products.Add(product);
                    if(products.Count>=request.numberItems)
                    {
                        break;
                    }
                }
                if (products.Count >= request.numberItems)
                {
                    break;
                }
            }
            request.AddLog(Domain.Enums.LogType.Information, $"Find {products.Count} Items");
            request.Products = products;
            if(sucessor!=null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
