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

        [Test]
        public void Bug_MixedValuesInJson()
        {
            // arrange
            var dx = new Medicine("DX");
            var p = new Medicine("P");
            var d5 = new Medicine("D5");
            var d3 = new Medicine("D3");

            var original = new Supplies();

            original.AddDosage(dx, new DateTime(2013, 12, 21), new CountPerDayDosage(3));
            original.AddDosage(p, new DateTime(2013, 12, 21), new CountPerDayDosage(1));
            original.AddDosage(d5, new DateTime(2013, 12, 21), new CountPerDayDosage(2));
            original.AddDosage(d3, new DateTime(2013, 11, 27), new CountPerDayDosage(1));

            original.Refill(new Dictionary<Medicine, Stock>
            {
                {dx, new Stock(205, new DateTime(2013, 12, 21))},
                {p, new Stock(60, new DateTime(2013, 12, 21))},
                {d5, new Stock(181, new DateTime(2013, 12, 21))},
                {d3, new Stock(100, new DateTime(2013, 11, 27))},
            });

            var repo = new Repository();

            // act
            var json = repo.DumpJson(original);
            var restored = repo.ReadJson(json);

            // assert
            Console.WriteLine(json);
            foreach (var medicine in original.GetMedicines())
            {
                Assert.That(restored.CurrentStock(medicine), Is.EqualTo(original.CurrentStock(medicine)), "CurrentStock: " + medicine.Name);
                Assert.That(restored.ExhaustionOf(medicine), Is.EqualTo(original.ExhaustionOf(medicine)), "ExhaustionDate: " + medicine.Name);
                Assert.That(restored.RemainingStock(medicine, DateTime.Today), Is.EqualTo(original.RemainingStock(medicine, DateTime.Today)), "RemainingStock: " + medicine.Name);
            }
        }
    }
}
