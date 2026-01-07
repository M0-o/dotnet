using LibrarySystem.Application.Interfaces;
using LibrarySystem.Infrastructure.Data;
using LibrarySystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibrarySystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("LibrarySystem.Infrastructure")));
        
        // Register services
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICartService, CartService>();
        
        return services;
    }
}
