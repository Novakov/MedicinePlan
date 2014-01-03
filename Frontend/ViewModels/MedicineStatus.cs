using System;
using MedicinePlan;

namespace Frontend.ViewModels
{
    public class MedicineStatus
    {
        public string Name { get; set; }

        public IDosage Dosage { get; set; }

        public int Remaining { get; set; }

        public DateTime ExhaustionDate { get; set; }
    }
}