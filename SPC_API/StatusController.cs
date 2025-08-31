using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SPC_Project.Data;
using SPC_Project.DTO;
using SPC_Project.Model;
using System.Collections.Generic;

namespace SPC_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly StatusRepo _repo;

        public StatusController(IMapper mapper, StatusRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpPost]
        public ActionResult CreateStatus(DTOStatusWrite dto)
        {
            var status = _mapper.Map<Status>(dto);
            if (_repo.CreateStatus(status))
                return Ok();
            else
                return BadRequest("Failed to create status.");
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTOStatusRead>> GetStatuse()
        {
            var statuses = _repo.GetAllStatus();
            return Ok(_mapper.Map<IEnumerable<DTOStatusRead>>(statuses));
        }

        [HttpGet("{id}", Name = "GetStatusByID")]
        public ActionResult<DTOStatusRead> GetStatusByID(int id)
        {
            var status = _repo.GetStatusByID(id);
            if (status != null)
                return Ok(_mapper.Map<DTOStatusRead>(status));
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStatus(int id, DTOStatusWrite dto)
        {
            var status = _mapper.Map<Status>(dto);
            status.StatusId = id;  // Ensure the ID is set for updating
            if (_repo.UpdateStatus(status))
                return Ok();
            else
                return BadRequest("Failed to update status.");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStatus(int id)
        {
            if (_repo.DeleteStatus(id))
                return Ok();
            else
                return BadRequest("Failed to delete status.");
        }
    }
}
