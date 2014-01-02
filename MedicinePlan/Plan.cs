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
            var applicableDosages = this.Dosages(asof).Where(x => x.Start >= initialStock.AsOfDate && x.End <= asof);

            return applicableDosages.Aggregate(initialStock, (stock, dosage) => stock.Reduce(dosage.Dosage.CalculateUsed(dosage.Start, dosage.End), dosage.End));
        }

        public DateTime CalculateExhaustionDate(Stock stock)
        {
            var applicableDosages = this.Dosages(DateTime.MaxValue).Where(x => x.Start >= stock.AsOfDate);

            var remaining = stock;

            foreach (var dosage in applicableDosages)
            {
                var used = dosage.Dosage.CalculateUsed(dosage.Start, dosage.End);

                if (used > remaining.Count)
                {
                    return dosage.Dosage.CalculateExhaustionDate(remaining);
                }

                remaining = remaining.Reduce(used, dosage.End);
            }

            throw new InvalidOperationException("Dosages cannot exhaust stock");
        }

        private IEnumerable<DosageValidOver> Dosages(DateTime lastDosageValidTo)
        {
            var list = this.dosages.ToList();
            list.Add(new KeyValuePair<DateTime, IDosage>(lastDosageValidTo, null));

            for (int i = 0; i <= list.Count - 2; i++)
            {
                yield return new DosageValidOver(list[i].Value, list[i].Key, list[i + 1].Key);
            }
        }
    }

    internal class DosageValidOver
    {
        public IDosage Dosage { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public DosageValidOver(IDosage dosage, DateTime start, DateTime end)
        {
            Dosage = dosage;
            Start = start;
            End = end;
        }
    }
}
