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
            //Questions = new List<DbQuestionModel>();
           // Grades = new Dictionary<StudentModel, decimal>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public int TeacherId { get; set; }

        public DateTime DateOfTest { get; set; }

        public int DurationMinutes { get; set; }

       // public Dictionary<StudentModel, decimal> Grades { get; set; }

        //public List<DbQuestionModel> Questions { get; set; }
    }
}
