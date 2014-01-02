using System;
using MedicinePlan;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class StockTest
    {
        [Test]
        public void CannotBeReducedBelowZero()
        {
            // arrange
            var initialStock = new Stock(100, new DateTime(2014, 1, 1));

            // act
            var reduced = initialStock.Reduce(120, new DateTime(2014, 2, 2));

            // assert
            Assert.That(reduced, Is.EqualTo(new Stock(0, new DateTime(2014, 2, 2))));
        }
    }
}