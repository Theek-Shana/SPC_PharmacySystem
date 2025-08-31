using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPC_Project.Data;
using SPC_Project.DTO;
using SPC_Project.Model;

namespace SPC_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TenderReplyController : ControllerBase
    {
        private IMapper mapper;
        private SPCTenderReplyRepo repo;
        public TenderReplyController(IMapper _mapper, SPCTenderReplyRepo _repo)
        {
            mapper = _mapper;
            repo = _repo;
        }
        [HttpPost]
        public IActionResult CreateTenderReply([FromBody] DTOTenderReplyWrite dto)
        {
            if (ModelState.IsValid)
            {
                var tenderReply = new TenderReplys
                {
                    // Map properties from DTO to entity
                    Brand = dto.Brand,
                    Description = dto.Description,
                    Price = dto.Price,
                    DiscountedPrice = dto.DiscountedPrice
                };

                bool isCreated = repo.CreateTenderReply(tenderReply); // Fixed reference here
                if (isCreated)
                {
                    // Returning 201 Created with the location of the created resource
                    return CreatedAtRoute("Get Tender Details ID", new { id = tenderReply.TenderReplyId }, tenderReply);
                }
                else
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        public ActionResult<IEnumerable<DTOTenderReplyRead>> GetTenderReplys()
        {
            var TenderReplys = repo.GetTenderReplys();
            return Ok(mapper.Map<IEnumerable<DTOTenderReplyRead>>(TenderReplys));
        }
        [HttpGet("{id}", Name = "Get Tender Details ID")]
        public ActionResult<DTOTenderReplyRead> CreateTenderReplyByID(int id)
        {
            var TenderReply = repo.CreateTenderReplysByID(id);
            if (TenderReply != null)
                return Ok(mapper.Map<DTOTenderReplyRead>(TenderReply));

            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTenderReply(int id, DTOTenderReplyWrite dTO)
        {
            var TenderReply = mapper.Map<TenderReplys>(dTO);
            TenderReply.TenderReplyId = id;
            if (repo.UpdateTenderReply(TenderReply))
                return Ok();
            else
                return NotFound();
        }



        [HttpDelete("{id}")]
        public ActionResult DeleteTenderReply(int id)
        {
            var TenderRequest = repo.CreateTenderReplysByID(id);
            if (TenderRequest != null)
            {
                repo.DeleteTenderReplys(TenderRequest);
                return Ok();
            }
            else
                return NotFound();


        }

    }
}

