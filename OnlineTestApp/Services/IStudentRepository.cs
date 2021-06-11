using OnlineTestApp.Models;
using System.Collections.Generic;

namespace OnlineTestApp.Controllers
{
    public interface IStudentRepository
    {
        public bool UpdateStudent(StudentModel studentToUpdate);

        public StudentModel GetStudentById(int ID);

        //public TeacherModel GetTeacherByEmail(string email);

        public bool DeleteStudent(int ID);

        public int AddNewStudent(StudentModel newTeacher);

        public List<StudentModel> GetAllStudents();

    }
}