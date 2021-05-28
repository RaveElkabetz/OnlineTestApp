using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineTestApp.Services;
using OnlineTestApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        public ExamsController(IExamRepository examRepository)
        {
            this.examRepository = examRepository;
        }
        private IExamRepository examRepository = null;
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<ExamModel> Get()
        {
            var exams = examRepository.GetAllExams();
            return exams;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IEnumerable<ExamModel> Get(int id)
        {
            var exams = examRepository.GetAllExamByTeacherId(id);
            return exams;
       

        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(ExamModel newExam)
        {
            int createdId = examRepository.AddExam(newExam);
            newExam.Id = createdId;

            //URL FOR THE NEW  EXAM
            return CreatedAtAction(nameof(Get), new { id = createdId }, newExam);

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExamModel newExam)
        {
            bool isUpdated = examRepository.UpdateExam(newExam);
            if (isUpdated)
                return Ok(id);
            else
                return NotFound("Exam-" + id + " Not Found");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = examRepository.DeleteExam(id);
            if (isDeleted)
                return Ok("Exam [" + id + "] Deleted");//200
            else

                return NotFound("Exam-" + id + " Not Found");//404
        }
    }
}
