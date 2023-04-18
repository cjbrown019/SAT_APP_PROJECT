using System;
using System.Collections.Generic;

namespace SAT_APP_PROJECT.DATA.EF.Models
{
    public partial class Student
    {
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Major { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public string? PhotoUrl { get; set; }
        public int Ssid { get; set; }

        public virtual StudentStatus Ss { get; set; } = null!;
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
