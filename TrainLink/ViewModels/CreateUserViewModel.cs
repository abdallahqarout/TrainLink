namespace TrainLink.ViewModels
{
    public class CreateUserViewModel
    {
        // all usere
        public string Name { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }


        // Student / Doctor 
        public int? UniversityId { get; set; }


        // Student 
        public string? StudentNumber { get; set; }

        public string? Major { get; set; }


        // Company Supervisor 
        public int? CompanyId { get; set; }
    }
}