using OnlineTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Services
{
    public interface IExamRepository
    {

        public List<ExamModel> GetAllExams();


        public ExamModel GetExamById(int Id);

        public ExamModel GetExamByIdWithQueations(int Id);

        public List<ExamModel> GetAllExamByTeacherId(int teacherId);

        public List<ExamModel> GetAllExamByTitle(string title);


        public int AddExam(ExamModel newExam);


        public bool DeleteExam(int ID);

        public bool UpdateExam(ExamModel newExam);

        


    }
}
