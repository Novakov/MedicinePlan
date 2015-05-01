using System;

namespace MedicinePlan
{
    public class NoDosage : IDosage
    {
        public int CalculateUsed(DateTime fromDate, DateTime toDate)
        {
            return 0;
        }

        public DateTime CalculateExhaustionDate(Stock stock)
        {
            return DateTime.MaxValue;            
        }
    }
}