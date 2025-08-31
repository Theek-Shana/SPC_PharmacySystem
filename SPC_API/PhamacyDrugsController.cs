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
    public class PharmacyDrugsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PharmacyDrugsRepo _repo;

        public PharmacyDrugsController(IMapper mapper, PharmacyDrugsRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTOPharmacyDrugsRead>> GetAllPharmacyDrugs()
        {
            var pharmacyDrugs = _repo.GetAllPharmacyDrugs();
            return Ok(_mapper.Map<IEnumerable<DTOPharmacyDrugsRead>>(pharmacyDrugs));
        }

        [HttpGet("{id}", Name = "GetPharmacyDrugByID")]
        public ActionResult<DTOPharmacyDrugsRead> GetPharmacyDrugByID(int id)
        {
            var drug = _repo.GetPharmacyDrugByID(id);
            if (drug != null)
                return Ok(_mapper.Map<DTOPharmacyDrugsRead>(drug));
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult CreatePharmacyDrug(DTOPharmacyDrugsWrite dto)
        {
            var model = _mapper.Map<PharmacyDrugs>(dto);
            _repo.CreatePharmacyDrug(model);
            if (_repo.Save())
                return Ok();
            else
                return BadRequest("Failed to create pharmacy drug.");
        }
        
        
        
        [HttpPut("{id}")]
        public ActionResult UpdatePharmacyDrug(int id, DTOPharmacyDrugsWrite dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }

            var existingDrug = _repo.GetPharmacyDrugByID(id);
            if (existingDrug == null)
            {
                return NotFound("Pharmacy drug not found for update.");
            }

            _mapper.Map(dto, existingDrug); // Map DTO to existing drug

            if (_repo.UpdatePharmacyDrug(existingDrug))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to update pharmacy drug.");
            }
        }




        [HttpDelete("{id}")]
        public ActionResult DeletePharmacyDrug(int id)
        {
            var drug = _repo.GetPharmacyDrugByID(id);
            if (drug != null)
            {
                _repo.DeletePharmacyDrug(drug);
                return Ok();
            }
            else
                return NotFound("Pharmacy drug not found for deletion.");
        }
    }
}
