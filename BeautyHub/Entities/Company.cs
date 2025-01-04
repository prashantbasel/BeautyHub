using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautyHub.Entities
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Company Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Company Phone")]
        [RegularExpression(@"(?:\+977[- ])?\d{2}-?\d{7,8}", ErrorMessage = "Please Enter a valid phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Company Logo")]
        public byte[] Logo { get; set; }

        public List<SocialLink> SocialLink { get; set; }
    }
}