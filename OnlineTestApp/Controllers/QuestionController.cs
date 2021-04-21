using Microsoft.AspNetCore.Http;
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
    public class QuestionController : ControllerBase
    {
        public QuestionController(IQuestionsRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }
        private IQuestionsRepository questionRepository = null;
        // GET: api/<ValuesController>
        /*
        [HttpGet]
        public IEnumerable<ExamModel> Get(int id)
        {
            var exams = questionRepository.GetAllQuestionByExamId(id);
            return exams;
        }*/

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List <DbQuestionModel> examsQuestions = questionRepository.GetAllQuestionByExamId(id);
            if (examsQuestions != null)
                return Ok(examsQuestions);
            else
                return NotFound(id);

        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(DbQuestionModel newQuestion)
        {
            int createdId = questionRepository.AddQuestion(newQuestion);
            newQuestion.Id = createdId;

            //URL FOR THE NEW  EXAM
            return CreatedAtAction(nameof(Get), new { id = createdId }, newQuestion);

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DbQuestionModel newQuestion)
        {
            bool isUpdated = questionRepository.UpdateQuestion(newQuestion);
            if (isUpdated)
                return Ok(id);
            else
                return NotFound("Question-" + id + " Not Found");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = questionRepository.DeleteQuestion(id);
            if (isDeleted)
                return Ok("Question [" + id + "] Deleted");//200
            else

                return NotFound("Question-" + id + " Not Found");//404
        }
    }
}
