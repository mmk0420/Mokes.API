namespace Mokes.API.Repositories.User
{
    public interface IUserRepository
    {
        Task<Models.User?> GetByIdAsync(Guid id);
        Task<Models.User?> GetByUsernameAsync(string username);
        Task AddAsync(Models.User user);
        Task RemoveAsync(Models.User user);
        Task UpdateAsync(Models.User user);
    }
}
