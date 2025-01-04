using System;
using System.ComponentModel.DataAnnotations;

namespace BeautyHub.Entities
{
    public class ServiceTime
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Service Time")]
        public string TimeRange { get; set; }
    }
}