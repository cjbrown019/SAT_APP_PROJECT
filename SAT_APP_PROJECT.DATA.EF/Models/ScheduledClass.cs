using System;
using System.Collections.Generic;

namespace SAT_APP_PROJECT.DATA.EF.Models
{
    public partial class ScheduledClass
    {
        public ScheduledClass()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int ScheduledClassId { get; set; }
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InstructorName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Scsid { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual ScheduledClassStatus Scs { get; set; } = null!;
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
