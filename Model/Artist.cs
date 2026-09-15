namespace MyPractice4.Model
{
    public class Artist
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Talent { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<UserArtist> UserArtists { get; set; } = new List<UserArtist>();

    }
}
