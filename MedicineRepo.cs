using SPC_Project.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SPC_Project.Data
{
    public class MedicineRepo
    {
        private readonly AppDBContext _dbContext;

        public MedicineRepo(AppDBContext context)
        {
            _dbContext = context;
        }

        // Save changes to the database
        public bool Save()
        {
            try
            {
                int count = _dbContext.SaveChanges();
                return count > 0;
            }
            catch
            {
                return false; // Return false in case of an exception
            }
        }

        // Create a new medicine
        public bool CreateMedicine(Medicine medicine)
        {
            if (medicine == null)
            {
                return false; // Ensure that the medicine object is not null
            }

            try
            {
                _dbContext.Medicine.Add(medicine);  // Add the new medicine to the context
                return Save();
            }
            catch
            {
                return false; // Handle any exceptions and return false
            }
        }

        // Update an existing medicine
        public bool UpdateMedicine(Medicine medicine)
        {
            if (medicine == null)
            {
                return false; // Ensure that the medicine object is not null
            }

            try
            {
                _dbContext.Medicine.Update(medicine);  // Update the existing medicine in the context
                return Save();
            }
            catch
            {
                return false; // Handle any exceptions and return false
            }
        }

        // Delete a medicine
        public bool DeleteMedicine(Medicine medicine)
        {
            if (medicine == null)
            {
                return false; // Ensure that the medicine object is not null
            }

            try
            {
                _dbContext.Medicine.Remove(medicine);  // Remove the medicine from the context
                return Save();
            }
            catch
            {
                return false; // Handle any exceptions and return false
            }
        }

        // Get a medicine by its ID
        public Medicine GetMedicineByID(int id)
        {
            return _dbContext.Medicine.FirstOrDefault(m => m.Id == id);  // Find medicine by ID
        }

        // Get all medicines
        public IEnumerable<Medicine> GetMedicines()
        {
            return _dbContext.Medicine.AsNoTracking().ToList();  // Return all medicines from the database, using AsNoTracking for better performance in read-only queries
        }

        // Additional methods can be added here as needed (e.g., searching, filtering, etc.)
    }
}
