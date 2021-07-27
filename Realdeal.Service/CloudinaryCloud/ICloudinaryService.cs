using Microsoft.AspNetCore.Http;

namespace Realdeal.Service.CloudinaryCloud
{
    public interface ICloudinaryService
    {
        public string UploadPhoto(IFormFile file,string folder);
    }
}
