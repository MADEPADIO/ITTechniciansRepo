namespace TechniciansRepo.Models
{
    public class KnowledgeBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string? Diagnosis { get; set; }
        public string? Fix { get; set; }
        public string? Component { get; set; }
        public string? Model { get; set; }
        public string? OS { get; set; }

        public KnowledgeBase() { }
    }
}
