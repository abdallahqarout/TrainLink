using DAL.Entities;

public class CompanySupervisor
{
    public int CompanySupervisorId { get; set; }

    public int UserId { get; set; }

    public int CompanyId { get; set; }

    public string Name { get; set; }

    public User User { get; set; }

    public Company Company { get; set; }

    public ICollection<Training> Trainings { get; set; }
}