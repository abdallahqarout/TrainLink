

namespace DAL.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public int UniversityId { get; set; }
        public string StudentNumber { get; set; }
        public string major { get; set; }
        public User User { get; set; }
        public University University { get; set; }

    }
}
