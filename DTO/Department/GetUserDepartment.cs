namespace MyPractice4.DTO.Department
{
    public class GetUserDepartment
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int? DepartmentId { get; set; }
        public string Name { get; set; }
        public string Head { get; set; }
    }
}
