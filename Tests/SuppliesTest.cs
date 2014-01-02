using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicinePlan;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SuppliesTest
    {
        private static readonly Medicine VitaminumC = new Medicine("VitC");
        private static readonly CountPerDayDosage FivePerDay = new CountPerDayDosage(5);

        [Test]
        public void ExhaustionDateShouldBeBasedOnDosageAndRefill()
        {
            // arrange
            var supplies = new Supplies();
            supplies.AddDosage(VitaminumC,  new DateTime(2014, 1, 1), FivePerDay);

            supplies.Refill(VitaminumC, new Stock(100, new DateTime(2014, 1, 1)));           

            // act
            var exhaustionDate = supplies.ExhaustionOf(VitaminumC);

            // assert
            Assert.That(exhaustionDate, Is.EqualTo(new DateTime(2014, 1, 21)));
        }

        [Test]
        public void RemainingStockShouldBeBasedOnDosageAndRefill()
        {
            // arrange
            var supplies = new Supplies();
            supplies.AddDosage(VitaminumC, new DateTime(2014, 1, 1), FivePerDay);

            supplies.Refill(VitaminumC, new Stock(100, new DateTime(2014, 1, 1)));

            // act
            var remaining = supplies.RemainingStock(VitaminumC, new DateTime(2014, 1, 5));

            // assert
            Assert.That(remaining, Is.EqualTo(new Stock(80, new DateTime(2014, 1, 5))));
        }

        [Test]
        public void MultipleRefillShouldUpdateStockInProperWay()
        {
            // arrange
            var supplies = new Supplies();
            supplies.AddDosage(VitaminumC, new DateTime(2014, 1, 1), FivePerDay);

            supplies.Refill(VitaminumC, new Stock(100, new DateTime(2014, 1, 1)));
            supplies.Refill(VitaminumC, new Stock(50, new DateTime(2014, 1, 5)));

            // act
            var stock = supplies.CurrentStock(VitaminumC);

            // assert
            Assert.That(stock, Is.EqualTo(new Stock(130, new DateTime(2014, 1, 5))));
        }
    }
}
