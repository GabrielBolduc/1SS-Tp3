using GestionBanque.Models;
using System;
using Xunit; //

namespace GestionBanque.Tests
{
    public class CompteTest
    {
        [Fact]
        public void Retirer_MontantValide_ShouldDecreaseBalance()
        {
            double balanceInitiale = 100.0;
            double montantRetrait = 20.0;
            double balanceAttendue = 80.0;
            Compte compte = new Compte(1, "12345", balanceInitiale, 1);

            compte.Retirer(montantRetrait);

            Assert.Equal(balanceAttendue, compte.Balance);
        }

        [Fact]
        public void Deposer_MontantValide_ShouldIncreaseBalance()
        {
            double balanceInitiale = 100.0;
            double montantDepot = 50.0;
            double balanceAttendue = 150.0;
            Compte compte = new Compte(1, "12345", balanceInitiale, 1);

            compte.Deposer(montantDepot); 

            Assert.Equal(balanceAttendue, compte.Balance);
        }

        [Fact]
        public void Retirer_MontantInvalide_ShouldThrowException()
        {
            double balanceInitiale = 100.0;
            double montantRetrait = 200.0;
            Compte compte = new Compte(1, "12345", balanceInitiale, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Retirer(montantRetrait));
        }

        [Fact]
        public void Deposer_MontantInvalide_ShouldThrowException()
        {
            double balanceInitiale = 100.0;
            double montantDepot = -50.0;
            Compte compte = new Compte(1, "12345", balanceInitiale, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => compte.Deposer(montantDepot));
        }
    }
}