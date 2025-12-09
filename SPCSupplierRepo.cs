 ﻿using SPC_Project.Model;
using System.Collections;
namespace SPC_Project.Data
{
    public class SPCSupplierRepo
    {

        private AppDBContext _dbContext;
        public SPCSupplierRepo(AppDBContext context)
        {
            _dbContext = context;
        }
        public bool Save()
        {
            int count = _dbContext.SaveChanges();
            if (count > 0)
                return true;
            else
                return false;
        }

        //add
        public bool CreateSupplier(SPCSupplier SPCSupplier)
        {
            if (SPCSupplier != null)
            {
                _dbContext.SPCSupplier.Add(SPCSupplier);
                return Save();
            }
            else
                return false;

        }

        //update
        public bool UpdateSupplier(SPCSupplier SPCSupplier)
        {
            if (SPCSupplier != null)
            {
                _dbContext.SPCSupplier.Update(SPCSupplier);
                return Save();
            }
            else
                return false;

        }

        //Delete
        public bool DeleteSupplier(SPCSupplier product)
        {
            if (product != null)
            {
                _dbContext.SPCSupplier.Remove(product);
                return Save();
            }
            else
                return false;

        }

        public SPCSupplier GetSupplierByID(int id)
        {
            return _dbContext.SPCSupplier.FirstOrDefault(s =>s.SupplierId == id);

        }
        public IEnumerable<SPCSupplier> GetSupplier()
        {
            return _dbContext.SPCSupplier.ToList();
        }
    }
}

