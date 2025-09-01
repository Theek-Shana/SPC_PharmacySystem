using SPC_Project.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SPC_Project.Data
{
    public class DrugOrderRepo
    {
        private readonly AppDBContext _dbContext;

        public DrugOrderRepo(AppDBContext context)
        {
            _dbContext = context;
        }

        private bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public bool CreateDrugOrder(DrugOrder drugOrder)
        {
            if (drugOrder == null) return false;

            drugOrder.Id = 0; // Ensure no ID conflicts
            _dbContext.DrugOrder.Add(drugOrder);
            return Save();
        }

        public bool UpdateDrugOrder(DrugOrder drugOrder)
        {
            if (drugOrder == null) return false;

            _dbContext.DrugOrder.Update(drugOrder);
            return Save();
        }

        public bool DeleteDrugOrder(DrugOrder drugOrder)
        {
            if (drugOrder == null) return false;

            _dbContext.DrugOrder.Remove(drugOrder);
            return Save();
        }

        public DrugOrder GetDrugOrderById(int id)
        {
            return _dbContext.DrugOrder.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<DrugOrder> GetAllDrugOrders()
        {
            return _dbContext.DrugOrder.AsNoTracking().ToList();
        }
    }
}
