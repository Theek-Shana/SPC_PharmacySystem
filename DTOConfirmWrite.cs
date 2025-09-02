using System.ComponentModel.DataAnnotations;

namespace SPC_Project.DTO
{
    public class DTOConfirmWrite
    {

        [Key]
        public int Id { get; set; }
        public bool IsConfirmed { get; set; }

    }
}
