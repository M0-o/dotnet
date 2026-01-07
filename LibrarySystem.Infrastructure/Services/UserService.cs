using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public ApplicationUser? Authenticate(string email, string password)
    {
        return _context.Users.FirstOrDefault(u => 
            u.Email.ToLower() == email.ToLower() && 
            u.Password == password);
    }

    public ApplicationUser? GetUserById(int id)
    {
        return _context.Users.Find(id);
    }

    public void UpdateUserProfile(int userId, string fullName, string? profilePictureUrl)
    {
        var user = _context.Users.Find(userId);
        if (user != null)
        {
            user.FullName = fullName;
            if (!string.IsNullOrEmpty(profilePictureUrl))
                user.ProfilePictureUrl = profilePictureUrl;
            
            _context.SaveChanges();
        }
    }
}
