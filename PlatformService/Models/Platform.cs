using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace PlatformService.Models
{
    public class Platform : IModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Cost { get; set; }
    }
}