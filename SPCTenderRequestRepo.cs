using SPC_Project.Model;
using System.Collections;
namespace SPC_Project.Data
{


    public class SPCTenderRequestRepo
    {
        private AppDBContext _dbContext;

        public SPCTenderRequestRepo(AppDBContext context)
        {
            _dbContext = context;
        }

        // Save changes to the database
        public bool Save()
        {
            int count = _dbContext.SaveChanges();
            return count > 0;
        }

        // Add a new tender request
        public bool CreateTenderRequest(TenderRequest tenderRequest)
        {
            if (tenderRequest != null)
            {
                _dbContext.TenderRequest.Add(tenderRequest);
                return Save();
            }
            return false;
        }

        // Update an existing tender request
        public bool UpdateTenderRequest(TenderRequest tenderRequest)
        {
            if (tenderRequest != null)
            {
                _dbContext.TenderRequest.Update(tenderRequest);
                return Save();
            }
            return false;
        }

        // Delete a tender request
        public bool DeleteTenderRequest(TenderRequest tenderRequest)
        {
            if (tenderRequest != null)
            {
                _dbContext.TenderRequest.Remove(tenderRequest);
                return Save();
            }
            return false;
        }

        // Get a tender request by its ID
        public TenderRequest GetTenderRequestByID(int id)
        {
            return _dbContext.TenderRequest.FirstOrDefault(t => t.TenderID == id);
        }

        // Get all tender requests
        public IEnumerable<TenderRequest> GetTenderRequests()
        {
            return _dbContext.TenderRequest.ToList();
        }

        internal object CreateTenderRequest()
        {
            throw new NotImplementedException();
        }
    }
}
