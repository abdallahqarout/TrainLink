using System;
namespace DAL.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; } // Primary key
        public int UserId { get; set; } // Foreign key to User
        public int UniversityId { get; set; } // Foreign key to University
        public string Name { get; set; } // name of doctor

        // Relationships
        public User User { get; set; } 
        public University University { get; set; }


    }
}
