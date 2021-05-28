using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineTestApp.Models;

namespace OnlineTestApp.Services
{
    public interface ITeacherRepository
    {
        //public List<ExamModel> GetAllExams();

        //       public ExamModel GetGradesByExam(int Id);

        //       public List<ExamModel> GetAllExamByTeacherId(int teacherId);

        //public int AddQuestionToExam(ExamModel newExam);  //can use insted the add question method. can get ques by exam id
        //public void DeleteQuestionFromExam(int id); // same as addQuestion above

        //       public bool ChangeDurationOfExam(int minutes, int TeacherId);
        public bool UpdateTeacher(TeacherModel teacherToUpdate);

        public TeacherModel GetTeacherById(int ID);

        //public TeacherModel GetTeacherByEmail(string email);

        public bool DeleteTeacher(int ID);

        public int AddNewTeacher(TeacherModel newTeacher);


    }
}
