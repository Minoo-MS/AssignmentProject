using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssignmentProject.Core.Services
{
    public static class FileSaveExtension
    {
        public static async Task SaveAsAsync( IFormFile formFile, string filePath)
        {
            await formFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

        }

        public static void SaveAs( IFormFile formFile, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
        }
    }
}
