namespace MyPractice4.Model
{
    public class UserPlaces
    {



        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public User User { get; set; }
        public Place Place { get; set; }
    }
}
