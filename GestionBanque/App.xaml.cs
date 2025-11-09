using Autofac;
using Autofac.Configuration;
using GestionBanque.Views; // Ajout du using pour MainView
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace GestionBanque
{
    public partial class App : Application
    {
        // On surcharge (override) la méthode OnStartup
        protected override void OnStartup(StartupEventArgs e)
        {
            // 1. Créer le ConfigurationBuilder pour lire le JSON
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("autofac.json");
            var configModule = new ConfigurationModule(configBuilder.Build());

            // 2. Créer le ContainerBuilder d'Autofac
            var containerBuilder = new ContainerBuilder();

            // 3. Enregistrer les composants définis dans le fichier JSON
            containerBuilder.RegisterModule(configModule);

            // 4. Construire le conteneur
            var container = containerBuilder.Build();

            // 5. Démarrer l'application en résolvant la vue principale
            // Autofac va s'occuper de créer MainView et de lui injecter MainViewModel
            using (var scope = container.BeginLifetimeScope())
            {
                var view = scope.Resolve<MainView>();
                view.Show();
            }
        }
    }
}