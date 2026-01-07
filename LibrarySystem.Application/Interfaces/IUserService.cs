using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Interfaces;

public interface IUserService
{
    ApplicationUser? Authenticate(string email, string password);
    ApplicationUser? GetUserById(int id);
    void UpdateUserProfile(int userId, string fullName, string? profilePictureUrl);
}
