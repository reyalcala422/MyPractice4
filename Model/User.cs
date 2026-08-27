namespace MyPractice4.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection <UserPlaces> UserPlaces { get; set; }

    }
}
