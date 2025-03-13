using File_Manager_MEDGEN.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;


namespace File_Manager_MEDGEN.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }


        //public bool AuthenticateUser(NetworkCredential credential)
        //{
        //    bool validUser;
        //    using (var connection = GetConnection())
        //    using (var command = new SqlCommand())
        //    {
        //        connection.Open();
        //        command.Connection = connection;
        //        command.CommandText = "SELECT * FROM [User] WHERE username=@username AND [password]=@password";

        //        command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
        //        command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
        //        validUser = command.ExecuteScalar() == null ? false : true;
        //    }
        //    return validUser;
        //}

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(1) FROM [User] WHERE Username=@username AND [Password]=@password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = (int)command.ExecuteScalar() > 0;
            }
            return validUser;
        }


        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
       



        public bool ResetPassword(string username, string newPassword)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [User] SET [Password] = @newPassword WHERE Username = @username";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@newPassword", newPassword);  // You can hash this password if needed

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;  // Return true if the update was successful
            }
        }

    }
}