using OnlineTestApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OnlineTestApp.Services
{
    public class SqlExamRepository : IExamRepository
    {
        /// <summary>
        /// Property Connection String (WHERE THE FILE /URL FOR SQL)
        /// </summary>
        private string ConnectionString { get; set; }

        public SqlExamRepository() 
        {
            this.ConnectionString =  @"Data Source=(localdb)\ProjectsV13;Initial Catalog=EXAMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public int AddExam(ExamModel newExam)
        {
            ExamModel examModel = null;
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

                    string addExam = "INSERT INTO Exams (Title, DateStarted, DurationMinutes ,TeacherId)" +
                                     " VALUES (@Title,@DateStarted,@DurationMinutes,@TeacherId); " +
                                     "SELECT SCOPE_IDENTITY()";
                    SqlCommand addCopmmand = new SqlCommand(addExam, connection);
                    addCopmmand.Parameters.AddWithValue("@Title", newExam.Title);
                    addCopmmand.Parameters.AddWithValue("@DateStarted", newExam.DateStarted);
                    addCopmmand.Parameters.AddWithValue("@DurationMinutes", newExam.DurationMinutes);
                    addCopmmand.Parameters.AddWithValue("@TeacherId", newExam.TeacherId);

                    newId = Convert.ToInt32(addCopmmand.ExecuteScalar());

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

        public bool DeleteExam(int ID)
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
                    string deleteQuery = "DELETE FROM Exams WHERE Id = @Id";
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

        public List<ExamModel> GetAllExamByTeacherId(int teacherId)
        {
            ExamModel examModel = null;
            List<ExamModel> examList = new List<ExamModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM EXAMS WHERE TeacherId =" + teacherId.ToString());
                using(var reader = allCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        examModel = new ExamModel();
                        examModel.Id = reader.GetInt32(0);
                        examModel.Title = reader.GetString(1);
                        examModel.DateStarted = reader.GetDateTime(2);
                        examModel.DurationMinutes = reader.GetInt32(3);
                        examModel.TeacherId = reader.GetInt32(4);
                    }
                }
            }
            return examList;
        }

        public List<ExamModel> GetAllExamByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public List<ExamModel> GetAllExams()
        {
            List<ExamModel> examsList = new List<ExamModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM EXAMS");
                using (var reader = allCommand.ExecuteReader()) 
                {
                    while (reader.Read()) 
                    {
                        ExamModel examModel = new ExamModel();
                        examModel.Id = reader.GetInt32(0);
                        examModel.Title = reader.GetString(1);
                        examModel.DateStarted = reader.GetDateTime(2);
                        examModel.DurationMinutes = reader.GetInt32(3);
                        examModel.TeacherId = reader.GetInt32(4);
                        examsList.Add(examModel);
                    }
                }
            }

            return examsList;
        }

        public ExamModel GetExamById(int Id)
        {
            ExamModel examModel = null;
            using (var connection = new SqlConnection(this.ConnectionString)) 
            {
                
                
                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM EXMAS WHERE Id =" + Id.ToString(), connection);
                
                
             
                Console.WriteLine("Cannot connect to the data base!");
                return null;
                

                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        examModel = new ExamModel();
                        examModel.Id = reader.GetInt32(0);
                        examModel.Title = reader.GetString(1);
                        examModel.DateStarted = reader.GetDateTime(2);
                        examModel.DurationMinutes = reader.GetInt32(3);
                        examModel.TeacherId = reader.GetInt32(4);

                    }
                }
            }
            return examModel;
            
        }

        public ExamModel GetExamByIdWithQueations(int Id)
        {

            throw new NotImplementedException();
        }

        public bool UpdateExam(ExamModel examToUpdate)
        {
            SqlConnection connection = null;
            bool isUpdated = false;
            try
            {
                //create connection
                using(connection = new SqlConnection(this.ConnectionString))
                {
                    //02 open connection
                    connection.Open();
                    string updateQuery = "UPDATE Exams SET " +
                                          "Title = @Title, " +
                                          "DateStarted = @DateStarted, " +
                                          "DurationMinutes = @DurationMinutes," +
                                          "TeacherId = @TeacherId " +
                                          "WHERE Id = @Id";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Id", examToUpdate.Id);
                    updateCommand.Parameters.AddWithValue("@Title", examToUpdate.Title);
                    updateCommand.Parameters.AddWithValue("@DateStarted", examToUpdate.DateStarted);
                    updateCommand.Parameters.AddWithValue("@DurationMinutes", examToUpdate.DurationMinutes);
                    updateCommand.Parameters.AddWithValue("@TeacherId", examToUpdate.TeacherId);

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
    

