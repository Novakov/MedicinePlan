using System;

namespace MedicinePlan
{
    public class CountPerDayDosage : IDosage
    {
        private readonly int countPerDay;

        public CountPerDayDosage(int countPerDay)
        {
            this.countPerDay = countPerDay;
        }

        public int CalculateUsed(DateTime fromDate, DateTime toDate)
        {
            return (int)Math.Floor((toDate - fromDate).TotalDays) * this.countPerDay;
        }

        public DateTime CalculateExhaustionDate(Stock stock)
        {
            return stock.AsOfDate.AddDays(Math.Floor(stock.Count / (double)countPerDay));
        }
    }
}