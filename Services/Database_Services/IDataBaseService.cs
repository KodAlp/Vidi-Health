using Vidi_Health.Models;

namespace Vidi_Health.Services.Database_Services
{
    public interface IDataBaseService
    {

        // User operations
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);

        // Measurement operations
        Task<Measurements> AddMeasurementAsync(Measurements measurement);
        Task<List<Measurements>> GetUserMeasurementsAsync(int userId);
        Task<Measurements> GetLatestMeasurementAsync(int userId);

        // BodyComposition operations
        Task<BodyCompositions> SaveBodyCompositionAsync(BodyCompositions bodyComp);
        Task<List<BodyCompositions>> GetUserBodyCompositionsAsync(int userId);

        // Database operations
        Task InitializeDatabaseAsync();
        Task<bool> ValidateDataAsync<T>(T entity);
    }
}
