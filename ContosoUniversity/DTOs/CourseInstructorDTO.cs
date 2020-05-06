using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.DTOs
{
    public class CourseInstructorDTO
    {
        public string ID { get; set; }
        public int CourseID { get; set; }
        public int InstructorID { get; set; }

        public Course Course { get; set; }
        public Instructor Instructor { get; set; }
    }
}
