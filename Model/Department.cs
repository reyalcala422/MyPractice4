namespace MyPractice4.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Head { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
