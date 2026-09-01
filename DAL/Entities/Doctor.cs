using DAL.Entities;

public class Doctor
{
    public int DoctorId { get; set; }

    public int UserId { get; set; }

    public int UniversityId { get; set; }

    public string Name { get; set; }

    public User User { get; set; }

    public University University { get; set; }
}