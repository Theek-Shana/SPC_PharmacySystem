using System.ComponentModel.DataAnnotations;
namespace SPC_Project.DTO
{
    public class DTOStatusWrite
    {
        public int StatusId { get; set; }

        public int TenderID { get; set; }


        public bool IsAccept { get; set; }
    }
}
