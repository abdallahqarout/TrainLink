namespace TrainLink.ViewModels
{
    public class UserDetailsViewModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }

        // Student / Doctor
        public string UniversityName { get; set; }

        // Student
        public string StudentNumber { get; set; }

        public string Major { get; set; }

        // Company Supervisor
        public string CompanyName { get; set; }
    }
}