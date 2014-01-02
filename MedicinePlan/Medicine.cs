using System;

namespace MedicinePlan
{
    public class Medicine : IEquatable<Medicine>
    {
        private readonly string name;

        public Medicine(string name)
        {
            this.name = name;
        }

        public bool Equals(Medicine other)
        {
            return this.name == other.name;
        }

        public override bool Equals(object obj)
        {
            return obj is Medicine && this.Equals((Medicine) obj);
        }

        public override int GetHashCode()
        {
            return (name != null ? name.GetHashCode() : 0);
        }
    }
}