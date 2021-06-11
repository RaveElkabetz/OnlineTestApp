using OnlineTestApp.Controllers;
using OnlineTestApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Services
{
    public class SqlStudentRepository : IStudentRepository
    {
        private string ConnectionString { get; set; }

        public SqlStudentRepository() : this(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OnlineExamsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {

        }
        public SqlStudentRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public int AddNewStudent(StudentModel newStudent)
        {
            //ExamModel examModel = null;
            SqlConnection connection = null;
            int newId = -1;
            try
            {
                //01 Create Connection
                using (connection = new SqlConnection(this.ConnectionString))
                {
                    //02 Open Connection
                    connection.Open();


                    string addStudent = "INSERT INTO Students (Name, Password, DateStartedLearning, examsIdList)" +
                                     " VALUES (@Name, @Password, @DateStartedLearning, @examsIdList); " +
                                     "SELECT SCOPE_IDENTITY()";
                    SqlCommand addCommand = new SqlCommand(addStudent, connection);
                    addCommand.Parameters.AddWithValue("@Name", newStudent.Name);
                    addCommand.Parameters.AddWithValue("@Password", newStudent.Password);
                    addCommand.Parameters.AddWithValue("@DateStartedWorking", newStudent.DateStartedLearning);
                    addCommand.Parameters.AddWithValue("@examsIdList", newStudent.examsIdList);
                    newId = Convert.ToInt32(addCommand.ExecuteScalar());



                }

                return newId;

            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public bool DeleteStudent(int id)
        {
            SqlConnection connection = null;
            bool isDeleted = false;
            try
            {
                //01 Create Connection
                using (connection = new SqlConnection(this.ConnectionString))
                {
                    //02 Open Connection
                    connection.Open();
                    string deleteQuery = "DELETE FROM Students WHERE Id = @Id";
                    SqlCommand DeleteCommand = new SqlCommand(deleteQuery, connection);
                    DeleteCommand.Parameters.AddWithValue("@Id", id);

                    int roesAffected = DeleteCommand.ExecuteNonQuery();
                    if (roesAffected > 0)
                        isDeleted = true;
                }

                return isDeleted;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public bool UpdateStudent(StudentModel studentToUpdate)
        {
            SqlConnection connection = null;
            bool isUpdated = false;
            try
            {
                //create connection
                using (connection = new SqlConnection(this.ConnectionString))
                {
                    //02 open connection
                    connection.Open();
                    string updateQuery = "UPDATE Students SET " +
                                          "Name = @Name, " +
                                          "Password = @Password, " +
                                          "DateStartedLearning = @DateStartedLearning," +
                                          "examsIdList = @examsIdList, " +
                                          "WHERE Id = @Id";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Name", studentToUpdate.Name.Trim());
                    updateCommand.Parameters.AddWithValue("@Password", studentToUpdate.Password.Trim());
                    updateCommand.Parameters.AddWithValue("@DateStartedLearning", studentToUpdate.DateStartedLearning);
                    updateCommand.Parameters.AddWithValue("@examsIdList", studentToUpdate.examsIdList.Trim());


                    int roesAffected = updateCommand.ExecuteNonQuery(); 
                    if (roesAffected > 0)
                        isUpdated = true;

                }


                return isUpdated;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public StudentModel GetStudentById(int ID)
        {
            StudentModel newStudent = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {


                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Students WHERE Id =" + ID.ToString(), connection);



                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        newStudent = new StudentModel();
                        newStudent.Id = reader.GetInt32(0);
                        newStudent.Name = reader.GetString(1);
                        newStudent.Password = reader.GetString(2);
                        newStudent.DateStartedLearning = reader.GetDateTime(3);
                        newStudent.examsIdList = reader.GetString(4);


                    }
                }
            }
            return newStudent;

        }

        public List<StudentModel> GetAllStudents()
        {
            //SqlConnection connection = null;
            List<StudentModel> studentsList = new List<StudentModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Students", connection);
                using ( var reader = allCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        StudentModel studentModel = new StudentModel();
                        studentModel.Id = reader.GetInt32(0);
                        studentModel.Name = reader.GetString(1);
                        studentModel.Password = reader.GetString(2);
                        studentModel.DateStartedLearning = reader.GetDateTime(3);
                        studentModel.examsIdList = reader.GetString(4);
                        studentsList.Add(studentModel);
                    }
                }
            }



            return studentsList;
        }
    }
}
