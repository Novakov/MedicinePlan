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
        private static readonly CountPerDayDosage FivePerDay = new CountPerDayDosage(5);
        private static readonly CountPerDayDosage TenPerDay = new CountPerDayDosage(10);

        [Test]
        public void ShouldReturnProperStockWithOneDosage()
        {
            // arrange
            var plan = new Plan();
            var startDate = new DateTime(2014, 1, 1);
            plan.RegisterDosage(startDate, FivePerDay);

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

            plan.RegisterDosage(startDate, FivePerDay);
            plan.RegisterDosage(secondDosageStartDate, TenPerDay);

            var calculateFor = new DateTime(2014, 1, 10);
            var initialStock = new Stock(100, startDate);

            // act
            var remaining = plan.CalculateRemaining(initialStock, calculateFor);

            // assert
            Assert.That(remaining, Is.EqualTo(new Stock(30, calculateFor)));
        }

        [Test]
        public void ShouldReturnProperExhaustionDateWithOneDosage()
        {
            // arrange
            var plan = new Plan();
            var startDate = new DateTime(2014, 1, 1);
            plan.RegisterDosage(startDate, FivePerDay);
            var stock = new Stock(100, startDate);

            // act
            var exhaustionDate = plan.CalculateExhaustionDate(stock);

            // assert
            Assert.That(exhaustionDate, Is.EqualTo(new DateTime(2014, 1, 21)));
        }

        [Test]
        public void ShouldReturnProperExhaustionDateWithTwoDosages()
        {
            // arrange
            var plan = new Plan();
            var startDate = new DateTime(2014, 1, 1);
            plan.RegisterDosage(startDate, FivePerDay);
            plan.RegisterDosage(new DateTime(2014, 1, 5), TenPerDay);
            var stock = new Stock(100, startDate);

            // act
            var exhaustionDate = plan.CalculateExhaustionDate(stock);

            // assert
            Assert.That(exhaustionDate, Is.EqualTo(new DateTime(2014, 1, 13)));
        }

        [Test]
        public void ShouldIgnoreDosagesPreviousToStartDate()
        {
            // arrange
            var plan = new Plan();
            var startDate = new DateTime(2014, 1, 1);
            plan.RegisterDosage(startDate.AddDays(-7), TenPerDay);
            plan.RegisterDosage(startDate, FivePerDay);

            var initialStock = new Stock(100, startDate);

            // act
            var remaining = plan.CalculateRemaining(initialStock, new DateTime(2014, 1, 5));
            var exhaustionDate = plan.CalculateExhaustionDate(initialStock);

            // assert
            Assert.That(remaining, Is.EqualTo(new Stock(80, new DateTime(2014, 1, 5))));
            Assert.That(exhaustionDate, Is.EqualTo(new DateTime(2014, 1, 21)));
        }

        [Test]
        public void ShouldTakeIntoAccountDosagesOverlapingWithStartDate()
        {
            // arrange
            var plan = new Plan();
            var startDate = new DateTime(2014, 1, 1);
            plan.RegisterDosage(startDate.AddDays(-7), TenPerDay);
            plan.RegisterDosage(startDate.AddDays(1), FivePerDay);

            var initialStock = new Stock(100, startDate);

            // act
            var remaining = plan.CalculateRemaining(initialStock, new DateTime(2014, 1, 5));
            var exhaustionDate = plan.CalculateExhaustionDate(initialStock);

            // assert
            Assert.That(remaining, Is.EqualTo(new Stock(75, new DateTime(2014, 1, 5))));
            Assert.That(exhaustionDate, Is.EqualTo(new DateTime(2014, 1, 20)));
        }

        [Test]
        public void ShouldIgnoreDosagesAfterEndDateWhenCalculatingRemaining()
        {
            // arrange
            var plan = new Plan();
            var startDate = new DateTime(2014, 1, 1);
            plan.RegisterDosage(startDate, TenPerDay);
            plan.RegisterDosage(new DateTime(2014, 1, 10), FivePerDay);

            var initialStock = new Stock(100, startDate);

            // act
            var remaining = plan.CalculateRemaining(initialStock, new DateTime(2014, 1, 5));            

            // assert
            Assert.That(remaining, Is.EqualTo(new Stock(60, new DateTime(2014, 1, 5))));           
        }
    }
}
