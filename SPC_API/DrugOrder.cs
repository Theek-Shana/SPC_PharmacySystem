using System.ComponentModel.DataAnnotations;

namespace SPC_Project.Model
{
    public class DrugOrder
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
      

    }
}
