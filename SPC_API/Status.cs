using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPC_Project.Model
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        // Foreign Key
        [ForeignKey("TenderRequest")]
        public int TenderID { get; set; }

        // Navigation Property
        public virtual TenderRequest TenderRequest { get; set; }

        public bool IsAccept { get; set; }
    }
}
