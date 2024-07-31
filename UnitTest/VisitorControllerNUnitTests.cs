using Microsoft.EntityFrameworkCore;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    public class VisitorFormRepositoryTests
    {
        private VisitorManagementDbContext _context;
        private VisitorFormRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<VisitorManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "VisitorManagementDb")
                .Options;

            _context = new VisitorManagementDbContext(options);
            _repository = new VisitorFormRepository(_context);

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
                new OfficeLocation { Id = 1, Name = "Head Office" },
                new OfficeLocation { Id = 2, Name = "Branch Office" }
            };

            var visitors = new List<Visitor>
            {
                new Visitor { Id = 1, Name = "Visitor1", Phone = "1111111111", PurposeId = 1, HostName = "Host1", OfficeLocationId = 1, StaffId = 1, VisitDate = DateTime.Today },
                new Visitor { Id = 2, Name = "Visitor2", Phone = "2222222222", PurposeId = 2, HostName = "Host2", OfficeLocationId = 2, StaffId = 1, VisitDate = DateTime.Today }
            };

            var devices = new List<VisitorDevice>
            {
                new VisitorDevice { VisitorId = 1, DeviceId = 1, SerialNumber = "L123" },
                new VisitorDevice { VisitorId = 1, DeviceId = 2, SerialNumber = "P123" },
                new VisitorDevice { VisitorId = 2, DeviceId = 1, SerialNumber = "L456" }
            };

            _context.PurposeOfVisits.AddRange(purposes);
            _context.OfficeLocations.AddRange(locations);
            _context.Visitors.AddRange(visitors);
            _context.VisitorDevices.AddRange(devices);

            _context.SaveChanges();
        }

        [Test]
        public async Task AddVisitorDeviceAsync_WhenDeviceIsValid_AddsDevice()
        {
            // Arrange
            var addDeviceDto = new AddVisitorDeviceDTO
            {
                VisitorId = 1,
                DeviceId = 3,
                SerialNumber = "D123"
            };

            // Act
            var result = await _repository.AddVisitorDeviceAsync(addDeviceDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.SerialNumber, Is.EqualTo("D123"));
            Assert.That(result.CreatedBy, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateVisitorAsync_WhenVisitorIsValid_AddsVisitor()
        {
            // Arrange
            var visitorDto = new VisitorCreationDTO
            {
                Name = "Visitor3",
                PhoneNumber = "3333333333",
                PurposeOfVisitId = 1,
                PersonInContact = "Host3",
                OfficeLocationId = 1,
                ImageData = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABkAAAAMCAYAAABwY..."
            };

            // Act
            var result = await _repository.CreateVisitorAsync(visitorDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Visitor3"));
            Assert.That(result.Photo, Is.Not.Null);
        }

        [Test]
        public void CreateVisitorAsync_WhenVisitorIsNull_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.CreateVisitorAsync(null));
        }

        [Test]
        public async Task GetPersonInContactAsync_WhenCalled_ReturnsDistinctHostNames()
        {
            // Act
            var result = await _repository.GetPersonInContactAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result, Does.Contain("Host1"));
            Assert.That(result, Does.Contain("Host2"));
        }

        [Test]
        public async Task GetVisitorByIdAsync_WhenIdIsValid_ReturnsVisitor()
        {
            // Act
            var result = await _repository.GetVisitorByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Visitor1"));
        }

        [Test]
        public async Task GetVisitorByIdAsync_WhenIdIsInvalid_ReturnsNull()
        {
            // Act
            var result = await _repository.GetVisitorByIdAsync(99);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetVisitorDetailsAsync_WhenCalled_ReturnsAllVisitors()
        {
            // Act
            var result = await _repository.GetVisitorDetailsAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task SaveAsync_WhenCalled_SavesChangesToDatabase()
        {
            // Arrange
            var visitor = new Visitor { Name = "New Visitor", Phone = "4444444444", PurposeId = 1, HostName = "Host4", OfficeLocationId = 1, StaffId = 1, VisitDate = DateTime.Today };
            _context.Visitors.Add(visitor);

            // Act
            await _repository.SaveAsync();

            // Assert
            var result = await _context.Visitors.FirstOrDefaultAsync(v => v.Name == "New Visitor");
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task AddVisitorDeviceAsync_WhenVisitorIdIsInvalid_ThrowsException()
        {
            // Arrange
            var addDeviceDto = new AddVisitorDeviceDTO
            {
                VisitorId = 99,
                DeviceId = 1,
                SerialNumber = "D123"
            };

            // Act & Assert
            Assert.ThrowsAsync<DbUpdateException>(async () => await _repository.AddVisitorDeviceAsync(addDeviceDto));
        }

        [Test]
        public async Task CreateVisitorAsync_WhenVisitorIsValidAndHasDevices_AddsVisitorAndDevices()
        {
            // Arrange
            var visitorDto = new VisitorCreationDTO
            {
                Name = "Visitor4",
                PhoneNumber = "4444444444",
                PurposeOfVisitId = 1,
                PersonInContact = "Host4",
                OfficeLocationId = 1,
                SelectedDevice = new List<VisitorDeviceDTO>
                {
                    new VisitorDeviceDTO { DeviceId = 1, SerialNumber = "D123" },
                    new VisitorDeviceDTO { DeviceId = 2, SerialNumber = "D456" }
                }
            };

            // Act
            var result = await _repository.CreateVisitorAsync(visitorDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.VisitorDevices.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetPersonInContactAsync_WhenCalled_ReturnsDistinctHostNamesInOrder()
        {
            // Act
            var result = await _repository.GetPersonInContactAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First(), Is.EqualTo("Host1"));
        }

        [Test]
        public async Task GetVisitorByIdAsync_WhenIdIsValid_ReturnsVisitorWithCorrectPhone()
        {
            // Act
            var result = await _repository.GetVisitorByIdAsync(2);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Phone, Is.EqualTo("2222222222"));
        }

        [Test]
        public async Task CreateVisitorAsync_WhenCalled_SetsDefaultPassCode()
        {
            // Arrange
            var visitorDto = new VisitorCreationDTO
            {
                Name = "Visitor5",
                PhoneNumber = "5555555555",
                PurposeOfVisitId = 1,
                PersonInContact = "Host5",
                OfficeLocationId = 1
            };

            // Act
            var result = await _repository.CreateVisitorAsync(visitorDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.VisitorPassCode, Is.EqualTo(0));
        }

        [Test]
        public async Task AddVisitorDeviceAsync_WhenDeviceIsValid_SetsCreatedBy()
        {
            // Arrange
            var addDeviceDto = new AddVisitorDeviceDTO
            {
                VisitorId = 2,
                DeviceId = 2,
                SerialNumber = "D789"
            };

            // Act
            var result = await _repository.AddVisitorDeviceAsync(addDeviceDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CreatedBy, Is.EqualTo(1));
        }

        [Test]
        public async Task GetVisitorDetailsAsync_WhenCalled_ValidatesVisitorCount()
        {
            // Act
            var result = await _repository.GetVisitorDetailsAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CreateVisitorAsync_WhenImageDataIsNull_DoesNotSavePhoto()
        {
            // Arrange
            var visitorDto = new VisitorCreationDTO
            {
                Name = "Visitor6",
                PhoneNumber = "6666666666",
                PurposeOfVisitId = 1,
                PersonInContact = "Host6",
                OfficeLocationId = 1
            };

            // Act
            var result = await _repository.CreateVisitorAsync(visitorDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Photo, Is.Null);
        }

        [Test]
        public async Task AddVisitorDeviceAsync_WhenSerialNumberIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            var addDeviceDto = new AddVisitorDeviceDTO
            {
                VisitorId = 1,
                DeviceId = 2,
                SerialNumber = ""
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _repository.AddVisitorDeviceAsync(addDeviceDto));
        }

        [Test]
        public async Task CreateVisitorAsync_WhenPurposeIdIsOutOfRange_ThrowsDbUpdateException()
        {
            // Arrange
            var visitorDto = new VisitorCreationDTO
            {
                Name = "Visitor7",
                PhoneNumber = "7777777777",
                PurposeOfVisitId = 99,
                PersonInContact = "Host7",
                OfficeLocationId = 1
            };

            // Act & Assert
            Assert.ThrowsAsync<DbUpdateException>(async () => await _repository.CreateVisitorAsync(visitorDto));
        }
    }
}
