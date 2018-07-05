namespace IntegrateBots
{
    using System.Configuration;
    using Autofac;
    using BotAssets;
    using Dialogs;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Internals.Fibers;
    using Microsoft.Bot.Builder.Location;
    using Microsoft.Bot.Builder.Scorables;
    using Microsoft.Bot.Connector;

    public class IntegrateBotsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RootDialog>()
                .As<IDialog<object>>()
                .InstancePerDependency();
           
        }
    }
}