using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace CommandService.Models
{
    public class Command : IModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string HowTo { get; set; }

        [Required]
        public string CommandLine { get; set; }

        [Required]
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}