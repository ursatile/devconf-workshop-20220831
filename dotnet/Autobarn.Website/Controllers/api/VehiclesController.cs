using System;
using Autobarn.Data;
using Autobarn.Data.Entities;
using Autobarn.Website.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Autobarn.Website.Controllers.api {
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase {
        private readonly IAutobarnDatabase db;

        public VehiclesController(IAutobarnDatabase db) {
            this.db = db;
        }

        private const int PAGE_SIZE = 10;

        // GET: api/vehicles
        [HttpGet]
        public IActionResult Get(int index = 0, int count = PAGE_SIZE) {
            dynamic _links = new ExpandoObject();
            var total = db.CountVehicles();
            _links.self = new {
                href = "/api/vehicles"
            };
            if (index > 0) {
                _links.previous = new {
                    href = $"/api/vehicles?index={index - count}&count={count}"
                };
            }

            if (index < total) {
                _links.next = new {
                    href = $"/api/vehicles?index={index + count}&count={count}"
                };
            }

            var result = new {
                _links,
                items = db.ListVehicles().Skip(index).Take(PAGE_SIZE)

            };
            return Ok(result);
        }

        // GET api/vehicles/ABC123
        [HttpGet("{id}")]
        public IActionResult Get(string id) {
            var vehicle = db.FindVehicle(id);
            if (vehicle == default) return NotFound();
            return Ok(vehicle);
        }

        // PUT api/vehicles/ABC123
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] VehicleDto dto) {
            var vehicleModel = db.FindModel(dto.ModelCode);
            var vehicle = new Vehicle {
                Registration = dto.Registration,
                Color = dto.Color,
                Year = dto.Year,
                ModelCode = vehicleModel.Code
            };
            db.UpdateVehicle(vehicle);
            return Ok(dto);
        }

        // DELETE api/vehicles/ABC123
        [HttpDelete("{id}")]
        public IActionResult Delete(string id) {
            var vehicle = db.FindVehicle(id);
            if (vehicle == default) return NotFound();
            db.DeleteVehicle(vehicle);
            return NoContent();
        }
    }
}
