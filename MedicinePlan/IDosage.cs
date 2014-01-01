using System;

namespace MedicinePlan
{
    public interface IDosage
    {
        int CalculateUsed(DateTime fromDate, DateTime toDate);
    }
}