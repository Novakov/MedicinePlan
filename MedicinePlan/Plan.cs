using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinePlan
{
    public class Plan
    {
        private IDosage dosage;

        public void RegisterDosage(IDosage dosage)
        {
            this.dosage = dosage;
        }

        public Stock CalculateRemaining(Stock initialState, DateTime asof)
        {
            var used = dosage.CalculateUsed(initialState.AsOfDate, asof);

            return new Stock(initialState.Count - used, asof);
        }
    }
}
