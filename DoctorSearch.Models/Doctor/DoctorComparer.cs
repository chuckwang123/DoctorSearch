using System;
using System.Collections.Generic;
using System.Linq;

namespace DoctorSearch.Models.Doctor
{
    public class DoctorComparer : IEqualityComparer<Doctor>
    {
        public bool Equals(Doctor x, Doctor y)
        {
            // Check whether the compared objects reference the same data. 
            if (object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null. 
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
                return false;

            // Check whether the properties are equal. 
            var result = x.Name == y.Name && x.Birth == y.Birth && x.Sex == y.Sex && x.TEL == y.TEL &&
                          x.AreaCode == y.AreaCode && x.Score == y.Score;
            return result && (!x.Specialties.Except(y.Specialties, new SpecialtiesComparer()).Any());
        }

        public int GetHashCode(Doctor record)
        {
            if (object.ReferenceEquals(record, null)) return 0;

            // Get the hash code for the tag field if it is not null. 
            var hashTag = record.Name?.GetHashCode() ?? 0;

            var hashTagid = 0;
            // Get the hash code for the tagid field. 
            if (record.Name != null)
            {
                hashTagid += record.Name.GetHashCode();
            }
            hashTagid += record.Birth.GetHashCode();
            if (record.AreaCode != null)
            {
                hashTagid += record.AreaCode.GetHashCode();
            }
            if (record.Score != null)
            {
                hashTagid += record.Score.GetHashCode();
            }
            hashTagid += record.Sex.GetHashCode();
            if (record.TEL != null)
            {
                hashTagid += record.TEL.GetHashCode();
            }
            if (record.Specialties != null)
            {
                hashTagid += record.Specialties.GetHashCode();
            }

            // Calculate the hash code for ARecord. 
            return hashTag ^ hashTagid;
        }
    }
}