using OnlineTestApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Services
{
    public class SqlExamInstanceRepository : IExamInstanceRepository
    {
        private string ConnectionString { get; set; }

        public SqlExamInstanceRepository() : this(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OnlineExamsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {

        }

        public SqlExamInstanceRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public int AddExamInstance(ExamInstanceModel newExamInstance)
        {

            ExamInstanceModel examInstanceModel = null;
            SqlConnection connection = null;
            int newId = -1;
            try
            {
                //01 Create Connection
                using (connection = new SqlConnection(this.ConnectionString))
                {
                    //02 Open Connection
                    connection.Open();

                    //string timeText =  newExam.DateStarted.ToString("yyyy-MM-dd HH:mm:ss");
                    // //Command (SQL QUEERY)
                    // string addExam = "INSERT INTO Exams (Title, DateStarted, DurationMinutes ,TeacherId)" +
                    //                  $" VALUES ('{newExam.Title}','{timeText}',{newExam.DurationMinutes},{newExam.TeachrId})";

                    string addExamInstance = "INSERT INTO EXAMSINSTANCES (ExamId, StudentId, TeacherId ,DateOfTest, ExamTitle, Grade)" +
                                     " VALUES (@ExamId, @StudentId, @TeacherId ,@DateOfTest, @ExamTitle, @Grade); " +
                                     "SELECT SCOPE_IDENTITY()";
                    SqlCommand addCommand = new SqlCommand(addExamInstance, connection);
                    //addCommand.Parameters.AddWithValue("@Id", newExamInstance.Id);
                    addCommand.Parameters.AddWithValue("@ExamId", newExamInstance.ExamId);
                    addCommand.Parameters.AddWithValue("@StudentId", newExamInstance.StudentId);
                    addCommand.Parameters.AddWithValue("@TeacherId", newExamInstance.TeacherId);
                    addCommand.Parameters.AddWithValue("@DateOfTest", newExamInstance.DateOfTest);
                    addCommand.Parameters.AddWithValue("@ExamTitle", newExamInstance.ExamTitle);
                    addCommand.Parameters.AddWithValue("@Grade", newExamInstance.Grade);
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

        public bool DeleteExamInstance(int ID)
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
                    string deleteQuery = "DELETE FROM EXAMSINSTANCES WHERE Id = @Id";
                    SqlCommand DeleteCommand = new SqlCommand(deleteQuery, connection);
                    DeleteCommand.Parameters.AddWithValue("@Id", ID);

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

        public List<ExamInstanceModel> GetAllExamInstances()
        {
            //SqlConnection connection = null;
            List<ExamInstanceModel> examsInstancesList = new List<ExamInstanceModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM EXAMSINSTANCES", connection);
                using (var reader = allCommand.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        ExamInstanceModel examInstanceModel = new ExamInstanceModel();
                        examInstanceModel.Id = reader.GetInt32(0);
                        examInstanceModel.ExamId = reader.GetInt32(1);
                        examInstanceModel.StudentId = reader.GetInt32(2);
                        examInstanceModel.TeacherId = reader.GetInt32(3);
                        examInstanceModel.DateOfTest = reader.GetDateTime(4);
                        examInstanceModel.ExamTitle = reader.GetString(5);
                        examInstanceModel.Grade = reader.GetDouble(6);
                        examsInstancesList.Add(examInstanceModel);
                    }
                }
            }



            return examsInstancesList;
        }

        public List<ExamInstanceModel> GetAllExamInstancesByStudentId(int studentId)
        {
            ExamInstanceModel examInstanceModel = null;
            List<ExamInstanceModel> examsInstanceList = new List<ExamInstanceModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM EXAMSINSTANCES WHERE StudentId =" + studentId.ToString(), connection);
                using (var reader = allCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        examInstanceModel = new ExamInstanceModel();
                        examInstanceModel.Id = reader.GetInt32(0);
                        examInstanceModel.ExamId = reader.GetInt32(1);
                        examInstanceModel.TeacherId = reader.GetInt32(2);
                        examInstanceModel.StudentId = reader.GetInt32(3);
                        examInstanceModel.DateOfTest = reader.GetDateTime(4);
                        examInstanceModel.ExamTitle = reader.GetString(5);
                        examInstanceModel.Grade = reader.GetDouble(6);
                        examsInstanceList.Add(examInstanceModel);
                    }
                }
            }
            return examsInstanceList;

        }

        public List<ExamInstanceModel> GetAllExamInstancesByTeachertId(int teacherId)
        {
            ExamInstanceModel examInstanceModel = null;
            List<ExamInstanceModel> examsInstanceList = new List<ExamInstanceModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM EXAMSINSTANCES WHERE TeacherId =" + teacherId.ToString(), connection);
                using (var reader = allCommand.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        examInstanceModel = new ExamInstanceModel();
                        examInstanceModel.Id = reader.GetInt32(0);
                        examInstanceModel.ExamId = reader.GetInt32(1);
                        examInstanceModel.StudentId = reader.GetInt32(2);
                        examInstanceModel.TeacherId = reader.GetInt32(3);
                        examInstanceModel.DateOfTest = reader.GetDateTime(4);
                        examInstanceModel.ExamTitle = reader.GetString(5);
                        examInstanceModel.Grade = reader.GetDouble(6);
                        examsInstanceList.Add(examInstanceModel);
                    }
                    

                    
                }
            }
            return examsInstanceList;
        }

        public ExamInstanceModel GetExamInstanceById(int Id)
        {
            ExamInstanceModel examInstanceModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {


                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM EXAMSINSTANCES WHERE Id =" + Id.ToString(), connection);



                using (var reader = allCommand.ExecuteReader())
                {

                   

                        examInstanceModel = new ExamInstanceModel();
                        examInstanceModel.Id = reader.GetInt32(0);
                        examInstanceModel.ExamId = reader.GetInt32(1);
                        examInstanceModel.TeacherId = reader.GetInt32(2);
                        examInstanceModel.StudentId = reader.GetInt32(3);
                        examInstanceModel.DateOfTest = reader.GetDateTime(4);
                        examInstanceModel.ExamTitle = reader.GetString(5);
                        examInstanceModel.Grade = reader.GetDouble(6);

                    
                }
            }
            return examInstanceModel;

        }

        public bool UpdateExamInstance(ExamInstanceModel examInstanceToUpdate)
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
                    string updateQuery = "UPDATE EXAMSINSTANCES SET " +

                                          "ExamId = @ExamId, " +
                                          "TeacherId = @TeacherId, " +
                                          "StudentId = @StudentId," +
                                          "DateOfTest = @DateOfTest, " +
                                          "ExamTitle = @ExamTitle, " +
                                          "Grade = @Grade " +
                                          "WHERE Id = @Id";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Id", examInstanceToUpdate.Id);
                    updateCommand.Parameters.AddWithValue("@ExamId", examInstanceToUpdate.ExamId);
                    updateCommand.Parameters.AddWithValue("@TeacherId", examInstanceToUpdate.TeacherId);
                    updateCommand.Parameters.AddWithValue("@StudentId", examInstanceToUpdate.StudentId);
                    updateCommand.Parameters.AddWithValue("@DateOfTest", examInstanceToUpdate.DateOfTest);
                    updateCommand.Parameters.AddWithValue("@ExamTitle", examInstanceToUpdate.ExamTitle);
                    updateCommand.Parameters.AddWithValue("@Grade", examInstanceToUpdate.Grade);


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
    }
}
