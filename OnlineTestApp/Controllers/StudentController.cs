using Microsoft.AspNetCore.Mvc;
using OnlineTestApp.Services;
using OnlineTestApp.Models;
using OnlineTestApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {

        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        private IStudentRepository studentRepository = null;
        // GET: api/<ValuesController>
        /*
        [HttpGet]
        public IEnumerable<ExamModel> Get(int id)
        {
            var exams = questionRepository.GetAllQuestionByExamId(id);
            return exams;
        }*/

        [HttpGet]
        public IEnumerable<StudentModel> Get()
        {
            var students = studentRepository.GetAllStudents();
            return students;
        }



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            StudentModel studentFound = studentRepository.GetStudentById(id);
            if (studentFound != null)
                return Ok(studentFound);
            else
                return NotFound(id);

        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(StudentModel newStudent)
        {
            int createdId = studentRepository.AddNewStudent(newStudent);
            newStudent.Id = createdId;

            //URL FOR THE NEW  student
            return CreatedAtAction(nameof(Get), new { id = createdId }, newStudent);

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StudentModel newStudent)
        {
            bool isUpdated = studentRepository.UpdateStudent(newStudent);
            if (isUpdated)
                return Ok(id);
            else
                return NotFound("Student-" + id + " Not Found");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = studentRepository.DeleteStudent(id);
            if (isDeleted)
                return Ok("Student [" + id + "] Deleted");//200
            else

                return NotFound("Student-" + id + " Not Found");//404
        }
    }
}
