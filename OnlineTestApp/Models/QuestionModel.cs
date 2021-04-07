using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<string> Choices { get; set; }
        public string Answer { get; set; }
        public int Points { get; set; }
    }
}
