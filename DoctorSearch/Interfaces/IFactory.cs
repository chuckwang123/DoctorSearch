namespace DoctorSearch.Interfaces
{
    public interface IFactory
    {
        IConfigurationManager WebConfig { get; }
        IFileReader Files { get; }
        IDapper Dapper { get; }
    }
}
