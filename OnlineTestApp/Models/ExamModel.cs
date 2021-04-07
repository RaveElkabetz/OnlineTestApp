using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Models
{
    public class ExamModel
    {

        public ExamModel()
        {
            Questions = new List<QuestionModel>();
            Grades = new Dictionary<StudentModel, int>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public int TeacherId { get; set; }

        public DateTime DateStarted { get; set; }

        public int DurationMinutes { get; set; }

        public Dictionary<StudentModel, int> Grades { get; set; }

        public List<QuestionModel> Questions { get; set; }
    }
}
