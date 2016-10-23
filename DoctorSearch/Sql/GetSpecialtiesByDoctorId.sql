select dbo.Specialties.*
from dbo.Doctor_details
inner join dbo.DoctorSpecialtyRelation
on Doctor_details.DoctorId = DoctorSpecialtyRelation.Doctorid and Doctor_details.DoctorId = @doctorId
inner join dbo.Specialties
On Specialties.SpecialtyId = DoctorSpecialtyRelation.SpecialtyId