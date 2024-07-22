﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VMS.Models;
using VMS.Models.DTO;
using VMS.Data;
using Microsoft.AspNetCore.Authorization;

namespace VMS.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class VisitorController : ControllerBase
    {
        private VisitorManagementDbContext _context;
        public VisitorController(VisitorManagementDbContext _context)
        {
            this._context = _context;

        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet]
        public IEnumerable<Visitor> GetVisitorDetails()
        {
            return _context.Visitors.ToList<Visitor>();

        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet]
        public IEnumerable<string> GetPersonInContact()
        {
            return _context.Visitors.Select(v => v.HostName).Distinct().ToList();
        }
        [HttpPost("create-and-add-item")]
        public IActionResult CreateVisitorAndAddItem([FromBody] VisitorCreationDTO visitorDto)
        {
            if (visitorDto == null)
            {
                return BadRequest("Visitor data is null.");
            }

            // Log the received DTO
            Console.WriteLine("Received DTO: " + visitorDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var visitor = new Visitor
            {
                VisitorName = visitorDto.Name,
                Phone = visitorDto.PhoneNumber,
                PurposeId = visitorDto.PurposeOfVisitId,
                HostName = visitorDto.PersonInContact,
                OfficeLocationId = visitorDto.OfficeLocationId,
                CreatedBy = 1,
                VisitDate = DateTime.Now.Date,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                VisitorDevices = new List<VisitorDevice>()
            };

            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            if (visitorDto.SelectedDevice != null && visitorDto.SelectedDevice.Count > 0)
            {
                foreach (var selectedDevice in visitorDto.SelectedDevice)
                {
                    var addDeviceDto = new AddVisitorDeviceDto
                    {
                        VisitorId = visitor.VisitorId,
                        DeviceId = selectedDevice.DeviceId,
                        SerialNumber = selectedDevice.SerialNumber
                    };

                    var addedDevice = AddVisitorDevice(addDeviceDto);
                }

                return Ok(new { CreatedVisitor = visitor, AddedItems = visitorDto.SelectedDevice });
            }
            else {

                return Ok(new { CreatedVisitor = visitor });
            }

            
        }

        // Method to call the AddVisitorDevice API
        private VisitorDevice AddVisitorDevice(AddVisitorDeviceDto addDeviceDto)
        {
            // Create VisitorDevice entity from AddVisitorDeviceDto
            var visitorDevice = new VisitorDevice
            {
                VisitorId = addDeviceDto.VisitorId,
                DeviceId = addDeviceDto.DeviceId,
                SerialNumber = addDeviceDto.SerialNumber
            };

            // Add VisitorDevice to context
            _context.VisitorDevices.Add(visitorDevice);

            // Save changes to the VisitorDevices
            _context.SaveChanges();

            return visitorDevice;
        }

        /*[Authorize(Policy = "AdminOnly")]*/
        [HttpGet("{id}")]
        public IActionResult GetVisitorById(int id)
        {
            var visitor = _context.Visitors.FirstOrDefault(v => v.VisitorId == id);

            if (visitor == null)
            {
                return NotFound();
            }

            return Ok(visitor);
        }
    }
}

