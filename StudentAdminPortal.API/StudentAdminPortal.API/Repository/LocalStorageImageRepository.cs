using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories
{
    public class ImageRepository
    {
        public string Upload(IFormFile file, string fileName)
        {
            var filePath= Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return GetServerRelative(fileName);
        }

        private string GetServerRelative(string fileName)
        {
            return Path.Combine(@"Resources\Images", fileName);
        }
    }
}
