using System;
using System.Collections.Generic;
using System.Linq;

namespace DoctorSearch.Models.Doctor
{
    public class Doctor : IEquatable<Doctor>
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Sex { get; set; }
        public string TEL { get; set; }
        public string AreaCode { get; set; }
        public string Score { get; set; }
        public List<Specialties> Specialties { get; set; }
        public bool Equals(Doctor other)
        {
            var result = false;
            result = Name == other.Name && Birth == other.Birth && Sex == other.Sex && TEL == other.TEL &&
                     AreaCode == other.AreaCode && Score == other.Score;
            if (Specialties == null || other.Specialties == null)
            {
                return result;
            }
            else
            {
                return result && (!Specialties.Except(other.Specialties, new SpecialtiesComparer()).Any());
            }
        }
    }
}
