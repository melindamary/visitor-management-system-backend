/*using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using VMS.Repository.IRepository;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Services;
using VMS.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace UnitTest
{
    [TestFixture]
    public class VisitorControllerTests
    {
        private Mock<IVisitorFormService> _mockService;
        private VisitorController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IVisitorFormService>();
            _controller = new VisitorController(_mockService.Object);
        }

        [Test]
        public async Task GetVisitorDetails_ReturnsListOfVisitors()
        {
            // Arrange
            _mockService.Setup(s => s.GetVisitorDetailsAsync()).ReturnsAsync(new List<Visitor>
            {
                new Visitor { Id = 1, Name = "John Doe" },
                new Visitor { Id = 2, Name = "Jane Smith" }
            });

            // Act
            var result = await _controller.GetVisitorDetails();

            // Assert
            if (result.Result is OkObjectResult okResult)
            {
                var visitors = okResult.Value as List<Visitor>;
                Assert.IsNotNull(visitors);
                Assert.AreEqual(2, visitors.Count);
            }
            else
            {
                Assert.Fail("Expected OkObjectResult");
            }
        }

        [Test]
        public async Task GetPersonInContact_ReturnsListOfContacts()
        {
            // Arrange
            _mockService.Setup(s => s.GetPersonInContactAsync()).ReturnsAsync(new List<string> { "John Doe", "Jane Smith" });

            // Act
            var result = await _controller.GetPersonInContact();

            // Assert
            if (result.Result is OkObjectResult okResult)
            {
                var contacts = okResult.Value as List<string>;
                Assert.IsNotNull(contacts);
                Assert.AreEqual(2, contacts.Count);
            }
            else
            {
                Assert.Fail("Expected OkObjectResult");
            }
        }

        [Test]
        public async Task GetVisitorById_ReturnsVisitor()
        {
            // Arrange
            var visitor = new Visitor { Id = 1, Name = "John Doe" };
            _mockService.Setup(s => s.GetVisitorByIdAsync(1)).ReturnsAsync(visitor);

            // Act
            var result = await _controller.GetVisitorById(1);

            // Assert
            if (result.Result is OkObjectResult okResult)
            {
                var returnedVisitor = okResult.Value as Visitor;
                Assert.IsNotNull(returnedVisitor);
                Assert.AreEqual(1, returnedVisitor.Id);
            }
            else
            {
                Assert.Fail("Expected OkObjectResult");
            }
        }

        [Test]
        public async Task GetVisitorById_ReturnsNotFound_ForInvalidId()
        {
            // Arrange
            _mockService.Setup(s => s.GetVisitorByIdAsync(1)).ReturnsAsync((Visitor)null);

            // Act
            var result = await _controller.GetVisitorById(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task CreateVisitor_ReturnsCreatedVisitor()
        {
            // Arrange
            var visitorDto = new VisitorCreationDTO { Name = "John Doe" };
            var createdVisitor = new Visitor { Id = 1, Name = "John Doe" };
            _mockService.Setup(s => s.CreateVisitorAsync(visitorDto)).ReturnsAsync(createdVisitor);

            // Act
            var result = await _controller.CreateVisitor(visitorDto);

            // Assert
            if (result.Result is CreatedAtActionResult createdResult)
            {
                var returnedVisitor = createdResult.Value as Visitor;
                Assert.IsNotNull(returnedVisitor);
                Assert.AreEqual(1, returnedVisitor.Id);
            }
            else
            {
                Assert.Fail("Expected CreatedAtActionResult");
            }
        }

        [Test]
        public async Task CreateVisitor_ReturnsBadRequest_ForInvalidData()
        {
            // Arrange
            var invalidDto = new VisitorCreationDTO(); // Assume this is invalid
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _controller.CreateVisitor(invalidDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task AddVisitorDevice_ReturnsCreatedDevice()
        {
            // Arrange
            var addDeviceDto = new AddVisitorDeviceDTO { DeviceId = 1, SerialNumber = "ABC123" };
            var addedDevice = new VisitorDevice { VisitorId = 1, DeviceId = 1, SerialNumber = "ABC123" };
            _mockService.Setup(s => s.AddVisitorDeviceAsync(addDeviceDto)).ReturnsAsync(addedDevice);

            // Act
            var result = await _controller.AddVisitorDevice(addDeviceDto);

            // Assert
            if (result.Result is CreatedAtActionResult createdResult)
            {
                var returnedDevice = createdResult.Value as VisitorDevice;
                Assert.IsNotNull(returnedDevice);
                Assert.AreEqual("ABC123", returnedDevice.SerialNumber);
            }
            else
            {
                Assert.Fail("Expected CreatedAtActionResult");
            }
        }

        [Test]
        public async Task AddVisitorDevice_ReturnsBadRequest_ForInvalidData()
        {
            // Arrange
            var invalidDeviceDto = new AddVisitorDeviceDTO(); // Assume this is invalid
            _controller.ModelState.AddModelError("DeviceId", "Required");

            // Act
            var result = await _controller.AddVisitorDevice(invalidDeviceDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task GetVisitorDetails_ReturnsEmptyList_WhenNoVisitors()
        {
            // Arrange
            _mockService.Setup(s => s.GetVisitorDetailsAsync()).ReturnsAsync(new List<Visitor>());

            // Act
            var result = await _controller.GetVisitorDetails();

            // Assert
            if (result.Result is OkObjectResult okResult)
            {
                var visitors = okResult.Value as List<Visitor>;
                Assert.IsNotNull(visitors);
                Assert.IsEmpty(visitors);
            }
            else
            {
                Assert.Fail("Expected OkObjectResult");
            }
        }

        [Test]
        public async Task GetPersonInContact_ReturnsEmptyList_WhenNoContacts()
        {
            // Arrange
            _mockService.Setup(s => s.GetPersonInContactAsync()).ReturnsAsync(new List<string>());

            // Act
            var result = await _controller.GetPersonInContact();

            // Assert
            if (result.Result is OkObjectResult okResult)
            {
                var contacts = okResult.Value as List<string>;
                Assert.IsNotNull(contacts);
                Assert.IsEmpty(contacts);
            }
            else
            {
                Assert.Fail("Expected OkObjectResult");
            }
        }
    }
}*/