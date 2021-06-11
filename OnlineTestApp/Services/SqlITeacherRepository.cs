using OnlineTestApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestApp.Services
{
    public class SqlITeacherRepository : ITeacherRepository
    {
       
        private string ConnectionString { get; set; }


        
        public SqlITeacherRepository(): this(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OnlineExamsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False") 
        {

        }
        
        public SqlITeacherRepository(string connectionString)
        {
            this.ConnectionString = connectionString;
        }





        public bool DeleteTeacher(int ID)
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
                    string deleteQuery = "DELETE FROM Teachers WHERE Id = @Id";
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

        public int AddNewTeacher(TeacherModel newTeacher)
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

      
                    string addTeacher = "INSERT INTO Teachers (Name, Password, Email, DateStartedWorking)" +
                                     " VALUES (@Name, @Password, @Email, @DateStartedWorking); " +
                                     "SELECT SCOPE_IDENTITY()";
                    SqlCommand addCommand = new SqlCommand(addTeacher, connection);
                    addCommand.Parameters.AddWithValue("@Name", newTeacher.Name);
                    addCommand.Parameters.AddWithValue("@Password", newTeacher.Password);
                    addCommand.Parameters.AddWithValue("@Email", newTeacher.Email);
                    addCommand.Parameters.AddWithValue("@DateStartedWorking", newTeacher.DateStartedWorking);
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


        public TeacherModel GetTeacherById(int ID)
        {
            TeacherModel teacherModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {


                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Teachers WHERE Id =" + ID.ToString(), connection);



                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        teacherModel = new TeacherModel();
                        teacherModel.Id = reader.GetInt32(0);
                        teacherModel.Name = reader.GetString(1);
                        teacherModel.Password = reader.GetString(2);
                        teacherModel.Email = reader.GetString(3);
                        teacherModel.DateStartedWorking = reader.GetDateTime(4);
                        

                    }
                }
            }
            return teacherModel;

        }


        public TeacherModel GetTeacherByEmail(int Id)
        {
            TeacherModel teacherModel = null;
            using (var connection = new SqlConnection(this.ConnectionString))
            {


                connection.Open();
                SqlCommand allCommand = new SqlCommand("SELECT * FROM Teachers WHERE Id =" + Id.ToString(), connection);



                using (var reader = allCommand.ExecuteReader())
                {

                    //Read ROW BY ROW
                    while (reader.Read())
                    {
                        teacherModel = new TeacherModel();
                        teacherModel.Id = reader.GetInt32(0);
                        teacherModel.Name = reader.GetString(1);
                        teacherModel.Password = reader.GetString(2);
                        teacherModel.Email = reader.GetString(3);
                        teacherModel.DateStartedWorking = reader.GetDateTime(4);


                    }
                }
            }
            return teacherModel;
        }
        public bool UpdateTeacher(TeacherModel teacherToUpdate)
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
                    string updateQuery = "UPDATE Teachers SET " +
                                          "Name = @Name, " +
                                          "Password = @Password,"+
                                          "DateStartedWorking = @DateStartedWorking, " +
                                          "WHERE Id = @Id";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Id", teacherToUpdate.Id);
                    updateCommand.Parameters.AddWithValue("@Name", teacherToUpdate.Name);
                    updateCommand.Parameters.AddWithValue("@Password", teacherToUpdate.Password);
                    updateCommand.Parameters.AddWithValue("@Email", teacherToUpdate.Email);
                    updateCommand.Parameters.AddWithValue("@DateStartedWorking", teacherToUpdate.DateStartedWorking);



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
