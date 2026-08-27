namespace MyPractice4.Model
{
    public class UserPlaces
    {
        public int UserId { get; set; }
        public User User { get; set; }


        public int PlaceId { get; set; }
        public Place Places { get; set; }
    }
}
