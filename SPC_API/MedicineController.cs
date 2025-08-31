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
    public class MedicineController : ControllerBase
    {
        private IMapper _mapper;
        private MedicineRepo _repo;

        public MedicineController(IMapper mapper, MedicineRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        
        [HttpPost]
        public ActionResult CreateMedicine(DTOMedicineWrite dto)
        {
            var model = _mapper.Map<Medicine>(dto);
            if (_repo.CreateMedicine(model))
                return Ok();
            else
                return BadRequest("Failed to create medicine.");
        }

       
        [HttpGet]
        public ActionResult<IEnumerable<DTOMedicineRead>> GetMedicines()
        {
            var medicines = _repo.GetMedicines();
            return Ok(_mapper.Map<IEnumerable<DTOMedicineRead>>(medicines));
        }

   
        [HttpGet("{id}", Name = "GetMedicineByID")]
        public ActionResult<DTOMedicineRead> GetMedicineByID(int id)
        {
            var medicine = _repo.GetMedicineByID(id);
            if (medicine != null)
                return Ok(_mapper.Map<DTOMedicineRead>(medicine));
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMedicine(int id, DTOMedicineWrite dto)
        {
            var medicine = _mapper.Map<Medicine>(dto);
            medicine.Id = id;  // Ensure the ID is set for updating
            if (_repo.UpdateMedicine(medicine))
                return Ok();
            else
                return NotFound("Medicine not found for update.");
        }

        
        [HttpDelete("{id}")]
        public ActionResult DeleteMedicine(int id)
        {
            var medicine = _repo.GetMedicineByID(id);
            if (medicine != null)
            {
                _repo.DeleteMedicine(medicine);
                return Ok();
            }
            else
                return NotFound("Medicine not found for deletion.");
        }
    }
}
