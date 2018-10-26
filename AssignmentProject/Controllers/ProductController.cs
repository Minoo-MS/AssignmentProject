using System.Collections.Generic;
using AssignmentProject.Core;
using AssignmentProject.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AssignmentProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _service;
        private readonly IHostingEnvironment _environment;
        private readonly FileSettings fileSettings;

        public ProductController(
            IProductService service,
            IHostingEnvironment hostingEnvironment, IOptionsSnapshot<FileSettings> options)
        {
            _service = service;
            _environment = hostingEnvironment;
            this.fileSettings = options.Value;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _service.GetAll();
        }


        [HttpPost("UploadFiles")]
        public IActionResult Post(IFormFile file)
        {
            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > fileSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (!fileSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");
            _service.UploadProducts(file);
            return Ok("File Upload is Successful");
        }
    }
}