using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautyHub.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Category")]
        public string Name { get; set; }

        [Required]
        public byte[] Image { get; set; }
        public bool IsActive { get; set; }

        public List<Service> Services { get; set; }
        public Category()
        {
            this.IsActive = true;
        }
    }
}