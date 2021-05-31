using OnlineTestApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Services
{

    /// <summary>
    /// Access SQL And return C# Objects
    /// </summary>
    public class SqlQuestionsRepository : IQuestionsRepository
    {

        /// <summary>
        /// Property Connection String (WHERE THE FILE /URL FOR SQL)
        /// </summary>
        private string ConnectionString { get; set; }

        public SqlQuestionsRepository(): this(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OnlineExamsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False") 
        {

        }

        /// <summary>
        /// Empty
        /// </summary>
        public SqlQuestionsRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        //@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OnlineExamsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int AddQuestion(DbQuestionModel newQuestion)
        {
            //DbQuestionModel newQuestionToAdd= null;
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

                    string addQuestion = "INSERT INTO Questions (Question, Choices, Correct ,ExamId, Points)" +
                                     " VALUES (@Question,@Choices,@Correct,@ExamId,@Points); " +
                                     "SELECT SCOPE_IDENTITY()";
                    SqlCommand addCommand = new SqlCommand(addQuestion, connection);
                    addCommand.Parameters.AddWithValue("@Question", newQuestion.Question.Trim());
                    addCommand.Parameters.AddWithValue("@Choices", newQuestion.Choices.Trim());
                    addCommand.Parameters.AddWithValue("@Correct", newQuestion.Correct.Trim());
                    addCommand.Parameters.AddWithValue("@ExamId", newQuestion.ExamId);
                    addCommand.Parameters.AddWithValue("@Points", newQuestion.Points);
                    

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

        public bool DeleteQuestion(int id)
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
                    string deleteQuery = "DELETE FROM Questions WHERE Id = @Id";
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

        public bool UpdateQuestion(DbQuestionModel questionToUpdate)
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
                    string updateQuery = "UPDATE Questions SET " +
                                          "Question = @Question, " +
                                          "Choices = @Choices, " +
                                          "Correct = @Correct," +
                                          "ExamId = @ExamId, " +
                                          "Points = @Points " +
                                          "WHERE Id = @Id";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Id", questionToUpdate.Id);
                    updateCommand.Parameters.AddWithValue("@Question", questionToUpdate.Question.Trim());
                    updateCommand.Parameters.AddWithValue("@Choices", questionToUpdate.Choices.Trim());
                    updateCommand.Parameters.AddWithValue("@Correct", questionToUpdate.Correct.Trim());
                    updateCommand.Parameters.AddWithValue("@ExamId", questionToUpdate.ExamId);
                    updateCommand.Parameters.AddWithValue("@Points", questionToUpdate.Points);


                    int roesAffected = updateCommand.ExecuteNonQuery(); //heres the problem
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

        public List<DbQuestionModel> GetAllQuestionByExamId(int examId)
        {
            List<DbQuestionModel> questionsInExam = new List<DbQuestionModel>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                //Command (SQL QUEERY)
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Questions Where ExamId=@examId", connection);
                allCommand.Parameters.AddWithValue("@examId", examId);
                // READ ROW BY ROW READER
                using (var reader = allCommand.ExecuteReader())
                {
                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        DbQuestionModel questionModel = new DbQuestionModel();
                        questionModel.Id = reader.GetInt32(0);
                        questionModel.Question = reader.GetString(1);
                        questionModel.Choices = reader.GetString(2);
                        questionModel.Correct = reader.GetString(3);
                        questionModel.ExamId = reader.GetInt32(4);
                        questionModel.Points = reader.GetInt32(5);
                        questionsInExam.Add(questionModel);
                    }
                }
            }
            return questionsInExam;
        }

        public List<DbQuestionModel> GetAllActiveQuestionByExamId(int examId)
        {
            throw new NotImplementedException();
        }

        public DbQuestionModel GetQuestionById(int id)
        {
            try
            {
                DbQuestionModel questionToReturn = new DbQuestionModel();
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    SqlCommand allCommand = new SqlCommand("SELECT * FROM Questions Where Id=@id", connection);
                    allCommand.Parameters.AddWithValue("@Id", id);
                    using (var reader = allCommand.ExecuteReader())
                    {
                        questionToReturn.Id = reader.GetInt32(0);
                        questionToReturn.Question = reader.GetString(1);
                        questionToReturn.Choices = reader.GetString(2);
                        questionToReturn.Correct = reader.GetString(3);
                        questionToReturn.ExamId = reader.GetInt32(4);
                        questionToReturn.Points = reader.GetInt32(5);

                    }
                }
                return questionToReturn;
            }
            
            catch (Exception)
            {
                
                throw null;
                
            }


        }
    }
}
