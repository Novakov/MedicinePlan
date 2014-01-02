using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinePlan
{
    internal static class DateTimeExtensions
    {
        public static DateTime NotBefore(this DateTime @this, DateTime dateTime)
        {
            if (@this < dateTime)
            {
                return dateTime;
            }

            return @this;
        }

        public static DateTime NotAfter(this DateTime @this, DateTime dateTime)
        {
            if (@this > dateTime)
            {
                return dateTime;
            }

            return @this;
        }
    }
}
