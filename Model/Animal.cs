namespace MyPractice4.Model
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<UserAnimals> UserAnimals { get; set; }=new List<UserAnimals>();
    }
}
