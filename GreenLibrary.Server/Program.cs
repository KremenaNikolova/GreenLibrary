
namespace GreenLibrary.Server
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using GreenLibrary.Data;
    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Services;
    using GreenLibrary.Data.Entities;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromSeconds(30);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });

            builder.Services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddXmlDataContractSerializerFormatters();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<GreenLibraryDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            builder.Services.AddAuthentication();
            builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 5;
            })
            .AddEntityFrameworkStores<GreenLibraryDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IImageService, ImageService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("https://localhost:5173") //this is the port where the client start
                    .WithExposedHeaders("pagination");

                });
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseSession();
            app.UseStaticFiles(); // Enable serving static files from wwwroot
            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
