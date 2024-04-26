namespace GreenLibrary.Services.Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
    }
}
