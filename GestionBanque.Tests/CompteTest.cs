// test 1
using GestionBanque.Models;
using System;
using Xunit; //

namespace GestionBanque.Tests
{
    public class CompteTest
    {
        //[Fact]
        public void Retirer_MontantValide_ShouldDecreaseBalance()
        {
            // Préparation (Arrange)
            Compte compte = new Compte(1, "12345", 100.0, 1);

            // Exécution (Act)
            compte.Retirer(20.0);

            // Affirmation (Assert)
            Assert.Equal(80.0, compte.Balance);
        }

        //[Fact]
        public void Deposer_MontantValide_ShouldIncreaseBalance()
        {
            // Préparation (Arrange)
            Compte compte = new Compte(1, "12345", 100.0, 1);

            // Exécution (Act)
            compte.Deposer(50.0); //

            // Affirmation (Assert)
            Assert.Equal(150.0, compte.Balance);
        }

        //[Fact]
        public void Retirer_MontantInvalide_ShouldThrowException()
        {
            Compte compte = new Compte(1, "12345", 100.0, 1);
            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Retirer(200.0));
        }

        //[Fact]
        public void Deposer_MontantInvalide_ShouldThrowException()
        {
            Compte compte = new Compte(1, "12345", 100.0, 1);
            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Deposer(-50.0));
        }
    }
}