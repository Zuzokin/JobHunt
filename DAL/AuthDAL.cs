using JobHunt.DAL.Models;
using Dapper;
using Npgsql;

namespace JobHunt.DAL
{
    public class AuthDAL : IAuthDAL
    {
        public async Task<UserModel> GetUser(string email)
        {
            using var connection = new NpgsqlConnection(DBHelper.ConnString);
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                    select UserId, Email, Password, Salt, Status 
                    from AppUser
                    where Email = @email", new { email }) ?? new UserModel();
        }
        public async Task<UserModel> GetUser(int id)
        {
            using var connection = new NpgsqlConnection(DBHelper.ConnString);
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                    select UserId, Email, Password, Salt, Status 
                    from AppUser
                    where UserId = @id", new { id }) ?? new UserModel();
        }

        public async Task<int> CreateUser(UserModel model)
        {
            using var connection = new NpgsqlConnection(DBHelper.ConnString);
            connection.Open();

            string sql = @"
                    insert into AppUser(Email, Password, Salt, Status)
                    values(@Email, @Password, @Salt, @Status);
                    SELECT currval(pg_get_serial_sequence('AppUser','userid'));
                    ";

            return await connection.QuerySingleAsync<int>(sql, model);
        }
    }
}
