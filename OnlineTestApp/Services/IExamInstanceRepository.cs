using OnlineTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Services
{
    public interface IExamInstanceRepository
    {
        public List<ExamInstanceModel> GetAllExamInstances();


        public ExamInstanceModel GetExamInstanceById(int Id);

        public List<ExamInstanceModel> GetAllExamInstancesByStudentId(double studentId);

        public List<ExamInstanceModel> GetAllExamInstancesByTeachertId(int teacherId);


        public int AddExamInstance(ExamInstanceModel newExamInstance);


        public bool DeleteExamInstance(int ID);

        public bool UpdateExamInstance(ExamInstanceModel newExamInstance);
        public List<ExamInstanceModel> GetAllExamInstancesByExamId(string id);
    }
}
