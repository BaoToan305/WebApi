using CreateApiProject.Models;
using Dapper;
using System.Data;

namespace CreateApiProject.Repositories
{
    public class EmployeeRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(DapperContext dapperContext, ILogger<EmployeeRepository> logger)
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> SpGetAllEmployee()
        {
            const string sp = "sp_get_all_emp";
            using IDbConnection connection = _dapperContext.CreateConnection();
            var emp = await connection.QueryAsync<Employee>(sp, commandType: CommandType.StoredProcedure);
            return emp;
        }
    }
}
