namespace MyPractice4.Model
{
    public class UserArtist
    {
        public int UserId { get; set; }
        public int ArtistId { get; set; }


       public Artist Artist { get; set; }
       public User User  { get; set; }
    }
}
