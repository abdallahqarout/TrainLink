using System;
namespace DAL.Entities
{
    public class University
    {
        public int UniversityId { get; set; } // Primary key
        public string Name { get; set; } // Name of the university
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

    }
}
