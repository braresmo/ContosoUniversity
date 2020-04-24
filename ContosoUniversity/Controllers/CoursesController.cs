using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using ContosoUniversity.DTOs;
using AutoMapper;


namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IMapper mapper;

        public CoursesController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            this.mapper = mapper;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int? id)
        {
            if(id != null)
            {
                var dataStudents = await _courseService.GetStudentsByCourse(id.Value);

                ViewBag.Students = dataStudents.Select(x => mapper.Map<StudentDTO>(x)).ToList();
            }


            var data = await _courseService.GetAll();
            var listCourses = data.Select(x => mapper.Map<CourseDTO>(x)).ToList();

            return View(listCourses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetById(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            var courseDTO = mapper.Map<CourseDTO>(course);

            return View(courseDTO);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Title,Credits")] CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                var course = mapper.Map<Course>(courseDTO);

                await _courseService.Insert(course);
                return RedirectToAction(nameof(Index));
            }
            return View(courseDTO);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetById(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            var CourseDTO = mapper.Map<CourseDTO>(course);


            return View(CourseDTO);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID,Title,Credits")] CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                var course = mapper.Map<Course>(courseDTO);
                await _courseService.Update(course);
                return RedirectToAction(nameof(Index));
            }
            return View(courseDTO);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetById(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try {
                await _courseService.Delete(id);
                return RedirectToAction(nameof(Index));

            }
            catch(System.Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.Type = "danger";

                return View("Delete");
            }
                    }
    }
}
