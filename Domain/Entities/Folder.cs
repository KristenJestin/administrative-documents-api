using Domain.Common;

namespace Domain.Entities
{
    public class Folder : DatedBaseEntity
    {
        public string Name { get; set; }
        public Folder Parent { get; set; }
        public string Color { get; set; }
    }
}
