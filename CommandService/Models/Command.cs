using Common.Models;

namespace CommandService.Models
{
    public class Command : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}