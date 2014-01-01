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
        public void ShouldReturnProperStockWithOneDosage()
        {
            // arrange
            var plan = new Plan();
            var startDate = new DateTime(2014, 1, 1);
            plan.RegisterDosage(startDate, new CountPerDayDosage(5))
            ;

            var calculateFor = new DateTime(2014, 1, 5);
            var initialStock = new Stock(100, startDate);

            // act
            var remaining = plan.CalculateRemaining(initialStock, calculateFor);

            // assert
            Assert.That(remaining, Is.EqualTo(new Stock(80, calculateFor)));
        }

        [Test]
        public void ShouldReturnProperStockWithTwoDosages()
        {
            // arrange
            var plan = new Plan();
            var startDate = new DateTime(2014, 1, 1);
            var secondDosageStartDate = new DateTime(2014, 1, 5);

            plan.RegisterDosage(startDate, new CountPerDayDosage(5));
            plan.RegisterDosage(secondDosageStartDate, new CountPerDayDosage(10));

            var calculateFor = new DateTime(2014, 1, 10);
            var initialStock = new Stock(100, startDate);

            // act
            var remaining = plan.CalculateRemaining(initialStock, calculateFor);

            // assert
            Assert.That(remaining, Is.EqualTo(new Stock(30, calculateFor)));
        }
    }
}
