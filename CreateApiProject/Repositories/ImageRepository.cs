using CreateApiProject.Models;
using Dapper;
using System.Data;

namespace CreateApiProject.Repositories
{
    public class ImageRepository
    {
        public readonly DapperContext _dapperContext;
        public readonly ILogger<ImageRepository> _logger;
        public ImageRepository(DapperContext dapperContext, ILogger<ImageRepository> logger) 
        {
            _dapperContext = dapperContext;
            _logger = logger;
        }

        public async Task<int> UploadImageAsync(ImageBody image)
        {
            const string sql = "sp_instert_image";
            using IDbConnection connection = _dapperContext.CreateConnection();
            int row = await connection.ExecuteAsync(sql, image, commandType: CommandType.StoredProcedure);
            return row;
        }
    }
}
