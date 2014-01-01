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
    public class PlanTest
    {
        [Test]
        public void ShouldReturnProperStockWithOnePlan()
        {
            // arrange
            var plan = new Plan();
            plan.RegisterDosage(new CountPerDayDosage(5));

            var startDate = new DateTime(2014, 1, 1);
            var calculateFor = new DateTime(2014, 1, 5);
            var initialStock = new Stock(100, startDate);

            // act
            var remaining = plan.CalculateRemaining(initialStock, calculateFor);

            // assert
            Assert.That(remaining, Is.EqualTo(new Stock(80, calculateFor)));
        }
    }
}
