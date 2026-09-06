namespace TrainLink.ViewModels
{
    public class ManagementDetailsViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}