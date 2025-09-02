using System.ComponentModel.DataAnnotations;
namespace SPC_Project.DTO
{
    public class DTOTenderReplyRead
    {

        public int TenderReplyId { get; set; }
        public string Brand { get; set; }

        public string Description { get; set; }


        public string Price { get; set; }
        public string DiscountedPrice { get; set; }

    }
}
