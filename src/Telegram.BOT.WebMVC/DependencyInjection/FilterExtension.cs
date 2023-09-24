using Telegram.BOT.WebMVC.Filters;

namespace Telegram.BOT.WebMVC.DependencyInjection
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
