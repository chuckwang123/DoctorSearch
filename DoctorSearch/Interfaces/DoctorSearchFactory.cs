using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorSearch.utilities;

namespace DoctorSearch.Interfaces
{
    public class DoctorSearchFactory : IFactory
    {
        public IConfigurationManager WebConfig => new Webconfig();
        public IFileReader Files => new FileReader();
        public IDapper Dapper => new DapperHelper();
    }
}