// test 2
using GestionBanque.Models;
using System;
using Xunit;

namespace GestionBanque.Tests
{
    public class ClientTest
    {
        private readonly Client _client = new Client(1, "Valide", "Valide", "valide@courriel.com");

        [Fact]
        public void Prenom_Setter_Valide_ShouldTrim()
        {
            _client.Prenom = "   Bob   "; 
            Assert.Equal("Bob", _client.Prenom);
        }

        [Fact]
        public void Nom_Setter_Valide_ShouldTrim()
        {
            _client.Nom = "   Gratton   "; 
            Assert.Equal("Gratton", _client.Nom);
        }

        [Theory] //
        [InlineData("test@test.com")]
        [InlineData("   test@test.com   ")]
        public void Courriel_Setter_Valide_ShouldTrim(string courriel)
        {
            _client.Courriel = courriel; //
            Assert.Equal("test@test.com", _client.Courriel);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test.test.com")] // Invalide, mais la regex l'accepte (Bogue 3)
        [InlineData("pasunboncourriel")]
        public void Courriel_Setter_Invalide_ShouldThrowException(string courrielInvalide)
        {
            Assert.Throws<ArgumentException>(() => _client.Courriel = courrielInvalide);
        }
    }
}