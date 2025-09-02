using System.ComponentModel.DataAnnotations;
namespace SPC_Project.DTO
{
    public class DTOPharmacyDrugsRead
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
