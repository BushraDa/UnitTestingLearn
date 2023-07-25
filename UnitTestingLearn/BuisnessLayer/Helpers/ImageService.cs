using ECommerceAPI.BusinessLayer.IHelpers;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ECommerceAPI.BusinessLayer.Helpers
{
    public class ImageService : IImageService
    {
        protected readonly IHostingEnvironment _hostingEnvironment;

        public ImageService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void DeleteImage(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public string SaveImage(IFormFile file, int Id, string BaseFolder, string SubFolder)
        {
            var ext = Path.GetExtension(file.FileName);
            var filename = Id + ext;

            var MainPath = Path.Combine(_hostingEnvironment.ContentRootPath, BaseFolder, SubFolder);
            if (!Directory.Exists(MainPath))
                Directory.CreateDirectory(MainPath);
            var FullPath = Path.Combine(MainPath, filename);
            using (var stream = System.IO.File.Create(FullPath))
            {
                file.CopyToAsync(stream);
            }

            return filename;
        }
    }
}
