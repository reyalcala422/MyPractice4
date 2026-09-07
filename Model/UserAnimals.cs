namespace MyPractice4.Model
{
    public class UserAnimals
    {
        public int UserId { get; set; }
        public int AnimalId { get; set; }


        public User User { get; set; }
        public Animal Animal { get; set; }
    }
}
