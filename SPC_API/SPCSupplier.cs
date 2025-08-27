using System.ComponentModel.DataAnnotations;
namespace SPC_Project.Model
{
    public class SPCSupplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhoneNumber { get; set; }

        [Required]
        public string SupplierEmail { get; set; }
    }
}
