using OnlineTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Services
{
    public class SqlITeacherRepository : ITeacherRepository
    {
       
        private string ConnectionString { get; set; }
        bool ITeacherRepository.AddNewExam()
        {
            throw new NotImplementedException();
        }

        public SqlITeacherRepository()
        {
            this.ConnectionString = "";
        }

        int ITeacherRepository.AddQuestionToExam(ExamModel newExam)
        {
            throw new NotImplementedException();
        }

        bool ITeacherRepository.ChangeDurationOfExam(int minutes, int TeacherId)
        {
            throw new NotImplementedException();
        }

        bool ITeacherRepository.DeleteExam(int ID)
        {
            throw new NotImplementedException();
        }

        void ITeacherRepository.DeleteQuestionFromExam(int id)
        {
            throw new NotImplementedException();
        }

        List<ExamModel> ITeacherRepository.GetAllExamByTeacherId(int teacherId)
        {
            throw new NotImplementedException();
        }

        List<ExamModel> ITeacherRepository.GetAllExams()
        {
            throw new NotImplementedException();
        }

        ExamModel ITeacherRepository.GetGradesByExam(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
