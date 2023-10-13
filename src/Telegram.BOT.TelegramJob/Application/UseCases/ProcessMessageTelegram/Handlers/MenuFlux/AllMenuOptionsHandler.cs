using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Application.UseCases;

namespace Telegram.BOT.TelegramJob.Application.UseCases.ProcessMessageTelegram.Handlers.MenuFlux
{
    public class AllMenuOptionsHandler : Handler<ProcessMessageTelegramRequest>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMarcRepository marcRepository;

        public AllMenuOptionsHandler(ICategoryRepository categoryRepository, IMarcRepository marcRepository)
        {
            this.categoryRepository = categoryRepository;
            this.marcRepository = marcRepository;
        }

        public override async Task ProcessRequest(ProcessMessageTelegramRequest request)
        {
            var categories = categoryRepository.GetByFilter(e => true, 1, int.MaxValue);
            var marcs = marcRepository.GetByFilter(e => true, 1, int.MaxValue);
            if (categories.Any())
            {
                request.Categories = categories;
            }
            if (marcs.Any())
            {
                request.Marcs = marcs;
            }
            if (sucessor != null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}
