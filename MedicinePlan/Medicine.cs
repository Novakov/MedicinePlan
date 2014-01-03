using System;

namespace MedicinePlan
{
    public class Medicine : IEquatable<Medicine>
    {
        public string Name { get; private set; }

        public Medicine(string name)
        {
            this.Name = name;
        }

        public bool Equals(Medicine other)
        {
            return this.Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Medicine && this.Equals((Medicine) obj);
        }

        public override int GetHashCode()
        {
            return (this.Name != null ? this.Name.GetHashCode() : 0);
        }
    }
}