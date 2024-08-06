using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using VMS.Data;
using VMS.Models;
using VMS.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    public class StatisticsRepositoryTest
    {
        private VisitorManagementDbContext _context;
        private StatisticsRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<VisitorManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestStatisticsDb")
                .Options;

            _context = new VisitorManagementDbContext(options);
            _repository = new StatisticsRepository(_context);

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
    _context.OfficeLocations.AddRange(
        new OfficeLocation { Id = 1, Name = "Head Office", Address = "123 Main St", Phone = "1234567890" },
        new OfficeLocation { Id = 2, Name = "Branch Office", Address = "456 Elm St", Phone = "0987654321" }
    );

    _context.Roles.Add(new Role { Id = 1, Name = "Security" });

    _context.Users.AddRange(
        new User { Id = 1, Username = "user1", Password = "super123" },
        new User { Id = 2, Username = "user2", Password = "super123" }
    );

    _context.UserRoles.AddRange(
        new UserRole { UserId = 1, RoleId = 1 },
        new UserRole { UserId = 2, RoleId = 1 }
    );

    _context.UserDetails.AddRange(
        new UserDetail { UserId = 1, OfficeLocationId = 1, FirstName = "John", LastName = "Doe", Phone = "1111111111" },
        new UserDetail { UserId = 2, OfficeLocationId = 2, FirstName = "Jane", LastName = "Smith", Phone = "2222222222" }
    );

    byte[] dummyPhoto = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }; // Dummy JPEG file header

    _context.Visitors.AddRange(
        new Visitor
        {
            Id = 1,
            OfficeLocationId = 1,
            VisitDate = DateTime.Now.AddDays(-1),
            CheckInTime = DateTime.Now.AddDays(-1),
            Name = "Visitor 1",
            Phone = "3333333333",
            HostName = "Host 1",
            PurposeId = 1,
            CheckedInBy = 1,
            VisitorPassCode = 1,
            Photo = dummyPhoto
        },
        new Visitor
        {
            Id = 2,
            OfficeLocationId = 1,
            VisitDate = DateTime.Now.AddDays(-1),
            CheckInTime = DateTime.Now.AddDays(-1),
            Name = "Visitor 2",
            Phone = "4444444444",
            HostName = "Host 2",
            PurposeId = 2,
            CheckedInBy = 1,
            VisitorPassCode = 2,
            Photo = dummyPhoto
        },
        new Visitor
        {
            Id = 3,
            OfficeLocationId = 2,
            VisitDate = DateTime.Now.AddDays(-1),
            CheckInTime = DateTime.Now.AddDays(-1),
            Name = "Visitor 3",
            Phone = "5555555555",
            HostName = "Host 3",
            PurposeId = 1,
            CheckedInBy = 2,
            VisitorPassCode = 3,
            Photo = dummyPhoto
        }
    );

    _context.PurposeOfVisits.AddRange(
        new PurposeOfVisit { Id = 1, Name = "Meeting" },
        new PurposeOfVisit { Id = 2, Name = "Interview" }
    );

    _context.SaveChanges();
}

/*        private void SeedDatabase()
        {
            _context.OfficeLocations.AddRange(
                new OfficeLocation { Id = 1, Name = "Head Office", Address = "123 Main St", Phone = "1234567890" },
                new OfficeLocation { Id = 2, Name = "Branch Office", Address = "456 Elm St", Phone = "0987654321" }
            );

            _context.Roles.Add(new Role { Id = 1, Name = "Security" });

            _context.Users.AddRange(
                new User { Id = 1, Username = "user1", Password = "super123" },
                new User { Id = 2, Username = "user2", Password = "super123" }
            );

            _context.UserRoles.AddRange(
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 1 }
            );

            _context.UserDetails.AddRange(
                new UserDetail { UserId = 1, OfficeLocationId = 1, FirstName = "John", LastName = "Doe", Phone = "1111111111" },
                new UserDetail { UserId = 2, OfficeLocationId = 2, FirstName = "Jane", LastName = "Smith", Phone = "2222222222" }
            );

            byte[] dummyPhoto = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }; // Dummy JPEG file header

            _context.Visitors.AddRange(
                new Visitor
                {
                    Id = 1,
                    OfficeLocationId = 1,
                    VisitDate = DateTime.Now,
                    CheckInTime = DateTime.Now,
                    Name = "Visitor 1",
                    Phone = "3333333333",
                    HostName = "Host 1",
                    PurposeId = 1,
                    StaffId = 1,
                    VisitorPassCode = 1,
                    Photo = dummyPhoto
                },
                new Visitor
                {
                    Id = 2,
                    OfficeLocationId = 1,
                    VisitDate = DateTime.Now,
                    CheckInTime = DateTime.Now,
                    Name = "Visitor 2",
                    Phone = "4444444444",
                    HostName = "Host 2",
                    PurposeId = 2,
                    StaffId = 1,
                    VisitorPassCode = 2,
                    Photo = dummyPhoto
                },
                new Visitor
                {
                    Id = 3,
                    OfficeLocationId = 2,
                    VisitDate = DateTime.Now,
                    CheckInTime = DateTime.Now,
                    Name = "Visitor 3",
                    Phone = "5555555555",
                    HostName = "Host 3",
                    PurposeId = 1,
                    StaffId = 2,
                    VisitorPassCode = 001,
                    Photo = dummyPhoto
                }
            );

            _context.PurposeOfVisits.AddRange(
                new PurposeOfVisit { Id = 1, Name = "Meeting" },
                new PurposeOfVisit { Id = 2, Name = "Interview" }
            );

            _context.SaveChanges();
        }
*/
        [Test]
        public async Task GetLocationStatistics_ReturnsCorrectData()
        {
            var result = await _repository.GetLocationStatistics(30);

            Assert.That(result.Count(), Is.EqualTo(2));
            var headOfficeStats = result.First(r => r.Location == "Head Office");
            Assert.That(headOfficeStats.NumberOfSecurity, Is.EqualTo(1));
            Assert.That(headOfficeStats.PassesGenerated, Is.EqualTo(2));
            Assert.That(headOfficeStats.TotalVisitors, Is.EqualTo(2));
        }





        [Test]
        public async Task GetPurposeStatistics_ReturnsCorrectData()
        {
            var result = await _repository.GetPurposeStatistics();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Sum(r => r.Value), Is.EqualTo(3));
        }

        [Test]
        public async Task GetDashboardStatistics_ReturnsCorrectData()
        {
            var result = await _repository.GetDashboardStatistics();

            Assert.That(result.Count(), Is.EqualTo(2));
            var headOfficeStats = result.First(r => r.Location == "Head Office");
            Assert.That(headOfficeStats.PassesGenerated, Is.EqualTo(2));
            Assert.That(headOfficeStats.TotalVisitors, Is.EqualTo(2));
        }





        [Test]
        public async Task GetPurposeStatistics_ReturnsZeroValues_WhenNoVisitorsInLast30Days()
        {
            _context.Visitors.RemoveRange(_context.Visitors);
            await _context.SaveChangesAsync();

            var result = await _repository.GetPurposeStatistics();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.All(r => r.Value == 0), Is.True);
        }

        [Test]
        public async Task GetDashboardStatistics_ReturnsZeroValues_WhenNoVisitors()
        {
            _context.Visitors.RemoveRange(_context.Visitors);
            await _context.SaveChangesAsync();

            var result = await _repository.GetDashboardStatistics();

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.All(r => r.PassesGenerated == 0 && r.ActiveVisitors == 0 && r.TotalVisitors == 0), Is.True);
        }

        [Test]
        public async Task GetLocationStatistics_HandlesMultipleLocations()
        {
            var result = await _repository.GetLocationStatistics(30);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Select(r => r.Location), Is.EquivalentTo(new[] { "Head Office", "Branch Office" }));
        }

        [Test]
        public async Task GetSecurityStatistics_HandlesMultipleSecurityPersonnel()
        {
            var result = await _repository.GetSecurityStatistics(30);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Select(r => r.Location), Is.EquivalentTo(new[] { "Head Office", "Branch Office" }));
        }
    }
}