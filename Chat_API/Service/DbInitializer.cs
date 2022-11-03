using Chat_API.Service.IService;
using Chat_DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat_API.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;

        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
