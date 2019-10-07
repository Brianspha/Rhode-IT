using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using RhodeIT.Classes;
using RhodeIT.Helpers;
using RhodeIT.Models;

namespace RhodeIT.Databases
{
    /// <summary>
    /// @Dev database emulates the rhodes university database which has details of student logins
    /// </summary>
    public class RhodesDataBase
    {

        public RhodesDataBase()
        {
        }

        public void StoreStudents(List<Student> students)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(Variables.connectionStringRhodesDB);
                connection.Open();
                foreach (Student student in students)
                {
                    NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Students (StudentNumber,Password) Values (" + student.StudentNo + "," + student.Password + ");", connection);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async Task<Tuple<bool, LoginDetails>> VerifyStudentAysnc(LoginDetails details)
        {
            bool found = false;
            using (NpgsqlConnection connection = new NpgsqlConnection(Variables.connectionStringRhodesDB))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("select * from student_staff where student_staff_id=" + "'" + details.User_ID + "'" + ";", connection);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                var user_id = dataReader[2].ToString();
                var password = dataReader[1].ToString();
                var eth_address = dataReader[3].ToString();
                details.Ethereum_Address = eth_address;
                found = user_id == details.User_ID && password == details.Password;
                if (!found)
                {
                    throw new LoginException("Invalid login credentials please ensure they match the ones you use to access ross or ruconnected");
                }
                connection.Close();
                command.Dispose();
                dataReader.Close();
            }
            return await Task.FromResult(new Tuple<bool,LoginDetails>(found,details));
        }
        public async Task<string> GetUserEthAddress(string user_id)
        {
            string eth_address = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(Variables.connectionStringRhodesDB))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("select * from student_staff where student_staff_id=" + "'" + user_id + "'" + ";", connection);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                dataReader.Read();
                eth_address = dataReader[3].ToString();
            }
            return await Task.FromResult(eth_address);
        }
    }
}
