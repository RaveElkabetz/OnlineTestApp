using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Models
{
    public class ExamInstanceModel
    {

        public int Id { get; set; }

        public int ExamId { get; set; }

        public int TeacherId { get; set; }

        public int StudentId { get; set; }

        public DateTime DateOfTest { get; set; }

        public string ExamTitle { get; set; }
        
        public double Grade { get; set; }


    }
}
