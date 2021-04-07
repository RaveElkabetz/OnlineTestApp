using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Models
{
    public class StudentModel
    {
        public StudentModel()
        {
            UpcommingExams = new List<ExamModel>();
            ExamsTaken = new List<ExamModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStartedLearning { get; set; }
        public List<string> Courses { get; set; }
        public List<ExamModel> UpcommingExams { get; set; }

        public List<ExamModel> ExamsTaken { get; set; }


    }
}
