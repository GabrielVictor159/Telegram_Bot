using Autofac;

namespace Telegram.BOT.WebMVC.Modules
{
    public class WebMVCModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UseCases.Category.GetCategory.CategoryPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<UseCases.Category.CreateCategory.CategoryPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
        }
    }
}
