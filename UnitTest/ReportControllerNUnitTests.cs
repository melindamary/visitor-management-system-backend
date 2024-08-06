using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Repository;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture]
    public class ReportControllerNUnitTests
    {

        private VisitorManagementDbContext _context;
        private ReportRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<VisitorManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "VisitorManagementDb")
                .Options;

            _context = new VisitorManagementDbContext(options);
            _repository = new ReportRepository(_context);

            // Seed the database with some data for testing
            SeedDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedDatabase()
        {
            var purposes = new List<PurposeOfVisit>
            {
                new PurposeOfVisit { Id = 1, Name = "Meeting" },
                new PurposeOfVisit { Id = 2, Name = "Interview" }
            };

            var locations = new List<OfficeLocation>
            {
                new OfficeLocation { Id = 1, Name = "Head Office", Address = "Trivandrum", Phone = "9087654321" },
                new OfficeLocation { Id = 2, Name = "Branch Office", Address = "Cochin", Phone = "9087654322" }
            };

            var users = new List<UserDetail>
            {
                new UserDetail { UserId = 1, FirstName = "John", LastName = "Doe", Phone = "1234567890" },
                new UserDetail { UserId = 2, FirstName = "Jane", LastName = "Smith", Phone = "0987654321" }
            };

            var visitors = new List<Visitor>
            {
                new Visitor
                {
                    Id = 1, Name = "Visitor1", Phone = "1111111111", VisitDate = DateTime.Today,
                    HostName = "Host1", PurposeId = 1, OfficeLocationId = 1, CheckedInBy = 1,
                    CheckInTime = DateTime.Now.AddHours(-1), CheckOutTime = DateTime.Now.AddMinutes(-30),
                    Photo = new byte[] { 0x20 }
                },
                new Visitor
                {
                    Id = 2, Name = "Visitor2", Phone = "2222222222", VisitDate = DateTime.Today,
                    HostName = "Host2", PurposeId = 2, OfficeLocationId = 2, CheckedInBy = 2,
                    CheckInTime = DateTime.Now.AddHours(-2), CheckOutTime = DateTime.Now.AddMinutes(-60),
                    Photo = new byte[] { 0x20 }
                }
            };

            var devices = new List<Device>
            {
                new Device { Id = 1, Name = "Laptop" },
                new Device { Id = 2, Name = "Phone" }
            };

            var visitorDevices = new List<VisitorDevice>
            {
                new VisitorDevice { VisitorId = 1, DeviceId = 1, SerialNumber = "L123" },
                new VisitorDevice { VisitorId = 1, DeviceId = 2, SerialNumber = "P123" },
                new VisitorDevice { VisitorId = 2, DeviceId = 1, SerialNumber = "L456" }
            };

            _context.PurposeOfVisits.AddRange(purposes);
            _context.OfficeLocations.AddRange(locations);
            _context.UserDetails.AddRange(users);
            _context.Visitors.AddRange(visitors);
            _context.Devices.AddRange(devices);
            _context.VisitorDevices.AddRange(visitorDevices);

            _context.SaveChanges();
        }

        [Test]
        public async Task GetVisitorCount_ReturnsCountOfVisitorsStored_CheckIfCountIsCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();

            // Assert
            Assert.That(2, Is.EqualTo(result.Count()));
        }
        [Test]
        public async Task GetAllVisitors_ReturnsListOfVisitors_CheckIfFirstVisitorDataIsCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();

            // Assert
            var visitor = result.First();
            Assert.That("Visitor1", Is.EqualTo(visitor.VisitorName));
            Assert.That("Host1", Is.EqualTo(visitor.HostName));
            Assert.That(2, Is.EqualTo(visitor.DeviceCount));
        }

        [Test]
        public async Task GetVisitorDevicesCount_ReturnsVisitorList_ChecksIfCountOfVisitorDevicesIsCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            Assert.That(2, Is.EqualTo(visitorList[0].Devices.Count));
        }

        [Test]
        public async Task GetListOfDevicesForAVisitor_ReturnsVisitorList_ChecksIfSerialNumberAndNameOfDevicesCarriedByVisitorIsCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            Assert.That("L123", Is.EqualTo(visitorList[0].Devices[0].SerialNumber));
            Assert.That("Laptop", Is.EqualTo(visitorList[0].Devices[0].Name));
            Assert.That("P123", Is.EqualTo(visitorList[0].Devices[1].SerialNumber));
            Assert.That("Phone", Is.EqualTo(visitorList[0].Devices[1].Name));
        }

        [Test]
        public async Task GetVisitorCheckInTime_ReturnsVisitorList_ChecksIfVisitorCheckInTimeIsNotEmpty()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            Assert.That(visitorList[0].CheckInTime, Is.Not.Null);
        }

        [Test]
        public async Task GetVisitorCheckOutTime_ReturnsVisitorList_ChecksIfVisitorCheckOutTimeIsNotEmpty()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            Assert.That(visitorList[0].CheckOutTime, Is.Not.Null);
        }

        [Test]
        public async Task GetAllVisitorsPurposeOfVisits_ReturnsVisitorList_ChecksIfVisitorPurposeOfVisitsAreCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            Assert.That("Meeting", Is.EqualTo(visitorList[0].PurposeName));
            Assert.That("Interview", Is.EqualTo(visitorList[1].PurposeName));
        }

        [Test]
        public async Task GetAllVisitorsLocationNames_ReturnsVisitorList_ChecksIfLocationNamesOfVisitorsAreCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            Assert.That("Head Office", Is.EqualTo(visitorList[0].LocationName));
            Assert.That("Branch Office", Is.EqualTo(visitorList[1].LocationName));
        }

        [Test]
        public async Task GetNamesOfStaffsWhoHaveCheckedInVisitors_ReturnsVisitorList_ChecksIfTheStaffNamesReturnedAreCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            Assert.That("John Doe", Is.EqualTo(visitorList[0].StaffName));
            Assert.That("Jane Smith", Is.EqualTo(visitorList[1].StaffName));
        }

        [Test]
        public async Task GetPhoneNumbersOfStaffsWhoHaveCheckedInVisitors_ReturnsVisitorList_ChecksIfThePhoneNumbersReturnedAreCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            Assert.That("1234567890", Is.EqualTo(visitorList[0].StaffPhoneNumber));
            Assert.That("0987654321", Is.EqualTo(visitorList[1].StaffPhoneNumber));
        }

        [Test]
        public async Task GetFirstVisitorPhoto_ValidatesPhotoData_IsBase64String()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitor = result.First();

            // Assert
            Assert.That(visitor.Photo, Does.StartWith("IA"), "The photo data should start with expected Base64 characters.");
        }

        [Test]
        public async Task GetAllVisitorsAsync_DoesNotReturnIncorrectVisitorData()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();

            // Assert
            Assert.That(result.All(v => v.VisitorName != "NonExistingVisitor"), "There should be no visitor with the name 'NonExistingVisitor'.");
        }

        [Test]
        public async Task GetAllVisitorsAsync_CheckInTime_FormatIsCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitor = result.First();

            // Assert
            Assert.That(visitor.CheckInTime, Is.TypeOf<DateTime>(), "The check-in time should be of type DateTime.");
        }

        [Test]
        public async Task GetAllVisitorsAsync_CheckOutTime_FormatIsCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitor = result.First();

            // Assert
            Assert.That(visitor.CheckOutTime, Is.TypeOf<DateTime>(), "The check-out time should be of type DateTime.");
        }

        [Test]
        public async Task GetFirstVisitorName_ReturnsVisitorList_ChecksIfFirstVisitorNameIsCorrect()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();

            // Assert
            Assert.That(result.First(v => v.VisitorName == "Visitor1").VisitorName, Is.EqualTo("Visitor1"), "The visitor name should be 'Visitor1'.");
        }

        [Test]
        public async Task GetVisitorPhoto_ReturnsFirstVisitor_CheckIfPhotoIsEmpty() { 
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitor = result.First();

            // Assert
            Assert.That(visitor.Photo, Is.Not.Null, "Photo data should not be null.");
        }

        [Test]
        public async Task GetVisitorDevicesCount_ReturnsVisitorList_CheckIfThereAreCorrectNumberOfDevicesForEachVisitor()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            Assert.That(visitorList[0].Devices.Count, Is.EqualTo(2), "The number of devices for the first visitor should be 2.");
            Assert.That(visitorList[1].Devices.Count, Is.EqualTo(1), "The number of devices for the second visitor should be 1.");
        }

        [Test]
        public async Task GetVisitorName_GetVisitorList_CheckIfVisitorNameIsString()
        {
            // Act
            var result = await _repository.GetAllVisitorsAsync();
            var visitorList = result.ToList();

            // Assert
            foreach (var visitor in visitorList)
            {
                Assert.That(visitor.VisitorName, Is.InstanceOf<string>(),
                    "Visitor name should be a string.");
            }
        }
    }
}


