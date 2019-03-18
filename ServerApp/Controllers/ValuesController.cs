using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Repositories;


namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IRepository _repository;

        public ValuesController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Ship> GetAll()
        {
            var listShips = _repository.ReadFile();

            return listShips;
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var ship = _repository.GetShip(name);
            if (ship == null)
                return NotFound();
            return new ObjectResult(ship);
            //return Ok(ship);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]ShipUpload shipUpload)
        {
            if (shipUpload != null)
            {
                if (_repository.GetShip(shipUpload.Name) == null)
                {
                    var ship = _repository.Create(shipUpload.Name, shipUpload.ImgByte);
                    return Ok(ship);
                }
                return BadRequest();
            }
            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{name}")]
        public IActionResult Put(string name, [FromBody] byte[] image)
        {
            if (name != null)
            {
                if (_repository.GetShip(name) != null)
                {
                    _repository.Update(name, image);
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }

        // DELETE api/values/5
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            if (name != null)
            {
                if (_repository.GetShip(name) != null)
                {
                    _repository.Delete(name);
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
