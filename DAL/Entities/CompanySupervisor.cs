using System;
namespace DAL.Entities
{
    public class CompanySupervisor
    {
        public int CompanySupervisorId { get; set; } // Primary key
        public int UserId { get; set; } // Foreign key to User
        public int CompanyId { get; set; } // Foreign key to Company
        public string Name { get; set; } // name of supervisor

        //Relationships
        public User User { get; set; }
        public Company Company { get; set; }
        public ICollection<Training> Trainings { get; set; }


    }
}
