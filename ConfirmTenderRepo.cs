using SPC_Project.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SPC_Project.Data
{
    public class ConfirmTenderRepo
    {
        private readonly AppDBContext _dbContext;

        public ConfirmTenderRepo(AppDBContext context)
        {
            _dbContext = context;
        }

        // Save changes to the database
        public bool Save() 
        {
            return _dbContext.SaveChanges() > 0;
        } 

        // Confirm a tender reply
        public bool ConfirmTender(TenderReplys tenderReply)
        {
            if (tenderReply == null)
                return false; 

            // Create a ConfirmTender entry to track confirmation
            var confirmTender = new ConfirmTender
            {
                IsConfirmed = true
            };

            _dbContext.confirmTenders.Add(confirmTender); // Add confirmation entry
            return Save();
        }

        // Get a tender reply by ID
        public TenderReplys GetTenderReplyByID(int id)
        {
            return _dbContext.tenderReplys.FirstOrDefault(t => t.TenderReplyId == id);
        }

        // Get all confirmed tenders
        public IEnumerable<ConfirmTender> GetConfirmedTenders()
        {
            return _dbContext.confirmTenders.AsNoTracking().ToList();
        }
    }
}



