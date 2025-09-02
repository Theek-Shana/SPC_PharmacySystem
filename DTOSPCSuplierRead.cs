using System.ComponentModel.DataAnnotations;

namespace SPC_Project.DTO
{
    public class DTOSPCSuplierRead
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhoneNumber { get; set; }
        public string SupplierEmail { get; set; }


    }
}
