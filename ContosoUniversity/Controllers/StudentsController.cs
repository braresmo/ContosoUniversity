﻿using System;
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
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            this.mapper = mapper;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var data = await _studentService.GetAll();

            var listStudents = data.Select(x => mapper.Map<StudentDTO>(x)).ToList();

            return View(listStudents);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetById(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            var studenDTO = mapper.Map<StudentDTO>(student);

            return View(studenDTO);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] StudentDTO studentDTO)
        {
            
            if (ModelState.IsValid)
            {
                //var student = new Student
                //{
                //    FirstMidName = studentDTO.FirstMidName,
                //    LastName = studentDTO.LastName,
                //    EnrollmentDate = studentDTO.EnrollmentDate
                //};
                var student = mapper.Map<Student>(studentDTO);

                await _studentService.Insert(student);
                return RedirectToAction(nameof(Index));
            }
            return View(studentDTO);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetById(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            var studenDTO = mapper.Map<StudentDTO>(student);

            return View(studenDTO);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,EnrollmentDate")] StudentDTO studentDTO)
        {
 
            
            if (ModelState.IsValid)
            {
                var student = mapper.Map<Student>(studentDTO);

                await _studentService.Update(student);
                return RedirectToAction(nameof(Index));
            }
            return View(studentDTO);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetById(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _studentService.Delete(id);
                return RedirectToAction(nameof(Index));

            }
            catch (System.Exception ex)
            {
                ViewBag.Message = ex.Message;
                ViewBag.Type = "danger";

                return View("Delete");
            }
        }
    }
    }

