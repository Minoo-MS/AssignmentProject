using System.Collections.Generic;
using AssignmentProject.Core.Models;
using Microsoft.AspNetCore.Http;

namespace AssignmentProject.Core
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        void UploadProducts(IFormFile file);
    }
}