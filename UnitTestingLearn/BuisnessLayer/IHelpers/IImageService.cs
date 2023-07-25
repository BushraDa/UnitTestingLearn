namespace ECommerceAPI.BusinessLayer.IHelpers
{
    public interface IImageService
    {
        string SaveImage(IFormFile file, int Id, string BaseFolder, string SubFolder);
        void DeleteImage(IFormFile file);
    }
}
