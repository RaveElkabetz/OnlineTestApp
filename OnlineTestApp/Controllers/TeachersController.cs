using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTestApp.Models;
using OnlineTestApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private ITeacherRepository teacherRepository;

        public TeachersController(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }
        // GET: api/<ValuesController>
        /*
        [HttpGet]
        public IEnumerable<TeacherModel> Get()
        {
            var teachers = teacherRepository.GetAllExams();
            return teachers;
        }*/

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            TeacherModel teacherFound = teacherRepository.GetTeacherById(id);
            if (teacherFound != null)
                return Ok(teacherFound);
            else
                return NotFound(id);

        }

    
        // POST: TeachersController/Create
        [HttpPost]
       
        public IActionResult Post(TeacherModel newTeacher)
        {
            int createdId = teacherRepository.AddNewTeacher(newTeacher);
            newTeacher.Id = createdId;

            //URL FOR THE NEW  EXAM
            return CreatedAtAction(nameof(Get), new { id = createdId }, newTeacher);

        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TeacherModel newTeacher)
        {
            bool isUpdated = teacherRepository.UpdateTeacher(newTeacher);
            if (isUpdated)
                return Ok(id);
            else
                return NotFound("Teacher-" + id + " Not Found");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = teacherRepository.DeleteTeacher(id);
            if (isDeleted)
                return Ok("Teacher [" + id + "] Deleted");//200
            else

                return NotFound("Teacher-" + id + " Not Found");//404
        }


    }
}
