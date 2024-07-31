using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using VMS.Data;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Repository;

namespace UnitTest
{
    [TestFixture]
    public class AdminRoleRepositoryTests
    {
        private VisitorManagementDbContext _context;
        private AdminRoleRepository _repository;
        private ILogger<AdminRoleRepository> _logger;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<VisitorManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "AdminRoleDb")
                .Options;

            _context = new VisitorManagementDbContext(options);
            _logger = new LoggerFactory().CreateLogger<AdminRoleRepository>();
            _repository = new AdminRoleRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Roles.AddRange(
                  new Role { Id = 1, Name = "Admin" },
                  new Role { Id = 2, Name = "User" }
              );

            _context.Pages.AddRange(
                new Page { Id = 1, Name = "Dashboard", Url = "/dashboard" },
                new Page { Id = 2, Name = "Users", Url = "/users" }
            );
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllRolesAsync_ReturnsAllRoles()
        {
            var result = await _repository.GetRoleIdAndNameAsync();
            Assert.That(result, Has.Count.EqualTo(2));
        }

       
/*        public async Task GetRoleByIdAsync_ReturnsCorrectRole()
        {
            var result = await _repository.GetRoleByIdAsync(1);
            Assert.That(result.Name, Is.EqualTo("Admin"));
        }*/

        [Test]
        public async Task AddRoleAsync_AddsRoleToDatabase()
        {
            var newRole = new AddNewRoleDTO { Name = "Manager" ,CreatedBy=1,status=1,UpdatedBy=1};
            await _repository.CreateRoleAsync(newRole);
            var result = await _repository.GetRoleIdAndNameAsync();
            Assert.That(result, Has.Count.EqualTo(3));
        }
        /*
                [Test]
                public async Task UpdateRoleAsync_UpdatesExistingRole()
                {
                    var role = await _repository.GetRoleByIdAsync(1);
                    role.Name = "Super Admin";
                    await _repository.UpdateRoleAsync(role);
                    var updatedRole = await _repository.GetRoleByIdAsync(1);
                    Assert.That(updatedRole.Name, Is.EqualTo("Super Admin"));
                }*/
        [Test]
        public async Task GetRolesAsync_ReturnsCorrectRole()
        {
            var role = await _repository.GetRolesAsync(1);

            Assert.That(role, Is.Not.Null);
            Assert.That(role.Name, Is.EqualTo("Admin"));
        }
        [Test]
        public async Task GetPagesByRoleIdAsync_ReturnsCorrectPages()
        {
            // Add a PageControl for testing
            _context.PageControls.Add(new PageControl { RoleId = 1, PageId = 1 });
            await _context.SaveChangesAsync();

            var pages = await _repository.GetPagesByRoleIdAsync(1);

            Assert.That(pages.Count(), Is.EqualTo(1));
            Assert.That(pages.First().Name, Is.EqualTo("Dashboard"));
        }
        [Test]
        public async Task CreatePageAsync_CreatesNewPage()
        {
            var newPage = new PageDTO
            {
                PageName = "Reports",
                PageUrl = "/reports",
                CreatedBy = 1,
                UpdatedBy = 1
            };

            var result = await _repository.CreatePageAsync(newPage);

            Assert.That(result.Name, Is.EqualTo("Reports"));
            Assert.That(_context.Pages.Count(), Is.EqualTo(3));
        }
        [Test]
        public async Task UpdateRolePagesAsync_UpdatesRolePages()
        {
            var updateDto = new UpdateRolePagesDTO
            {
                RoleId = 1,
                PageIds = new List<int> { 1, 2 },
                Status = 0,
                UpdatedBy = 1
            };

            await _repository.UpdateRolePagesAsync(updateDto);

            var rolePages = await _context.PageControls.Where(pc => pc.RoleId == 1).ToListAsync();
            Assert.That(rolePages.Count, Is.EqualTo(2));
            Assert.That(rolePages.Select(rp => rp.PageId).OrderBy(id => id), Is.EqualTo(new[] { 1, 2 }));
        }
    }
}