using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssignmentProject.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AssignmentProject.Core.Services
{
    public class ProductService : IProductService
    {
        private IProductsRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _environment;

        public ProductService(
            IProductsRepository repository,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment environment
        )
        {
            this._repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        public void UploadProducts(IFormFile file)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "UploadedFiles");
            var fullPath = Path.Combine(uploads, GetUniqueFileName(file.FileName));

            FileSaveExtension.SaveAs(file, fullPath);
            var dataTable = CsvToDataTableService.CsvToDataTable(fullPath);
            CsvToDataTableService.InsertDataIntoSQLServerUsingSQLBulkCopy(dataTable);
        }

        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

    }
}
