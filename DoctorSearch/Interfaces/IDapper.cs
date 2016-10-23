using System.Collections.Generic;

namespace DoctorSearch.Interfaces
{
    public interface IDapper
    {
        IEnumerable<T> Query<T>(string connection, string sql, object parameter = null);
    }
}
