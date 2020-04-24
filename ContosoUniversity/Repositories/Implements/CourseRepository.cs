using System;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repositories.Implements
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private SchoolContext SchoolContext;

        public CourseRepository(SchoolContext SchoolContext) : base(SchoolContext) {
            this.SchoolContext = SchoolContext;
        }

        public async Task<IEnumerable<Student>> GetStudentsByCourse(int id)
        {
            //var listStudents = await SchoolContext.Enrollments
            //    .Include(x => x.Student)
            //    .Where(x => x.CourseID == id)
            //    .Select(x => x.Student)
            //    .ToListAsync();

            var listStudents = await (from enrollment in SchoolContext.Enrollments
                                      join student in SchoolContext.Students on enrollment.StudentID equals student.ID
                                      where enrollment.CourseID == id
                                      select student).ToListAsync();
            return listStudents;

        }
        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
                throw new System.Exception("The entity is null");

            var flag = SchoolContext.Enrollments.Any(x => x.CourseID == id);

            if (flag)
                throw new Exception("The Course Have Enrollments");

            SchoolContext.Courses.Remove(entity);
            await SchoolContext.SaveChangesAsync();

        }

    }
}
