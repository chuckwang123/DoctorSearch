using System;
using System.IO;
using System.Web.Hosting;
using DoctorSearch.Interfaces;

namespace DoctorSearch.utilities
{
    public class FileReader : IFileReader
    {
        private readonly IConfigurationManager _webconfig = new Webconfig();

        public string GetFile(string fileName)
        {
            var content = "";
            // ReSharper disable once AssignNullToNotNullAttribute
            using (var sr = new StreamReader(HostingEnvironment.MapPath(_webconfig.SqlQueryPath + fileName)))
            {
                var line = sr.ReadToEnd();
                content = line.Replace(Environment.NewLine, " ");
            }
            return content;
        }
    }
}