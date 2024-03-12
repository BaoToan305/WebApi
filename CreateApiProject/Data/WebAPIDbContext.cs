using Microsoft.EntityFrameworkCore;

namespace CreateApiProject.Data
{
    public class WebAPIDbContext : DbContext
    {
        public WebAPIDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) 
        {
        
        }
    }
}
