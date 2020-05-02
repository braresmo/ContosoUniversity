using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Services;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        private IEnrollmentService enrollmentService;
        private IStudentService studentService;
        private ICourseService courseService;

        public EnrollmentsController(IEnrollmentService enrollmentService , IStudentService studentService, ICourseService courseService)
        {
            this.enrollmentService = enrollmentService;
            this.studentService = studentService;
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var listEntollments = await enrollmentService.GetAll();
            return View(listEntollments);
        }

        public async Task <ActionResult> Create()
        {
            ViewBag.Courses = await courseService.GetAll();
            ViewBag.Students = await studentService.GetAll();


            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Enrollment model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await enrollmentService.Insert(model);

            return RedirectToAction("Index");
        }
    }
}