using System.Linq;
using AssignmentProject.Core.Models;

namespace AssignmentProject.Core
{
    public interface IProductsRepository
    {
        IQueryable<Product> GetAll();
    }
}