using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using RhodeIT.Classes;
using RhodeIT.Models;

namespace RhodeIT.Databases
{
    /// <summary>
    /// @Dev database emulates the rhodes university database which has details of student logins
    /// </summary>
    public class RhodesDataBase
    {
        private string connectionString = "Server=146.231.123.144; Port=5432; User Id=admin; Password=1234; Database=RhodesDB; Timeout=30;";
        
        public RhodesDataBase()
        {
        }

        public void StoreStudents(List<Student> students)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                foreach (Student student in students)
                {
                    NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Students (StudentNumber,Password) Values ("+ student.StudentNo+","+student.Password+");", connection);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                }
                connection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async Task<bool> VerifyStudentAysnc(LoginDetails details)
        {
            bool found = false;
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand("select * from students where studentnumber="+"'"+details.userID+ "'" + ";", connection);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    var stdNumber = dataReader[1].ToString();
                    var password = dataReader[0].ToString(); 
                    found = stdNumber==details.userID && password==details.password;
                    if (!found)
                    {
                        throw new LoginException("Invalid login credentials please ensure they match the ones you use to access ross or runconnected");
                    }
                    connection.Close();
                }


            return await Task.FromResult(found);
        }
    }
}
