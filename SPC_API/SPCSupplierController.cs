using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SPC_Project.Data;
using SPC_Project.DTO;
using SPC_Project.Model;

namespace SPC_Project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]


    public class SPCSupplierController : Controller
    {
        private IMapper mapper;
        private SPCSupplierRepo repo;
        public SPCSupplierController(IMapper _mapper, SPCSupplierRepo _repo)
        {
            mapper = _mapper;
            repo = _repo;
        }
        [HttpPost]
        public ActionResult CreateSupplier(DTOSPCSupplierWrite dTO)
        {
        
            var model = mapper.Map<SPCSupplier>(dTO);
            if (repo.CreateSupplier(model))
                return Ok();
            else
                return BadRequest();

        }
        [HttpGet]
        public ActionResult<IEnumerable<DTOSPCSuplierRead>> GetSupplier()
        {
            var Suppliers = repo.GetSupplier();
            return Ok(mapper.Map<IEnumerable<DTOSPCSuplierRead>>(Suppliers));
        }
        [HttpGet("{id}", Name = "GetSupplierByID")]
        public ActionResult<DTOSPCSuplierRead> GetSupplierByID(int id)
        {
            var SPCSupplier = repo.GetSupplierByID(id);
            if (SPCSupplier != null)
                return Ok(mapper.Map<DTOSPCSuplierRead>(SPCSupplier));
            else
                return NotFound();

        }

        [HttpPut("{id}")]
        public ActionResult UpdateSupplier(int id, DTOSPCSupplierWrite dTO)
        {
            var SPCSuppliers = mapper.Map<SPCSupplier>(dTO);
            SPCSuppliers.SupplierId = id;
            if (repo.UpdateSupplier(SPCSuppliers))
                return Ok();
            else
                return NotFound();
        }



        [HttpDelete("{id}")]
        public ActionResult DeleteSupplier(int id)
        {
            var SPCSuppliers = repo.GetSupplierByID(id);
            if (SPCSuppliers != null)
            {
                repo.DeleteSupplier(SPCSuppliers);
                return Ok();
            }
            else
                return NotFound();


        }

    }

}