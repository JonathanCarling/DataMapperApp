namespace DataMapperApp.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}