using System;
namespace DAL.Entities
{
    public class Student
    {
        public int StudentId { get; set; } // Primary key
        public string Name { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public int UniversityId { get; set; } // Foreign key to University
        public string StudentNumber { get; set; } // student number
        public string major { get; set; } // student major

        // Relationships
        public User User { get; set; } 
        public University University { get; set; }

    }
}
