namespace MyPractice4.Model
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Artist> Artists { get; set; }
    }
}
