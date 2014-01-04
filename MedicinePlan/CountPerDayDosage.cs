using System;

namespace MedicinePlan
{
    public class CountPerDayDosage : IDosage
    {
        public int CountPerDay { get; private set; }

        public CountPerDayDosage(int countPerDay)
        {
            this.CountPerDay = countPerDay;
        }

        public int CalculateUsed(DateTime fromDate, DateTime toDate)
        {
            return (int)Math.Floor((toDate - fromDate).TotalDays) * this.CountPerDay;
        }

        public DateTime CalculateExhaustionDate(Stock stock)
        {
            return stock.AsOfDate.AddDays(Math.Max(Math.Floor(stock.Count/(double) this.CountPerDay) - 1, 0));
        }
    }
}