using System.ComponentModel.DataAnnotations;

namespace SPC_Project.Model
{
    public class TenderReplys
    {
        [Key]
        public int TenderReplyId { get; set; }
        public string Brand { get; set; }

        public string Description { get; set; }


        public string Price { get; set; }
        public string DiscountedPrice { get; set; }

    }
}

             