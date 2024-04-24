namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Server.Dtos.Category;

    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetAllCategoriesAsync();
    }
}
