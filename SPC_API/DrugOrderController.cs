using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SPC_Project.Data;
using SPC_Project.DTO;
using SPC_Project.Model;
using System.Collections.Generic;

namespace SPC_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugOrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DrugOrderRepo _repository;

        public DrugOrderController(DrugOrderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/drugorder
        [HttpGet]
        public ActionResult<IEnumerable<DTODrugOrderRead>> GetAllDrugOrders()
        {
            var drugOrderItems = _repository.GetAllDrugOrders();
            return Ok(_mapper.Map<IEnumerable<DTODrugOrderRead>>(drugOrderItems));
        }

        // GET api/drugorder/{id}
        [HttpGet("{id}", Name = "GetDrugOrderById")]
        public ActionResult<DTODrugOrderRead> GetDrugOrderById(int id)
        {
            var drugOrderItem = _repository.GetDrugOrderById(id);
            if (drugOrderItem == null)
            {
                return NotFound("Drug order not found.");
            }
            return Ok(_mapper.Map<DTODrugOrderRead>(drugOrderItem));
        }

        // POST api/drugorder
        [HttpPost]
        public ActionResult<DTODrugOrderRead> CreateDrugOrder(DTODrugOrderWrite dtoDrugOrder)
        {
            var drugOrderModel = _mapper.Map<DrugOrder>(dtoDrugOrder);

            // Try creating the order
            bool isCreated = _repository.CreateDrugOrder(drugOrderModel);
            if (!isCreated)
            {
                return BadRequest("Failed to create drug order.");
            }

            var drugOrderRead = _mapper.Map<DTODrugOrderRead>(drugOrderModel);
            return CreatedAtRoute(nameof(GetDrugOrderById), new { id = drugOrderRead.Id }, drugOrderRead);
        }

        // PUT api/drugorder/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, DTODrugOrderWrite dto)
        {
            var drugOrder = _mapper.Map<DrugOrder>(dto);
            drugOrder.Id = id;

            if (_repository.UpdateDrugOrder(drugOrder))
            {
                return Ok("Update successful.");
            }
            return NotFound("DrugOrder not found for update.");
        }

        // DELETE api/drugorder/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteDrugOrder(int id)
        {
            var drugOrderModelFromRepo = _repository.GetDrugOrderById(id);
            if (drugOrderModelFromRepo == null)
            {
                return NotFound("Drug order not found.");
            }

            if (!_repository.DeleteDrugOrder(drugOrderModelFromRepo))
            {
                return BadRequest("Failed to delete drug order.");
            }
            return NoContent();
        }
    }
}
