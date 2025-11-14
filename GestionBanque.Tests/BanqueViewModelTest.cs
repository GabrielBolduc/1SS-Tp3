using GestionBanque.Models;
using GestionBanque.Models.DataService;
using GestionBanque.ViewModels;
using GestionBanque.ViewModels.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GestionBanque.Tests
{
    public class BanqueViewModelTest
    {
        private readonly Mock<IInteractionUtilisateur> _interactionMock;
        private readonly Mock<IDataService<Client>> _dsClientsMock;
        private readonly Mock<IDataService<Compte>> _dsComptesMock;

        private readonly BanqueViewModel _viewModel;
        private readonly List<Client> _clientsFactices;

        public BanqueViewModelTest()
        {
            _interactionMock = new Mock<IInteractionUtilisateur>();
            _dsClientsMock = new Mock<IDataService<Client>>();
            _dsComptesMock = new Mock<IDataService<Compte>>();

            _clientsFactices = new List<Client>
            {
                new Client(1, "Gratton", "Bob", "bob@gratton.com"),
                new Client(2, "Bolduc", "Gabriel", "gabriel@bolduc.com")
            };
            _clientsFactices[0].Comptes.Add(new Compte(10, "C1", 100, 1));
            _dsClientsMock.Setup(ds => ds.GetAll()).Returns(_clientsFactices); 


            _viewModel = new BanqueViewModel(
                _interactionMock.Object,
                _dsClientsMock.Object,
                _dsComptesMock.Object
            );
        }

        [Fact]
        public void Constructeur_ShouldLoadClients()
        {
            Assert.Equal(2, _viewModel.Clients.Count);
            Assert.Equal(_clientsFactices, _viewModel.Clients);
        }

        [Fact]
        public void ClientSelectionne_Setter_ShouldUpdateProperties()
        {
            // Préparation (Arrange)
            Client clientTest = _clientsFactices[0]; // Bob Gratton

            // Exécution (Act)
            _viewModel.ClientSelectionne = clientTest; //

            Assert.Equal(clientTest.Nom, _viewModel.Nom);
            Assert.Equal(clientTest.Prenom, _viewModel.Prenom);
            // Bogue 7
            Assert.Equal(clientTest.Courriel, _viewModel.Courriel);
        }

        [Fact]
        public void Modifier_Exception_ShouldResetAllProperties()
        {

            Client clientTest = _clientsFactices[0]; // Bob Gratton
            _viewModel.ClientSelectionne = clientTest;

            string ancienNom = clientTest.Nom; // "Gratton"
            string ancienPrenom = clientTest.Prenom; // "Bob"
            string ancienCourriel = clientTest.Courriel; // "bob@gratton.com"

            _viewModel.Nom = "NouveauNom";
            _viewModel.Prenom = "NouveauPrenom";
            _viewModel.Courriel = "nouveau@courriel.com";

            _dsClientsMock.Setup(ds => ds.Update(It.IsAny<Client>())).Throws(new Exception("Erreur BD"));

            _viewModel.Modifier(null);

            Assert.Equal(ancienPrenom, _viewModel.ClientSelectionne.Prenom);
            Assert.Equal(ancienCourriel, _viewModel.ClientSelectionne.Courriel);
            //Bogue 8
            Assert.Equal(ancienNom, _viewModel.ClientSelectionne.Nom);
        }

        [Fact]
        public void Retirer_ShouldCallUpdateAndResetMontant()
        {
            // Préparation
            Compte compteTest = _clientsFactices[0].Comptes[0];
            _viewModel.CompteSelectionne = compteTest;
            _viewModel.MontantTransaction = 10.0;
            double balanceAttendue = 90.0; 

            _viewModel.Retirer(null); 

            Assert.Equal(balanceAttendue, compteTest.Balance); 
            Assert.Equal(0, _viewModel.MontantTransaction); 

            _dsComptesMock.Verify(ds => ds.Update(compteTest), Times.Once());
        }

        [Fact]
        public void Deposer_ShouldCallUpdateAndResetMontant()
        {
            Compte compteTest = _clientsFactices[0].Comptes[0]; 
            _viewModel.CompteSelectionne = compteTest;
            _viewModel.MontantTransaction = 10.0;
            double balanceAttendue = 110.0; 

            _viewModel.Deposer(null); 

            Assert.Equal(balanceAttendue, compteTest.Balance);
            Assert.Equal(0, _viewModel.MontantTransaction);
            _dsComptesMock.Verify(ds => ds.Update(compteTest), Times.Once());
        }
    }
}