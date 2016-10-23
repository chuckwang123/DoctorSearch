
using System.Configuration;
using DoctorSearch.Interfaces;

namespace DoctorSearch.utilities
{
    public class Webconfig : IConfigurationManager
    {
        public string RdssqlServerConnection => ConfigurationManager.ConnectionStrings["RDSSQLServerConnection"].ConnectionString;
        public string GetSpecialtiesByDoctorId => ConfigurationManager.AppSettings["GetSpecialtiesByDoctorId"];
        public string GetDoctors => ConfigurationManager.AppSettings["GetDoctors"];
        public string SqlQueryPath => ConfigurationManager.AppSettings["SqlQueryPath"];
    }
}