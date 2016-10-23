using System;

namespace DoctorSearch.Models.Doctor
{
    public class Specialties : IEquatable<Specialties>
    {
        public int SpecialtyId { get; set; }
        public string Name { get; set; }
        public bool Equals(Specialties other)
        {
            return Name == other.Name;
        }
    }
}
