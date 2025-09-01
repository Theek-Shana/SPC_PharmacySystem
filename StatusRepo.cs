using SPC_Project.Model;
using System.Collections.Generic;
using System.Linq;

namespace SPC_Project.Data
{
    public class StatusRepo
    {
        private readonly AppDBContext _dbContext;

        public StatusRepo(AppDBContext context)
        {
            _dbContext = context;
        }

        public bool Save()
        {
            try
            {
                int count = _dbContext.SaveChanges();
                return count > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateStatus(Status status)
        {
            if (status != null)
            {
                _dbContext.Status.Add(status);
                return Save();
            }
            return false;
        }

        public bool UpdateStatus(Status status)
        {
            if (status != null)
            {
                _dbContext.Status.Update(status);
                return Save();
            }
            return false;
        }

        public bool DeleteStatus(int id)
        {
            var status = GetStatusByID(id);
            if (status != null)
            {
                _dbContext.Status.Remove(status);
                return Save();
            }
            return false;
        }

        public Status GetStatusByID(int id)
        {
            return _dbContext.Status.FirstOrDefault(s => s.StatusId == id);
        }

        public IEnumerable<Status> GetAllStatus()
        {
            return _dbContext.Status.ToList();
        }
    }
}
