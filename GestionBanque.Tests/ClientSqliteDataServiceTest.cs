
using GestionBanque.Models.DataService;
using GestionBanque.Models;

namespace GestionBanque.Tests
{
    // Ce décorateur s'assure que toutes les classes de tests ayant le tag "Dataservice" soit
    // exécutées séquentiellement. Par défaut, xUnit exécute les différentes suites de tests
    // en parallèle. Toutefois, si nous voulons forcer l'exécution séquentielle entre certaines
    // suites, nous pouvons utiliser un décorateur avec un nom unique. Pour les tests sur les DataService,
    // il est important que cela soit séquentiel afin d'éviter qu'un test d'une classe supprime la 
    // bd de tests pendant qu'un test d'une autre classe utilise la bd. Bref, c'est pour éviter un
    // accès concurrent à la BD de tests!
    [Collection("Dataservice")]
    public class ClientSqliteDataServiceTest
    {
        private const string CheminBd = "..\\test.bd";

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Get_ShouldBeValid()
        {
            // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client clientAttendu = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            clientAttendu.Comptes.Add(new Compte(1, "9864", 831.76, 1));
            clientAttendu.Comptes.Add(new Compte(2, "2370", 493.04, 1));

            // Exécution
            Client? clientActuel = ds.Get(1);

            // Affirmation
            Assert.Equal(clientAttendu, clientActuel);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void GetAll_ShouldBeValid()
        {
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            List<Client> clientsAttendus = new List<Client>
            {
                new Client(1, "Amar", "Quentin", "quentin@gmail.com"),
                new Client(2, "Agère", "Tex", "tex@gmail.com"),
                new Client(3, "Vigote", "Sarah", "sarah@gmail.com")
            };
            clientsAttendus[0].Comptes.Add(new Compte(1, "9864", 831.76, 1));
            clientsAttendus[0].Comptes.Add(new Compte(2, "2370", 493.04, 1));
            clientsAttendus[1].Comptes.Add(new Compte(3, "7640", 634.73, 2));
            clientsAttendus[2].Comptes.Add(new Compte(4, "7698", 906.72, 3));

            List<Client> clientsActuels = ds.GetAll().ToList(); //

            Assert.Equal(clientsAttendus.Count, clientsActuels.Count);
            Assert.Equal(clientsAttendus, clientsActuels);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void RecupererComptes_ShouldNotDuplicateOnSecondCall()
        {
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client? client = ds.Get(1);
            int nbComptesAttendu = 2;
            Assert.Equal(nbComptesAttendu, client.Comptes.Count); 

            ds.RecupererComptes(client); //

            Assert.Equal(nbComptesAttendu, client.Comptes.Count);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Update_ShouldBeValid()
        {
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client? clientAModifier = ds.Get(1);
            string nouveauNom = "NouveauNom";
            clientAModifier.Nom = nouveauNom;

            bool updateReussi = ds.Update(clientAModifier); //
            Assert.True(updateReussi);
            Client? clientModifie = ds.Get(1);
            Assert.Equal(nouveauNom, clientModifie.Nom);
        }
    }
}
