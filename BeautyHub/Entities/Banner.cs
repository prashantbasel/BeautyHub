using System.ComponentModel.DataAnnotations;

namespace BeautyHub.Entities
{
    public class Banner
    {
        public int Id { get; set; }

        [Required]
        public byte[] Image { get; set; }
        public string Link { get; set; }
    }
}