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

                remainingStock = new Stock(remainingStock.Count - used, end);
            }

            return remainingStock;
        }
    }
}
