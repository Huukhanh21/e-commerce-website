namespace backend.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public byte Status { get; set; } = 1;
    }
}
