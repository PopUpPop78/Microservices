using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public interface IModel
    {
        [Key]
        [Required]
        int Id { get; set; }
    }
}