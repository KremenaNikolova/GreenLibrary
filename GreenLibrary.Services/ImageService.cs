namespace GreenLibrary.Services
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Http;
    
    using GreenLibrary.Data;
    using GreenLibrary.Services.Interfaces;

    public class ImageService : IImageService
    {
        private readonly GreenLibraryDbContext dbContext;

        public ImageService(GreenLibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var fileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine("wwwroot/Images", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
