using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models
{
    public interface IModel
    {
        [Key]
        [Required]
        int Id { get; set; }
    }
}