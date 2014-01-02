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
            var applicableDosages = this.dosages.Where(x => x.Key >= initialStock.AsOfDate && x.Key <= asof).OrderBy(x => x.Key).ToList();

            applicableDosages.Add(new KeyValuePair<DateTime, IDosage>(asof, null));

            var remainingStock = initialStock;

            for (int i = 0; i <= applicableDosages.Count - 2; i++)
            {
                var start = applicableDosages[i].Key;
                var end = applicableDosages[i + 1].Key;

                var used = applicableDosages[i].Value.CalculateUsed(start, end);

                remainingStock = remainingStock.Reduce(used, end);
            }

            return remainingStock;
        }

        public DateTime CalculateExhaustionDate(Stock stock)
        {
            var applicableDosages = this.dosages.Where(x => x.Key >= stock.AsOfDate).OrderBy(x => x.Key).ToList();
            applicableDosages.Add(new KeyValuePair<DateTime, IDosage>(DateTime.MaxValue, null));

            var remaining = stock;
            int i = 0;

            while (remaining.Count > 0)
            {
                var start = applicableDosages[i].Key;
                var end = applicableDosages[i + 1].Key;

                var used = applicableDosages[i].Value.CalculateUsed(start, end);

                if (used > remaining.Count)
                {
                    return applicableDosages[i].Value.CalculateExhaustionDate(remaining);
                }
                else
                {
                    remaining = remaining.Reduce(used, end);
                }

                i++;
            }

            return remaining.AsOfDate;
        }
    }
}
