using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace SAT_APP_PROJECT.DATA.EF.Models
{
    [ModelMetadataType(typeof(StudentsMetadata))]
    public partial class Student { }
    [ModelMetadataType(typeof(CourseMetadata))]
    public partial class Course { }
    [ModelMetadataType(typeof(ScheduledClassStatusesMetadata))]
    public partial class ScheduledClassStatus { }
    [ModelMetadataType(typeof(ScheduledClassesMetadata))]
    public partial class ScheduledClass { }
    [ModelMetadataType(typeof(EnrollmentsMetadata))]
    public partial class Enrollment { }
    [ModelMetadataType(typeof (StudentStatusesMetadata))]
    public partial class StudentStatus { }
}
