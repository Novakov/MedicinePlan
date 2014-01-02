using System;

namespace MedicinePlan
{
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