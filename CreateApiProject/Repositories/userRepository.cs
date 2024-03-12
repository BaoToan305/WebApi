

using CreateApiProject.Models;
using System.Data;
using Dapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CreateApiProject.Repositories
{
    public class userRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger<userRepository> _logger;

        public userRepository(DapperContext dapperContext, ILogger<userRepository> logger)
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }

        public async Task<int> AddUser(User user)
        {
            const string sql = "sp_insert_user";
            using IDbConnection connection = _dapperContext.CreateConnection();
            int rowAffected = await connection.ExecuteAsync(sql, user ,commandType: CommandType.StoredProcedure);
            return rowAffected;
        }

        public async Task<IEnumerable<User>> GetUser()
        {
            const string sql = "sp_get_user";
            using IDbConnection connection = _dapperContext.CreateConnection();
            var users = await connection.QueryAsync<User>(sql, commandType: CommandType.StoredProcedure);
            return users;
        }
    }
}
