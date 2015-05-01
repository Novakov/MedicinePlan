using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinePlan
{
    public class Supplies
    {
        private readonly Dictionary<Medicine, MedicineState> medicines;

        public Supplies()
        {
            this.medicines = new Dictionary<Medicine, MedicineState>();
        }

        private MedicineState this[Medicine medicine]
        {
            get
            {
                if (!this.medicines.ContainsKey(medicine))
                {
                    this.medicines[medicine] = new MedicineState();
                }

                return this.medicines[medicine];
            }
        }

        public void AddDosage(Medicine medicine, DateTime since, IDosage dosage)
        {
            this[medicine].Plan.RegisterDosage(since, dosage);
        }

        public DateTime ExhaustionOf(Medicine medicine)
        {
            return this[medicine].Plan.CalculateExhaustionDate(this[medicine].Stock);
        }

        public void Refill(Medicine medicine, Stock stockToAdd)
        {
            var remainingFromCurrentStock = this[medicine].Plan.CalculateRemaining(this[medicine].Stock, stockToAdd.AsOfDate);

            this[medicine].Stock = remainingFromCurrentStock.Add(stockToAdd);
        }

        public void Refill(IDictionary<Medicine, Stock> stocksToAdd)
        {
            foreach (var stock in stocksToAdd)
            {
                this.Refill(stock.Key, stock.Value);
            }
        }

        public Stock RemainingStock(Medicine medicine, DateTime asof)
        {
            return this[medicine].Plan.CalculateRemaining(this[medicine].Stock, asof);
        }

        public Stock CurrentStock(Medicine medicine)
        {
            return this[medicine].Stock;
        }

        public IEnumerable<Medicine> GetMedicines()
        {
            return this.medicines.Keys;
        }

        public IDosage CurrentDosage(Medicine medicine, DateTime asof)
        {
            return this[medicine].Plan.CurrentDosage(asof);
        }

        public void RemoveMedicine(Medicine medicine)
        {
            this.medicines.Remove(medicine);
        }

        private class MedicineState
        {
            public MedicineState()
            {
                this.Plan = new Plan();
                this.Stock = Stock.Empty;
            }

            public Stock Stock { get; set; }

            public Plan Plan { get; private set; }
        }
    }
}
