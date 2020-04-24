using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Repositories.Implements
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private SchoolContext SchoolContext;
        public StudentRepository(SchoolContext SchoolContext) : base(SchoolContext) {
            this.SchoolContext = SchoolContext;
        }

    }
}
