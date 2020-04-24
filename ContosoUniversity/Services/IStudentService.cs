using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Services.Implements;

namespace ContosoUniversity.Services
{
    public interface IStudentService : IGenericService<Student>
    {
    }
}
