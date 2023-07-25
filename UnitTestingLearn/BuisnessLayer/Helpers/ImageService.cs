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

        public string SaveImage(IFormFile file)
        {
            var filename = file.FileName;

            var MainPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Images//");
            if (!Directory.Exists(MainPath))
                Directory.CreateDirectory(MainPath);

            var FullPath = Path.Combine(MainPath, filename);
            using (var stream = System.IO.File.Create(FullPath))
            {
                file.CopyToAsync(stream);
            }

            return "Images//" + filename;
        }
    }
}
