using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MerchantCsNG;
using NUnit.Framework;

namespace NunitTetstsMerchant
{
    [TestFixture]
    public class ExchangeRateTests
    {
        public CurrencyContainer currencyContainer = new CurrencyContainer();

        [Test]
        void AmountCalculationPTGG()
        {
            List<string> IGUnits = new List<string>() { "pish", "tegj", "glob", "glob" };
            Double totalAmount = currencyContainer.GetDecimalValue(IGUnits);
            Assert.AreEqual(totalAmount, 42);
        }

        [Test]
        void CreditCalculationGPSilver()
        {
            List<string> IGUnits = new List<string>() { "glob", "prok" };
            Double totalAmount = currencyContainer.CalculateCredit(IGUnits, "Silver");
            Assert.AreEqual(totalAmount, 68);
        }

        [Test]
        void CreditCalculationGPGold()
        {
            List<string> IGUnits = new List<string>() { "glob", "prok" };
            Double totalAmount = currencyContainer.CalculateCredit(IGUnits, "Gold");
            Assert.AreEqual(totalAmount, 57800);
        }

        [Test]
        void CreditCalculationGPIron()
        {
            List<string> IGUnits = new List<string>() { "glob", "prok" };
            Double totalAmount = currencyContainer.CalculateCredit(IGUnits, "Iron");
            Assert.AreEqual(totalAmount, 782);
        }
    }
}
