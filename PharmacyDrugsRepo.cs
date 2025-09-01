using SPC_Project.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace SPC_Project.Data
{
    public class PharmacyDrugsRepo
    {
        private AppDBContext _dbContext;
        public PharmacyDrugsRepo(AppDBContext context)
        {
            _dbContext = context;
        }

        public bool Save()
        {
            int count = _dbContext.SaveChanges();
            return count > 0;
        }

        public void CreatePharmacyDrug(PharmacyDrugs pharmacyDrug)
        {
            // Ensure Id is not set manually
            pharmacyDrug.Id = 0; // or simply do not set it at all
            _dbContext.PharmacyDrugs.Add(pharmacyDrug);
            Save();
        }

        public bool UpdatePharmacyDrug(PharmacyDrugs pharmacyDrug)
        {
            if (pharmacyDrug == null)
            {
                return false;
            }

            // Check if the drug exists in the database
            var existingEntity = _dbContext.PharmacyDrugs.Find(pharmacyDrug.Id);
            if (existingEntity != null)
            {
                // Directly update the existing entity's properties
                existingEntity.Name = pharmacyDrug.Name;
                existingEntity.Description = pharmacyDrug.Description;
                existingEntity.Quantity = pharmacyDrug.Quantity;
                existingEntity.Price = pharmacyDrug.Price;

                // Save the changes
                _dbContext.Update(existingEntity);
                return Save();
            }

            return false;
        }



        public bool DeletePharmacyDrug(PharmacyDrugs pharmacyDrug)
        {
            if (pharmacyDrug != null)
            {
                _dbContext.PharmacyDrugs.Remove(pharmacyDrug);
                return Save();
            }
            return false;
        }

        public PharmacyDrugs GetPharmacyDrugByID(int id)
        {
            return _dbContext.PharmacyDrugs.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<PharmacyDrugs> GetAllPharmacyDrugs()
        {
            return _dbContext.PharmacyDrugs.AsNoTracking().ToList();
        }

    }
}
