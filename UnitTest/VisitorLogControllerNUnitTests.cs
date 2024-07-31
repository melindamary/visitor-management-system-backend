using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository;

namespace UnitTest
{
    [TestFixture]
    public class VisitorRepositoryTests
    {
        private VisitorManagementDbContext _context;
        private VisitorRepository _repository;
        private IMapper _mapper;
        private ILogger<VisitorRepository> _logger;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<VisitorManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "VisitorManagementDb")
                .Options;

            _context = new VisitorManagementDbContext(options);
            _mapper = new MapperConfiguration(cfg => {
                // Add your AutoMapper profile configurations here
                cfg.CreateMap<Visitor, VisitorLogDTO>();
                cfg.CreateMap<VisitorLogDTO, Visitor>();
            }).CreateMapper();

            _logger = new LoggerFactory().CreateLogger<VisitorRepository>();
            _repository = new VisitorRepository(_context, _mapper, _logger);

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
            var visitors = new List<Visitor>
            {
                new Visitor
                {
                    Id = 1, Name = "Visitor1", Phone = "1111111111", VisitDate = DateTime.Today,
                    HostName = "Host1", PurposeId = 1, CheckInTime = DateTime.Now.AddHours(-1),
                    CheckOutTime = null, VisitorPassCode = 1234, Photo = new byte[] { 0x01, 0x02 } // Placeholder
                },
                new Visitor
                {
                    Id = 2, Name = "Visitor2", Phone = "2222222222", VisitDate = DateTime.Today,
                    HostName = "Host2", PurposeId = 2, CheckInTime = DateTime.Now.AddHours(-2),
                    CheckOutTime = DateTime.Now.AddHours(-1), VisitorPassCode = 5678, Photo = new byte[] { 0x01, 0x02 } // Placeholder
                }
            };

            _context.Visitors.AddRange(visitors);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetVisitorByIdAsync_ReturnsCorrectVisitor()
        {
            var visitor = await _repository.GetVisitorByIdAsync(1);

            Assert.That(visitor, Is.Not.Null);
            Assert.That(visitor.Name, Is.EqualTo("Visitor1"));
            Assert.That(visitor.Phone, Is.EqualTo("1111111111"));
        }

        [Test]
        public async Task GetVisitorCount_ReturnsCorrectCount()
        {
            Func<IQueryable<Visitor>, IQueryable<Visitor>> filter = visitors => visitors;

            var count = await _repository.GetVisitorCount(filter);

            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateCheckInTimeAndCardNumber_UpdatesVisitor()
        {
            var updateDto = new UpdateVisitorPassCodeDTO { VisitorPassCode = 4321 };

            var updatedVisitor = await _repository.UpdateCheckInTimeAndCardNumber(1, updateDto);

            Assert.That(updatedVisitor, Is.Not.Null);
            Assert.That(updatedVisitor.VisitorPassCode, Is.EqualTo(4321));
            Assert.That(updatedVisitor.CheckInTime, Is.Not.Null);
        }

        [Test]
        public async Task UpdateCheckOutTime_UpdatesVisitorCheckOutTime()
        {
            var updatedVisitor = await _repository.UpdateCheckOutTime(1);

            Assert.That(updatedVisitor, Is.Not.Null);
            Assert.That(updatedVisitor.CheckOutTime, Is.Not.Null);
            Assert.That(updatedVisitor.VisitorPassCode, Is.EqualTo(0)); // PassCode should be reset to 0
        }

        // Additional tests with placeholders or simple checks
        [Test]
        public async Task GetVisitorByIdAsync_ReturnsNullForNonExistentVisitor()
        {
            var visitor = await _repository.GetVisitorByIdAsync(999);

            Assert.That(visitor, Is.Null);
        }

        [Test]
        public async Task UpdateCheckInTimeAndCardNumber_HandlesNonExistentVisitor()
        {
            var updateDto = new UpdateVisitorPassCodeDTO { VisitorPassCode = 9999 };

            var updatedVisitor = await _repository.UpdateCheckInTimeAndCardNumber(999, updateDto);

            Assert.That(updatedVisitor, Is.Null);
        }

        [Test]
        public async Task GetVisitorLogs_EmptyDatabaseReturnsEmptyList()
        {
            _context.Visitors.RemoveRange(_context.Visitors);
            _context.SaveChanges();

            var logs = await _repository.GetVisitorLogs(v => v);

            Assert.That(logs, Is.Empty);
        }

        [Test]
        public async Task UpdateCheckOutTime_HandlesNonExistentVisitor()
        {
            var updatedVisitor = await _repository.UpdateCheckOutTime(999);

            Assert.That(updatedVisitor, Is.Null);
        }

        [Test]
        public async Task GetVisitorCount_EmptyDatabaseReturnsZero()
        {
            _context.Visitors.RemoveRange(_context.Visitors);
            _context.SaveChanges();

            Func<IQueryable<Visitor>, IQueryable<Visitor>> filter = visitors => visitors;

            var count = await _repository.GetVisitorCount(filter);

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateCheckInTimeAndCardNumber_ThrowsExceptionForDuplicatePassCode()
        {
            var updateDto = new UpdateVisitorPassCodeDTO { VisitorPassCode = 5678 };

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _repository.UpdateCheckInTimeAndCardNumber(1, updateDto));
        }
    }
}
