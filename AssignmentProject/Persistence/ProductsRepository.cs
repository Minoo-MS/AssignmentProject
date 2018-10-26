using System.Linq;
using AssignmentProject.Core;
using AssignmentProject.Core.Models;

namespace AssignmentProject.Persistence
{
    public class ProductsRepository : IProductsRepository
    {
        private AppDbContext db;

        public ProductsRepository(AppDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Product> GetAll()
        {
            return db.Set<Product>();

        }
    }
}
