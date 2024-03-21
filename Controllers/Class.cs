using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly List<Student> _students;

        public StudentsController()
        {
            _students = new List<Student>
            {
                new Student { Id = 1, Name = "John Doe", DateOfBirth = new DateTime(2000, 5, 15), Grade = "A" },
                new Student { Id = 2, Name = "Jane Smith", DateOfBirth = new DateTime(2001, 7, 20), Grade = "B" }
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return Ok(_students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            var student = _students.Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student student)
        {
            _students.Add(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            var existingStudent = _students.Find(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = student.Name;
            existingStudent.DateOfBirth = student.DateOfBirth;
            existingStudent.Grade = student.Grade;

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _students.Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            _students.Remove(student);
            return NoContent();
        }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Grade { get; set; }
    }
}
