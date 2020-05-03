using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Services;
using ContosoUniversity.Models;
using ContosoUniversity.DTOs;
using AutoMapper;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService enrollmentService;
        private readonly IStudentService studentService;
        private readonly ICourseService courseService;
        private readonly IMapper mapper;

        public EnrollmentsController(IEnrollmentService enrollmentService , IStudentService studentService, ICourseService courseService, IMapper mapper)
        {
            this.enrollmentService = enrollmentService;
            this.studentService = studentService;
            this.courseService = courseService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await enrollmentService.GetEnrollmentsByCoursestudent();
            var listEntollments = data.Select(x => mapper.Map<EnrollmentDTO>(x)).ToList();
            return View(listEntollments);
        }

        public async Task <ActionResult> Create()
        {
            var dataCourses = await courseService.GetAll();
            ViewBag.Courses = dataCourses.Select(x => mapper.Map<CourseDTO>(x)).ToList();
            var dataStudents = await studentService.GetAll();
            ViewBag.Students = dataStudents.Select(x => mapper.Map<StudentDTO>(x)).ToList();


            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(EnrollmentDTO enrollmentDTO)
        {
            if (!ModelState.IsValid) {

                var enrollment = mapper.Map<Enrollment>(enrollmentDTO);
                await enrollmentService.Insert(enrollment);
                return RedirectToAction(nameof(Index));

            }
            return View(enrollmentDTO);


        }
    }
}