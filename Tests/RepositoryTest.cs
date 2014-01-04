using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frontend;
using MedicinePlan;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RepositoryTest
    {
        [Test]
        public void SaveTest()
        {
            // arrange
            var supplies = new Supplies();
            supplies.AddDosage(new Medicine("VitaminumC"), DateTime.Today.AddDays(-7), new CountPerDayDosage(5));
            supplies.Refill(new Medicine("VitaminumC"), new Stock(100, DateTime.Today.AddDays(-7)));
            supplies.AddDosage(new Medicine("VitaminumD"), DateTime.Today.AddDays(-7), new CountPerDayDosage(5));
            supplies.Refill(new Medicine("VitaminumD"), new Stock(100, DateTime.Today.AddDays(-7)));
            supplies.AddDosage(new Medicine("VitaminumE"), DateTime.Today.AddDays(-7), new CountPerDayDosage(5));
            supplies.Refill(new Medicine("VitaminumE"), new Stock(100, DateTime.Today.AddDays(-7)));

            var repo = new Repository();

            // act
            var json = repo.DumpJson(supplies);
            var readed = repo.ReadJson(json);
            var jsonForRestored = repo.DumpJson(readed);            

            // assert
            Console.WriteLine(jsonForRestored);
            Assert.That(json, Is.EqualTo(jsonForRestored));
        }
    }
}
