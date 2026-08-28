using System;
namespace DAL.Entities
{
    public class Company
    {
        public int CompanyId { get; set; } // Primary key
        public string Name { get; set; } // Name of the company
        public string Address { get; set; } // Address of the company

        public string phone  { get; set; } // Phone number of the company 
        public ICollection<CompanySupervisor> CompanySupervisors { get; set; }
        public ICollection<Training> Trainings { get; set; }

    }
}
