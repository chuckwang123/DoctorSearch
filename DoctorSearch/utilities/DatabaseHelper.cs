using System;
using System.Collections.Generic;
using DoctorSearch.Interfaces;
using DoctorSearch.Models.Doctor;

namespace DoctorSearch.utilities
{
    public class DatabaseHelper
    {
        private readonly IDapper _dapper;
        private readonly IFileReader _fileReader;
        private readonly IConfigurationManager _configuration;

        public DatabaseHelper(IFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            _dapper = factory.Dapper;
            _fileReader = factory.Files;
            _configuration = factory.WebConfig;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            var response = _dapper.Query<Doctor>(_configuration.RdssqlServerConnection, _fileReader.GetFile(_configuration.GetDoctors));
            return response;
        }

        public IEnumerable<Specialties> GetSpecialtiesByDoctorId(int doctorId)
        {
            var response = _dapper.Query<Specialties>(_configuration.RdssqlServerConnection, _fileReader.GetFile(_configuration.GetSpecialtiesByDoctorId), new { doctorId = doctorId });
            return response;
        }
    }
}