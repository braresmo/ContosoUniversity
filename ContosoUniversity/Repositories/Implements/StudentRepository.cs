using System;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repositories.Implements
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private SchoolContext SchoolContext;
        public StudentRepository(SchoolContext SchoolContext) : base(SchoolContext) {
            this.SchoolContext = SchoolContext;
        }

        public async Task<IEnumerable<Course>> GetStudentsByStudent(int id)
        {
            //var listStudents = await SchoolContext.Enrollments
            //    .Include(x => x.Student)
            //    .Where(x => x.CourseID == id)
            //    .Select(x => x.Student)
            //    .ToListAsync();

            var listCourses = await (from enrollment in SchoolContext.Enrollments
                                      join course in SchoolContext.Courses on enrollment.CourseID equals course.CourseID
                                      where enrollment.StudentID == id
                                      select course).ToListAsync();
            return listCourses;

        }

    }
}
