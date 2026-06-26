using EmailSchedulerApp.Models;
namespace EmailSchedulerApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByUsername(string? Email);
    }
}