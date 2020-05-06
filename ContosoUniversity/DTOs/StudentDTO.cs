using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.DTOs
{
    public class StudentDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The LastName is Required")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The FirstMidName is Required")]
        [Display(Name = "FirstMidName")]
        public string FirstMidName { get; set; }
        [Required(ErrorMessage = "The EnrollmentDate is Required")]
        [Display(Name = "EnrollmentDate")]
        public DateTime EnrollmentDate { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", LastName, FirstMidName);

            }
        }
    }
}
