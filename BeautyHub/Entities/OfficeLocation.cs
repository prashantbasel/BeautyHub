using System.ComponentModel.DataAnnotations;

namespace BeautyHub.Entities
{
    public class OfficeLocation
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"(?:\+977[- ])?\d{2}-?\d{7,8}", ErrorMessage = "Please Enter a valid phone Number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string MapLink { get; set; }
    }
}