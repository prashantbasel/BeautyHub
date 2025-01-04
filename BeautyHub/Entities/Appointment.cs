using BeautyHub.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeautyHub.Entities
{
    public class Appointment
    {
        
        public int Id { get; set; }

        [MinLength(9, ErrorMessage = "Must be of 9 characters")]
        [MaxLength(9, ErrorMessage = "Must be of 9 characters")]
        public string Token { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name ="Phone Number")]
        [Required]
        [RegularExpression(@"(?:\+977[- ])?\d{2}-?\d{7,8}", ErrorMessage = "Please Enter a valid phone Number")]
        public string PhoneNumber { get; set; }

        public string Message { get; set; }
        public bool IsServed { get; set; }
        public bool IsActive { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Service Service { get; set; }
        public int ServiceId { get; set; }


        [Column(TypeName = "date")]
        [Display(Name = "Date")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Time")]
        public ServiceTime ServiceTime { get; set; }
        public int ServiceTimeId { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Location")]
        public OfficeLocation OfficeLocation { get; set; }
        public int OfficeLocationId { get; set; }

        public Appointment()
        {
            this.IsServed = false;
            this.IsActive = true;
        }
    }

}