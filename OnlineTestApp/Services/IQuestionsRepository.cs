using OnlineTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Services
{
    public interface IQuestionsRepository
    {
        public int AddQuestion(DbQuestionModel newExam);
        public bool DeleteQuestion(int id);

        public bool UpdateQuestion(DbQuestionModel newExam);
        public List<DbQuestionModel> GetAllQuestionByExamId(int examId);

        public List<DbQuestionModel> GetAllActiveQuestionByExamId(int examId);

        public DbQuestionModel GetQuestionById(int id);

    }
}
