using Autobarn.Data;
using Autobarn.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Autobarn.Website.Models;

namespace Autobarn.Website.Controllers.api {
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase {
        private readonly IAutobarnDatabase db;

        public ModelsController(IAutobarnDatabase db) {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get() {
            var result = new {
                items = db.ListModels().Select(model => model.ToResource())
            };
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id) {
            var vehicleModel = db.FindModel(id);
            if (vehicleModel == default) return NotFound();
            var result = vehicleModel.ToResource();
            result._actions = new
            {
                create = new
                {
                    method = "POST",
                    href = $"/api/models/{id}",
                    name = $"Create a new {vehicleModel.Manufacturer.Name} {vehicleModel.Name}",
                    type = "application/json"
                }
            };
            return Ok(result);
        }

        // POST api/models/{modelCode}
        [HttpPost("{id}")]
        public IActionResult Post(string id, [FromBody] VehicleDto dto) {
            var vehicleModel = db.FindModel(id);
            var vehicle = new Vehicle {
                Registration = dto.Registration,
                Color = dto.Color,
                Year = dto.Year,
                VehicleModel = vehicleModel
            };
            db.CreateVehicle(vehicle);
            return Created($"/api/vehicles/{dto.Registration}", vehicle);
        }
    }
}