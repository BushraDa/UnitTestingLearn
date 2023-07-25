namespace ECommerceAPI.BusinessLayer.IHelpers
{
    public interface IImageService
    {
        string SaveImage(IFormFile file);
        void DeleteImage(IFormFile file);
    }
}
