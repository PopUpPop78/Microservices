using Common.Events;

namespace Common.Dtos
{
    public class PlatformPublishDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public EventType Event { get; set; }
    }
}