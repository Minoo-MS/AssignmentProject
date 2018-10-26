using System.Threading.Tasks;
using AssignmentProject.Core;

namespace AssignmentProject.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext db;

        public UnitOfWork(AppDbContext db)
        {
            this.db = db;
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
