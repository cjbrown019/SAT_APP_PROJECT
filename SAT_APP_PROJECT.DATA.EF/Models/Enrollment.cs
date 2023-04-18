using System;
using System.Collections.Generic;

namespace SAT_APP_PROJECT.DATA.EF.Models
{
    public partial class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int ScheduledClassId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual ScheduledClass ScheduledClass { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
