using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineTestApp.Models;

namespace OnlineTestApp.Services
{
    interface ITeacherRepository
    {
        public List<ExamModel> GetAllExams();

        public ExamModel GetGradesByExam(int Id);

        public List<ExamModel> GetAllExamByTeacherId(int teacherId);

        public int AddQuestionToExam(ExamModel newExam);
        public void DeleteQuestionFromExam(int id);

        public bool ChangeDurationOfExam(int minutes, int TeacherId);

        public bool DeleteExam(int ID);

        public bool AddNewExam();


    }
}
