using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineTestApp.Services;
using OnlineTestApp.Models;

namespace OnlineTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamInstanceController : Controller
    {

        public ExamInstanceController(IExamInstanceRepository examInstanceRepository)
        {
            this.examInstanceRepository = examInstanceRepository;
        }
        private IExamInstanceRepository examInstanceRepository = null;
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<ExamInstanceModel> Get()
        {
            var exams = examInstanceRepository.GetAllExamInstances();
            return exams;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IEnumerable<ExamInstanceModel> Get(int id)
        {
            var exams = examInstanceRepository.GetAllExamInstancesByTeachertId(id);
            return exams;


        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(ExamInstanceModel newExam)
        {
            int createdId = examInstanceRepository.AddExamInstance(newExam);
            newExam.Id = createdId;

            //URL FOR THE NEW  EXAM
            return CreatedAtAction(nameof(Get), new { id = createdId }, newExam);

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExamInstanceModel newExam)
        {
            bool isUpdated = examInstanceRepository.UpdateExamInstance(newExam);
            if (isUpdated)
                return Ok(id);
            else
                return NotFound("Exam-" + id + " Not Found");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = examInstanceRepository.DeleteExamInstance(id);
            if (isDeleted)
                return Ok("Exam [" + id + "] Deleted");//200
            else

                return NotFound("Exam-" + id + " Not Found");//404
        }
    }
}
