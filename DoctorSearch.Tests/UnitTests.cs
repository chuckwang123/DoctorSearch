using System;
using System.Collections.Generic;
using System.Linq;
using DoctorSearch.Controllers;
using DoctorSearch.Interfaces;
using DoctorSearch.Models.Doctor;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DoctorSearch.Tests
{
    [TestClass]
    public class UnitTests
    {
        private DoctorsController _controller;

        private Mock<IFactory> _factoryMock;
        private Mock<IConfigurationManager> _configMock;
        private Mock<IFileReader> _fileMock;
        private Mock<IDapper> _dapperMock;


        [TestInitialize]
        public void TestInitialize()
        {
            _configMock = new Mock<IConfigurationManager>();
            _configMock.Setup(c => c.GetDoctors).Returns("GetDoctors");
            _configMock.Setup(c => c.GetSpecialtiesByDoctorId).Returns("GetSpecialtiesByDoctorId");
            _configMock.Setup(c => c.RdssqlServerConnection).Returns("RdssqlServerConnection");
            _configMock.Setup(c => c.SqlQueryPath).Returns("path");

            _fileMock = new Mock<IFileReader>();
            _fileMock.Setup(f => f.GetFile("GetDoctors")).Returns("GetDoctors");
            _fileMock.Setup(f => f.GetFile("GetSpecialtiesByDoctorId")).Returns("GetSpecialtiesByDoctorId");

            _dapperMock = new Mock<IDapper>();
            
            _factoryMock = new Mock<IFactory>();
            _factoryMock.Setup(f => f.Files).Returns(_fileMock.Object);
            _factoryMock.Setup(f => f.WebConfig).Returns(_configMock.Object);
            _factoryMock.Setup(f => f.Dapper).Returns(_dapperMock.Object);

            _controller = new DoctorsController(_factoryMock.Object);
        }

        #region setup
        private static IEnumerable<Doctor> DoctorsList()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    Name = "Name Test",
                    AreaCode = "12345",
                    Birth = DateTime.Today,
                    Score = "90",
                    Sex = "Male",
                    TEL = "1231456789"
                },
                new Doctor
                {
                    DoctorId = 2,
                    Name = "Name Tes1t1",
                    AreaCode = "45678",
                    Birth = DateTime.Today,
                    Score = "42",
                    Sex = "Female",
                }
            };
        }

        private static IEnumerable<Specialties> SpecialtiesList()
        {
            return  new List<Specialties>
            {
                new Specialties
                {
                    Name = "test",
                    SpecialtyId = 1
                },
                new Specialties
                {
                    Name = "tes1",
                    SpecialtyId = 2
                },
            };
        }

        private static IEnumerable<Specialties> ExceptSpecialtiesList()
        {
            return new List<Specialties>
            {
                new Specialties
                {
                    Name = "Nothing",
                    SpecialtyId = 1
                }
            };
        }

        private static List<Doctor> ExpectedDoctorList()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    Name = "Name Test",
                    AreaCode = "12345",
                    Birth = DateTime.Today,
                    Score = "90",
                    Sex = "Male",
                    TEL = "1231456789",
                    Specialties = new List<Specialties>
                    {
                        new Specialties
                        {
                            Name = "test",
                            SpecialtyId = 1
                        },
                        new Specialties
                        {
                            Name = "tes1",
                            SpecialtyId = 2
                        },
                    }
                },
                new Doctor
                {
                    DoctorId = 2,
                    Name = "Name Tes1t1",
                    AreaCode = "45678",
                    Birth = DateTime.Today,
                    Score = "42",
                    Sex = "Female",
                    Specialties = new List<Specialties>
                    {
                        new Specialties
                        {
                            Name = "test",
                            SpecialtyId = 1
                        },
                        new Specialties
                        {
                            Name = "tes1",
                            SpecialtyId = 2
                        },
                    }
                }
            };
        }

        private void GetDoctorsSetup()
        {
            var doctorList = DoctorsList();
            _dapperMock.Setup(f => f.Query<Doctor>("RdssqlServerConnection", "GetDoctors", null)).Returns(doctorList);
            _dapperMock.Setup(f => f.Query<Specialties>("RdssqlServerConnection", "GetSpecialtiesByDoctorId", It.IsAny<object>())).Returns(SpecialtiesList());
            _factoryMock.Setup(f => f.Dapper).Returns(_dapperMock.Object);
        }

        private void SearchSimilarDoctors_exactMatchSetup()
        {
            var doctorList = new List<Doctor> {PostBody()};
            _dapperMock.Setup(f => f.Query<Doctor>("RdssqlServerConnection", "GetDoctors", null)).Returns(doctorList);
            _dapperMock.Setup(f => f.Query<Specialties>("RdssqlServerConnection", "GetSpecialtiesByDoctorId", It.IsAny<object>())).Returns(SpecialtiesList());
            _factoryMock.Setup(f => f.Dapper).Returns(_dapperMock.Object);
        }

        private void SearchSimilarDoctors_NameSimilarSetup()
        {
            _dapperMock.Setup(f => f.Query<Doctor>("RdssqlServerConnection", "GetDoctors", null)).Returns(NameMatchedDoctorsList());
            _dapperMock.Setup(f => f.Query<Specialties>("RdssqlServerConnection", "GetSpecialtiesByDoctorId", It.IsAny<object>())).Returns(SpecialtiesList());
            _factoryMock.Setup(f => f.Dapper).Returns(_dapperMock.Object);
        }

        private void SearchSimilarDoctors_SpecialtiesSimilarSetup()
        {
            _dapperMock.Setup(f => f.Query<Doctor>("RdssqlServerConnection", "GetDoctors", null)).Returns(SpecialtiesMatchedDoctorsList());
            _dapperMock.Setup(f => f.Query<Specialties>("RdssqlServerConnection", "GetSpecialtiesByDoctorId", It.IsAny<object>())).Returns(ExceptSpecialtiesList());
            _factoryMock.Setup(f => f.Dapper).Returns(_dapperMock.Object);
        }

        private static Doctor PostBody()
        {
            return new Doctor
            {
                DoctorId = 1,
                Name = "Name Test",
                AreaCode = "12345",
                Birth = DateTime.Today,
                Score = "90",
                Sex = "Male",
                TEL = "1231456789",
                Specialties = new List<Specialties>
                {
                    new Specialties
                    {
                        Name = "test",
                        SpecialtyId = 1
                    },
                    new Specialties
                    {
                        Name = "tes1",
                        SpecialtyId = 2
                    },
                }
            };
        }

        private static IEnumerable<Doctor> NameMatchedDoctorsList()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    Name = "Name Test2",
                    AreaCode = "12345",
                    Birth = DateTime.Today,
                    Score = "90",
                    Sex = "Male",
                    TEL = "1231456789"
                },
                new Doctor
                {
                    DoctorId = 2,
                    Name = "Name Tes1t1",
                    AreaCode = "45678",
                    Birth = DateTime.Today,
                    Score = "42",
                    Sex = "Female",
                },
                new Doctor
                {
                    DoctorId = 1,
                    Name = "John Doe",
                    AreaCode = "12345",
                    Birth = DateTime.Today,
                    Score = "90",
                    Sex = "Male",
                    TEL = "1231456789"
                }
            };
        }

        private static IEnumerable<Doctor> SpecialtiesMatchedDoctorsList()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    Name = "Name Test2",
                    AreaCode = "12345",
                    Birth = DateTime.Today,
                    Score = "90",
                    Sex = "Male",
                    TEL = "1231456789"
                },
                new Doctor
                {
                    DoctorId = 2,
                    Name = "Name Tes1t1",
                    AreaCode = "45678",
                    Birth = DateTime.Today,
                    Score = "42",
                    Sex = "Female",
                },
                new Doctor
                {
                    DoctorId = 3,
                    Name = "Test Name",
                    AreaCode = "12345",
                    Birth = DateTime.Today,
                    Score = "90",
                    Sex = "Male",
                    TEL = "1231456789"
                }
            };
        }

        private static IEnumerable<Doctor> ExpectedNameMatchedDoctorsList()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    Name = "Name Test2",
                    AreaCode = "12345",
                    Birth = DateTime.Today,
                    Score = "90",
                    Sex = "Male",
                    TEL = "1231456789",
                    Specialties = new List<Specialties>
                    {
                        new Specialties
                        {
                            Name = "test",
                            SpecialtyId = 1
                        },
                        new Specialties
                        {
                            Name = "tes1",
                            SpecialtyId = 2
                        },
                    }
                },
                new Doctor
                {
                    DoctorId = 2,
                    Name = "Name Tes1t1",
                    AreaCode = "45678",
                    Birth = DateTime.Today,
                    Score = "42",
                    Sex = "Female",
                    Specialties = new List<Specialties>
                    {
                        new Specialties
                        {
                            Name = "test",
                            SpecialtyId = 1
                        },
                        new Specialties
                        {
                            Name = "tes1",
                            SpecialtyId = 2
                        },
                    }
                }
            };
        }
        #endregion

        #region Positive Tests
        /*call getdoctors functions
         * expected: return doctor lists and Specialties
         */
        [TestMethod, TestCategory("UnitTest")]
        public void GetDoctors_ShouldReturnAllDoctors()
        {
            GetDoctorsSetup();
            var response =  _controller.GetDoctors();
            //verify return 2 objects
            response.Count().ShouldBeEquivalentTo(2);
            //verify exact match
            response.ShouldBeEquivalentTo(ExpectedDoctorList());
            //verify really call the queries
            _dapperMock.Verify(d => d.Query<Doctor>("RdssqlServerConnection", "GetDoctors", null), Times.Once);
            _dapperMock.Verify(d => d.Query<Specialties>("RdssqlServerConnection", "GetSpecialtiesByDoctorId", It.IsAny<object>()), Times.Exactly(2));
        }


        /*Call SearchSimilarDoctors method
         * moq database to pass only one exact match doctor
         * expect the doctor returned
         */
        [TestMethod, TestCategory("UnitTest")]
        public void SearchSimilarDoctors_exactMatch()
        {
            SearchSimilarDoctors_exactMatchSetup();
            var response = _controller.SearchSimilarDoctors(PostBody());
            //verify return 2 objects
            response.Count().ShouldBeEquivalentTo(1);
            //verify exact match
            response.FirstOrDefault().ShouldBeEquivalentTo(PostBody());
            //verify really call the queries
            _dapperMock.Verify(d => d.Query<Doctor>("RdssqlServerConnection", "GetDoctors", null), Times.Once);
            _dapperMock.Verify(d => d.Query<Specialties>("RdssqlServerConnection", "GetSpecialtiesByDoctorId", It.IsAny<object>()), Times.Once);
        }

        /*Call SearchSimilarDoctors method
         * moq database to pass 3 doctors which one of them is not match the name similar
         * expect the doctor is not returned
         */
        [TestMethod, TestCategory("UnitTest")]
        public void SearchSimilarDoctors_NameSimilar()
        {
            SearchSimilarDoctors_NameSimilarSetup();
            var response = _controller.SearchSimilarDoctors(PostBody());
            //verify return 2 objects
            response.Count().ShouldBeEquivalentTo(2);
            //verify exact match
            response.ShouldBeEquivalentTo(ExpectedNameMatchedDoctorsList());
            //verify really call the queries
            _dapperMock.Verify(d => d.Query<Doctor>("RdssqlServerConnection", "GetDoctors", null), Times.Once);
            _dapperMock.Verify(d => d.Query<Specialties>("RdssqlServerConnection", "GetSpecialtiesByDoctorId", It.IsAny<object>()), Times.Exactly(3));
        }

        /*Call SearchSimilarDoctors method
         * moq database to pass 3 doctors which one of them's Specialties is not match the Specialties similar
         * which mean Specialties is not in the user's required
         * expect the doctor is not returned
         */
        [TestMethod, TestCategory("UnitTest")]
        public void SearchSimilarDoctors_SpecialtiesSimilar()
        {
            SearchSimilarDoctors_SpecialtiesSimilarSetup();
            var response = _controller.SearchSimilarDoctors(PostBody());
            //verify return 2 objects
            response.Count().ShouldBeEquivalentTo(0);
            //verify really call the queries
            _dapperMock.Verify(d => d.Query<Doctor>("RdssqlServerConnection", "GetDoctors", null), Times.Once);
            _dapperMock.Verify(d => d.Query<Specialties>("RdssqlServerConnection", "GetSpecialtiesByDoctorId", It.IsAny<object>()), Times.Exactly(3));
        }
        #endregion

        #region Negative Tests
        /*If not pass factory obj to constructor
         * Expected, throw an exception
         */
        [TestMethod, TestCategory("UnitTest")]
        public void DoctorController_Constructor_shouldThrow()
        {
            Action act = () =>
            {
                new DoctorsController(null);
            };
            act.ShouldThrow<ArgumentNullException>().WithMessage("Value cannot be null.\r\nParameter name: factory");
        }

        #endregion
    }
}
