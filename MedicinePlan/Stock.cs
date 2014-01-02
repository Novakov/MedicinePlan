using System;

namespace MedicinePlan
{
    public class Stock : IEquatable<Stock>
    {
        public static readonly Stock Empty = new Stock(0, DateTime.MinValue);

        public int Count { get; private set; }
        public DateTime AsOfDate { get; private set; }

        public Stock(int count, DateTime asOfDate)
        {
            Count = count;
            AsOfDate = asOfDate;
        }

        public bool Equals(Stock other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Count == other.Count && AsOfDate.Equals(other.AsOfDate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Stock) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Count * 397) ^ AsOfDate.GetHashCode();
            }
        }

        public override string ToString()
        {
            return this.AsOfDate.ToShortDateString() + "#" + this.Count;
        }

        public Stock Reduce(int used, DateTime end)
        {
            if (used > this.Count)
            {
                return new Stock(0, end);
            }

            return new Stock(this.Count - used, end);
        }

        public Stock Add(Stock stockToAdd)
        {            
            return new Stock(this.Count + stockToAdd.Count, DateTimeExtensions.Latest(this.AsOfDate, stockToAdd.AsOfDate));
        }
    }
}