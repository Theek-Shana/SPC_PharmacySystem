using System.ComponentModel.DataAnnotations;
namespace SPC_Project.DTO
{
    public class DTODrugOrderWrite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

    }
}
