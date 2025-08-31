using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SPC_Project.Data;
using SPC_Project.DTO;
using SPC_Project.Model;

namespace SPC_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SPCTenderRequestController : ControllerBase
    {
        private IMapper mapper;
        private SPCTenderRequestRepo repo;

        public SPCTenderRequestController(IMapper _mapper, SPCTenderRequestRepo _repo)
        {
            mapper = _mapper;
            repo = _repo;
        }

        // POST: api/SPCTenderRequest
        [HttpPost]
        public ActionResult CreateTenderRequest(DTOSPCTenderRequestWrite dTO)
        {
            var model = mapper.Map<TenderRequest>(dTO);
            if (repo.CreateTenderRequest(model))
                return Ok();
            else
                return BadRequest();
        }

        // GET: api/SPCTenderRequest
        [HttpGet]
        public ActionResult<IEnumerable<DTOSPCTenderRequestRead>> GetTenderRequests()
        {
            var tenderRequests = repo.GetTenderRequests();
            return Ok(mapper.Map<IEnumerable<DTOSPCTenderRequestRead>>(tenderRequests));
        }

        // GET: api/SPCTenderRequest/{id}
        // Renamed route to ensure it is unique
        [HttpGet("GetTenderDetails/{id}", Name = "GetTenderRequestByID")]
        public ActionResult<DTOSPCTenderRequestRead> GetTenderRequestByID(int id)
        {
            var tenderRequest = repo.GetTenderRequestByID(id);
            if (tenderRequest != null)
                return Ok(mapper.Map<DTOSPCTenderRequestRead>(tenderRequest));

            else
            {
                return NotFound();
            }
        }

        // PUT: api/SPCTenderRequest/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateTenderRequest(int id, DTOSPCTenderRequestWrite dTO)
        {
            var tenderRequest = mapper.Map<TenderRequest>(dTO);
            tenderRequest.TenderID = id;
            if (repo.UpdateTenderRequest(tenderRequest))
                return Ok();
            else
                return NotFound();
        }

        // DELETE: api/SPCTenderRequest/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTenderRequest(int id)
        {
            var tenderRequest = repo.GetTenderRequestByID(id);
            if (tenderRequest != null)
            {
                repo.DeleteTenderRequest(tenderRequest);
                return Ok();
            }
            else
                return NotFound();
        }
    }
}
