using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>();
        private static int nextId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> AddStudent(Student student)
        {
            student.Id = nextId++;
            students.Add(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStudent(int id, Student student)
        {
            var existingStudent = students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
                return NotFound();
            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();
            students.Remove(student);
            return NoContent();
        }
    }
}