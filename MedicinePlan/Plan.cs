using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinePlan
{
    public class Plan
    {
        private readonly Dictionary<DateTime, IDosage> dosages;

        public Plan()
        {
            this.dosages = new Dictionary<DateTime, IDosage>();
        }

        public void RegisterDosage(DateTime useSince, IDosage dosage)
        {
            this.dosages[useSince] = dosage;
        }

        public Stock CalculateRemaining(Stock initialStock, DateTime asof)
        {
            var applicableDosages = this.DosagesValidOn(initialStock.AsOfDate, asof);

            var remaining = initialStock;
            foreach (var dosage in applicableDosages)
            {
                var used = dosage.Dosage.CalculateUsed(remaining.AsOfDate, dosage.End);

                remaining = remaining.Reduce(used, dosage.End);
            }

            return remaining;
        }

        public DateTime CalculateExhaustionDate(Stock stock)
        {
            var applicableDosages = this.DosagesValidOn(stock.AsOfDate, DateTime.MaxValue);

            var remaining = stock;

            foreach (var dosage in applicableDosages)
            {
                var used = dosage.Dosage.CalculateUsed(remaining.AsOfDate, dosage.End);

                if (used > remaining.Count)
                {
                    return dosage.Dosage.CalculateExhaustionDate(remaining);
                }

                remaining = remaining.Reduce(used, dosage.End);
            }

            throw new InvalidOperationException("Dosages cannot exhaust stock");
        }

        private IEnumerable<DosageValidOver> DosagesValidOn(DateTime from, DateTime to)
        {
            var list = this.dosages.ToList();
            list.Add(new KeyValuePair<DateTime, IDosage>(to, null));

            for (int i = 0; i <= list.Count - 2; i++)
            {
                var start = list[i].Key.NotBefore(from);
                var end = list[i + 1].Key.NotAfter(to);

                if (end >= from && start <= to)
                {
                    yield return new DosageValidOver(list[i].Value, start, end);
                }
            }
        }

        public IDosage CurrentDosage(DateTime asof)
        {
            return this.DosagesValidOn(asof, asof).Select(x => x.Dosage).SingleOrDefault();
        }       
    }    
}
