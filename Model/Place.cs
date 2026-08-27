namespace MyPractice4.Model
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<UserPlaces> UserPlaces { get; set; } = new List<UserPlaces>();
    }
}
