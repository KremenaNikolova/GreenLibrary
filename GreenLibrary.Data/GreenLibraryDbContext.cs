namespace GreenLibrary.Data
{
    using System.Reflection;
    
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    using GreenLibrary.Data.Entities;
    using Microsoft.AspNetCore.Identity;

    public class GreenLibraryDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public GreenLibraryDbContext(DbContextOptions<GreenLibraryDbContext> options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; } = null!;

        public DbSet<ArticleLike> ArticlesLikes { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Tag> Tags { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            var configAssembly = Assembly.GetAssembly(typeof(GreenLibraryDbContext));
            Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly!);

            base.OnModelCreating(builder);
        }

    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GreenLibraryDbContext>
    {
        public GreenLibraryDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../GreenLibrary.Server/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<GreenLibraryDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new GreenLibraryDbContext(builder.Options);
        }
    }
}
