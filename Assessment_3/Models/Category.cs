namespace Assessment_3.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryQuantity { get; set; }
    }
}
