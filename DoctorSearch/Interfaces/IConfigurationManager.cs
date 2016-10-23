namespace DoctorSearch.Interfaces
{
    public interface IConfigurationManager
    {
        string SqlQueryPath { get; }
        string RdssqlServerConnection { get; }

        string GetSpecialtiesByDoctorId { get; }
        string GetDoctors { get; }
    }
}
