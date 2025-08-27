using System.ComponentModel.DataAnnotations;

namespace SPC_Project.Model
{
    public class ConfirmTender
    {
        [Key]
        public int Id { get; set; }
        public bool IsConfirmed { get; set; }

    }
}
