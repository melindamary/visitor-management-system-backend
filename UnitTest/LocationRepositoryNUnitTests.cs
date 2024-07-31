using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using VMS;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository;

namespace UnitTest
{
    [TestFixture]
    public class LocationRepositoryNUnitTests
    {
        private VisitorManagementDbContext _context;
        private LocationRepository _repository;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<VisitorManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "VisitorManagementDb")
                .Options;

            _context = new VisitorManagementDbContext(options);

            // Setup AutoMapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });
            _mapper = config.CreateMapper();

            _repository = new LocationRepository(_context, _mapper);

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
            var users = new List<User>
            {
                new User { Id = 1, Username = "JohnDoe", Password = "password123" },
                new User { Id = 2, Username = "JaneSmith", Password = "password456" }
            };

            var locations = new List<OfficeLocation>
            {
                new OfficeLocation { Id = 1, Name = "Head Office", Address = "Trivandrum", Phone = "9087654321", CreatedBy = 1, CreatedDate = DateTime.Now },
                new OfficeLocation { Id = 2, Name = "Branch Office", Address = "Cochin", Phone = "9087654322", CreatedBy = 2, CreatedDate = DateTime.Now }
            };

            _context.Users.AddRange(users);
            _context.OfficeLocations.AddRange(locations);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllLocationDetailsAsync_ReturnsListOfLocations()
        {
            // Act
            var result = await _repository.GetAllLocationDetailsAsync();

            // Assert
            Assert.That(result, Is.Not.Null, "The result should not be null.");
            Assert.That(result.Count(), Is.EqualTo(2), "The number of locations returned should be 2.");
            Assert.That(result.First().Name, Is.EqualTo("Head Office"), "The first location should be 'Head Office'.");
            Assert.That(result.Last().Name, Is.EqualTo("Branch Office"), "The last location should be 'Branch Office'.");
        }

        [Test]
        public async Task GetLocationByIdAsync_ValidId_ReturnsLocation()
        {
            // Act
            var result = await _repository.GetLocationByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Head Office"));
        }

        [Test]
        public async Task GetLocationIdAndNameAsync_ReturnsListOfIdAndName()
        {
            // Act
            var result = await _repository.GetLocationIdAndNameAsync();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("Head Office"));
        }

        [Test]
        public async Task GetAllLocationAsync_ReturnsAllLocations()
        {
            // Act
            var result = await _repository.GetAllLocationAsync();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetLocationByIdAsync_ValidId_ReturnsCorrectName()
        {
            // Act
            var result = await _repository.GetLocationByIdAsync(1);

            // Assert
            Assert.That(result.Name, Is.EqualTo("Head Office"));
        }

        [Test]
        public async Task GetLocationByIdAsync_ValidId_ReturnsCorrectAddress()
        {
            // Act
            var result = await _repository.GetLocationByIdAsync(1);

            // Assert
            Assert.That(result.Address, Is.EqualTo("Trivandrum"));
        }

        [Test]
        public async Task AddLocationAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var newLocation = new AddOfficeLocationDTO
            {
                Name = "New Office",
                Address = "Delhi",
                Phone = "9087654323",
                Username = "JohnDoe" // Ensure this user exists in the seed data
            };

            // Act
            var result = await _repository.AddLocationAsync(newLocation);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_context.OfficeLocations.Count(), Is.EqualTo(3)); // Include the new location
        }

        [Test]
        public async Task AddLocationAsync_EmptyUsername_ReturnsTrue()
        {
            // Arrange
            var newLocation = new AddOfficeLocationDTO
            {
                Name = "Another Office",
                Address = "Bangalore",
                Phone = "9087654324",
                Username = "" // Empty username
            };

            // Act
            var result = await _repository.AddLocationAsync(newLocation);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(_context.OfficeLocations.Count(), Is.EqualTo(3)); // Include the new location
        }

        [Test]
        public async Task UpdateLocationAsync_ValidData_ReturnsTrue()
        {
            // Arrange
            var updateDto = new UpdateLocationDTO
            {
                Name = "Updated Head Office",
                Address = "Trivandrum Updated",
                Phone = "9087654321",
                Username = "JaneSmith" // Ensure this user exists in the seed data
            };

            // Act
            var result = await _repository.UpdateLocationAsync(1, updateDto);

            // Assert
            Assert.That(result, Is.True);
            var updatedLocation = await _context.OfficeLocations.FindAsync(1);
            Assert.That(updatedLocation.Name, Is.EqualTo("Updated Head Office"));
        }

        [Test]
        public async Task UpdateLocationAsync_NonExistentLocation_ReturnsFalse()
        {
            // Arrange
            var updateDto = new UpdateLocationDTO
            {
                Name = "Nonexistent Office",
                Address = "Nowhere",
                Phone = "0000000000",
                Username = "JohnDoe"
            };

            // Act
            var result = await _repository.UpdateLocationAsync(999, updateDto); // Non-existent ID

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetAllLocationDetailsAsync_WhenNoLocations_ReturnsEmptyList()
        {
            // Arrange
            _context.OfficeLocations.RemoveRange(_context.OfficeLocations);
            _context.SaveChanges();

            // Act
            var result = await _repository.GetAllLocationDetailsAsync();

            // Assert
            Assert.That(result, Is.Empty, "The result should be empty.");
        }

        [Test]
        public async Task GetLocationByIdAsync_InvalidId_ReturnsNull()
        {
            // Act
            var result = await _repository.GetLocationByIdAsync(999); // Non-existent ID

            // Assert
            Assert.That(result, Is.Null);
        }

        
    }
}
