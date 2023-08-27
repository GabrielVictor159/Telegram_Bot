using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.WebApi.Filters;

namespace Telegram.BOT.WebApi.DependencyInjection
{
    public static class FiltersExtensions
    {
        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(NotificationFilter));
            });
            return services;
        }
    }
}