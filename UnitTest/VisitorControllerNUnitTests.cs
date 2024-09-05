using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using VMS.AVHubs;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository;
using VMS.Repository.IRepository;
using VMS.Services;


namespace UnitTest
{
    public class TestHubContext : IHubContext<VisitorHub>
    {
        public IHubClients Clients { get; } = null;
        public IGroupManager Groups { get; } = null;
    }

    public class TestStatisticsRepository : IStatisticsRepository
    {
        // Implement methods or properties as needed
        public Task<IEnumerable<DashboardStatisticsDTO>> GetDashboardStatistics()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LocationStatisticsDTO>> GetLocationStatistics(int days)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PurposeStatisticsDTO>> GetPurposeStatistics()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SecurityStatisticsDTO>> GetSecurityStatistics(int days)
        {
            throw new NotImplementedException();
        }
    }

    public class TestDashboardService : DashboardService
    {
        public TestDashboardService(VisitorManagementDbContext context, IStatisticsRepository statisticsRepository)
            : base(context, statisticsRepository)
        {
            // Initialize any additional properties or fields if necessary
        }
    }

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
            var hubContext = new TestHubContext();
            var statisticsRepository = new TestStatisticsRepository();
            var dashboardService = new TestDashboardService(_context, statisticsRepository);

            // Pass them to the repository constructor
            _repository = new VisitorFormRepository(_context, hubContext, dashboardService);

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
                new Visitor { Id = 1, Name = "Visitor1", Phone = "1111111111", PurposeId = 1, HostName = "Host1", OfficeLocationId = 1, CheckedInBy = 1, VisitDate = DateTime.Today },
                new Visitor { Id = 2, Name = "Visitor2", Phone = "2222222222", PurposeId = 2, HostName = "Host2", OfficeLocationId = 2, CheckedInBy = 1, VisitDate = DateTime.Today }
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
            string validBase64ImageData = "data:image/jpeg;base64,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";
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
