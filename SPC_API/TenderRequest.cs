using System.ComponentModel.DataAnnotations;

namespace SPC_Project.Model
{
    public class TenderRequest
    {
        [Key]
        public int TenderID { get; set; }

        public string TenderDescription { get; set; }

 
        public string TenderValue { get; set; }
        public string TenderDate { get; set; }


        // Navigation Property (One-to-Many Relationship)
        public virtual ICollection<Status> Statuses { get; set; }
    }
}
