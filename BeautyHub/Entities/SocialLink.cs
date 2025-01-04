using System.ComponentModel.DataAnnotations;

namespace BeautyHub.Entities
{
    public class SocialLink
    {
        public int Id { get; set; }
        [Required]
        public string LinkName { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public byte[] Icon { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}