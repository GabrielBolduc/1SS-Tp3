using Autofac;
using Autofac.Configuration;
using GestionBanque.Views;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace GestionBanque
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("autofac.json");
            var configModule = new ConfigurationModule(configBuilder.Build());

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(configModule);

            var container = containerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var view = scope.Resolve<MainView>();
                view.Show();
            }
        }
    }
}