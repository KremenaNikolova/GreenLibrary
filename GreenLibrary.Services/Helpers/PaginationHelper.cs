namespace GreenLibrary.Services.Helpers
{
    using Microsoft.EntityFrameworkCore;

    public static class PaginationHelper
    {
        public static async Task<(List<T>, PaginationMetadata)> CreatePaginatedResponseAsync<T>(IQueryable<T> query, int currentPage, int pageSize)
        {
            var totalRecords = query.Count();
            var items = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            var paginationMetadata = new PaginationMetadata(totalRecords, pageSize, currentPage);

            return (items, paginationMetadata);
        }
    }
}
