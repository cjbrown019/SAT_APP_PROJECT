using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT_APP_PROJECT.DATA.EF.Models
{
    public class StudentsMetadata
    {
        public int StudentId { get; set; }
        [StringLength(20, ErrorMessage = "Maximum 20 characters"), Required(ErrorMessage = "*Required")]
        public string FirstName { get; set; }
        [StringLength(20, ErrorMessage = "Maximum 20 characters"), Required(ErrorMessage = "*Required")]
        public string LastName { get; set; }
        [StringLength(15, ErrorMessage = "Maximum 15 characters"), DisplayFormat(NullDisplayText ="Undeclared")]
        public string Major { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters"), DisplayFormat(NullDisplayText = "N/A")]
        public string Address { get; set; }
        [StringLength(25, ErrorMessage = "Maximum 25 characters"), DisplayFormat(NullDisplayText = "N/A")]
        public string City { get; set; }
        [StringLength(2, ErrorMessage = "Maximum 2 characters"), DisplayFormat(NullDisplayText = "N/A")]
        public string State { get; set; }
        [StringLength(10, ErrorMessage = "Maximum 10 characters"), DisplayFormat(NullDisplayText = "N/A")]
        public string ZipCode { get; set; }
        [StringLength(13, ErrorMessage = "Maximum 13 characters"),Phone(ErrorMessage ="Please enter a valid phone number"), DisplayFormat(NullDisplayText = "N/A")]
        public string Phone { get; set; }
        [StringLength(60, ErrorMessage = "Maximum 60 characters"), Required(ErrorMessage = "*Required"), EmailAddress(ErrorMessage ="Please enter a VALID email")]
        public string Email { get; set; }

        [StringLength(100)]
        [DisplayName("Image")]//looks better on labels
        public string PhotoUrl { get; set; }

        //Student Statuses key
        public int SSID { get; set; }


    }

    public class CourseMetadata
    {
        public int CourseId { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters"), Required(ErrorMessage = "*Required")]
        public string CourseName { get; set; }
        [StringLength(8000, ErrorMessage = "Maximum 8000 characters"), Required(ErrorMessage = "*Required")]
        public string CourseDescription { get; set; }
        [Required(ErrorMessage ="*Required")]
        public int CreditHours { get; set; }
        [StringLength(250, ErrorMessage = "Maximum 250 characters"), DisplayFormat(NullDisplayText = "N/A")]
        public string Curriculum { get; set; }
        [StringLength(500, ErrorMessage = "Maximum 500 characters"), DisplayFormat(NullDisplayText = "N/A")]
        public string Notes { get; set; }
        [Required(ErrorMessage = "*Required")]

        public bool isActive { get; set; }

    }

    public class ScheduledClassStatusesMetadata
    {
        public int SCSID { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters"), Required(ErrorMessage ="*Required")]
        public string SCSName { get; set; }
    }

    public class ScheduledClassesMetadata
    {
        public int ScheduledClassId { get; set; }
        [Required(ErrorMessage ="*Required")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "*Required"),DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "*Required"),DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }
        [StringLength(40,ErrorMessage= "Maximum 40 Characters"),Required(ErrorMessage ="*Required")]

        public string InstructorName { get; set; }  
        [StringLength(20, ErrorMessage = "Maximum 20 Characters"), Required(ErrorMessage = "*Required")]
        public string Location { get; set; }
        [Required(ErrorMessage ="*Required")]

        public int SCSID { get; set; }
    }

    public class EnrollmentsMetadata
    {
        public int EnrollmentId { get; set; }
        [Required(ErrorMessage = "*Required")]

        public int StudentId { get; set; }
        [Required(ErrorMessage = "*Required")]

        public int ScheduledClassId { get; set; }
        [Required(ErrorMessage = "*Required"),DataType(DataType.Date)]  
         
        public DateOnly EnrollmentDate { get; set; }
    }

    public class StudentStatusesMetadata
    {
        public int SSID { get; set; }
        [StringLength(30, ErrorMessage = "Maximum 30 Characters"), Required(ErrorMessage = "*Required")]

        public string SSName { get; set; }
        [StringLength(250, ErrorMessage = "Maximum 250 Characters"), DisplayFormat(NullDisplayText = "N/A")]

        public string SSDescription { get; set; }
    }
}
