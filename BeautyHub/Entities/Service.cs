using System.ComponentModel.DataAnnotations;

namespace BeautyHub.Entities
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Service Name")]
        public string Name { get; set; }


        [Required]
        [Range(0,999999)]
        public double Price { get; set; }


        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [Required]
        public byte[] Image { get; set; }

        public bool IsActive { get; set; }



        public Category Category { get; set; }
        public int CategoryId { get; set; }


        public Service()
        {
            this.IsActive = true;
        }
    }
}