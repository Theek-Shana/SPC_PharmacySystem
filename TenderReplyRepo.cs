using SPC_Project.Model;
using System.Collections;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SPC_Project.Data
{
    public class SPCTenderReplyRepo
    {
        private AppDBContext _dbContext;

        public SPCTenderReplyRepo(AppDBContext context)
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
            catch (DbUpdateException ex)
            {
                // Log the exception or inspect the inner exception
                Console.WriteLine($"Error saving changes: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return false;
            }
        }

        // Add a new tender reply
        public bool CreateTenderReply(TenderReplys tenderReplys)
        {
            if (tenderReplys != null)
            {
                _dbContext.tenderReplys.Add(tenderReplys);
                return Save();
            }
            return false;
        }

        // Update an existing tender request
        public bool UpdateTenderReply(TenderReplys tenderReplys)
        {
            if (tenderReplys != null)
            {
                _dbContext.tenderReplys.Update(tenderReplys);
                return Save();
            }
            return false;
        }


        // Get a tender request by its ID
        public TenderReplys CreateTenderReplysByID(int id)
        {
            return _dbContext.tenderReplys.FirstOrDefault(t => t.TenderReplyId == id);
        }

        // Get all tender replys
        public IEnumerable<TenderReplys> GetTenderReplys()
        {
            return _dbContext.tenderReplys.ToList();
        }
        // Delete a tender request
        public bool DeleteTenderReplys(TenderReplys tenderReplys)
        {
            if (tenderReplys != null)
            {
                _dbContext.tenderReplys.Remove(tenderReplys);
                return Save();
            }
            return false;
        }
    }
}