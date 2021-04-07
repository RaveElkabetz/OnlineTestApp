using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Models
{
    public class TeacherModel
    {
        
        public TeacherModel()
        {
            TeachingTopics = new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStartedWorking { get; set; }
        public List<string> TeachingTopics { get; set; }
        public List<ExamModel> UpcommingExams { get; set; }

        public List<ExamModel>  ExamsGiven { get; set; }

    
    }
}
