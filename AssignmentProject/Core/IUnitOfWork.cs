using System.Threading.Tasks;

namespace AssignmentProject.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}