using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.DTOs
{
    public class CourseDTO
    {
        [Required(ErrorMessage = "The CourseID is Required")]
        [Display(Name = "CourseID")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "The Title is Required")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Credits is Required")]
        [Display(Name = "Credits")]
        public int Credits { get; set; }
    }
}
