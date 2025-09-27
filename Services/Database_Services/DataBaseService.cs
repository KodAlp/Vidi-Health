using Microsoft.EntityFrameworkCore;
using Vidi_Health.Models;

namespace Vidi_Health.Services.Database_Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly DietContext _context;

        public DatabaseService(DietContext context)
        {
            _context = context;
        }

        public async Task InitializeDatabaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }

        // User Operations
        public async Task<User> CreateUserAsync(User user)
        {
            if (!await ValidateDataAsync(user))
                throw new ArgumentException("Invalid user data");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.measurements)
                .Include(u => u.bodyCompositions)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Validation method
        public async Task<bool> ValidateDataAsync<T>(T entity)
        {
            if (entity is User user)
            {
                return !string.IsNullOrEmpty(user.name) &&
                       user.dateOfBirth != default(DateTime) &&
                       user.dateOfBirth < DateTime.Now;
            }
            if (entity is Measurements measurement)
            {
                return measurement.Height > 0 && measurement.Weight > 0;
            }
            return true;
        }

        // User Operations - devam
        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Measurement Operations
        public async Task<Measurements> AddMeasurementAsync(Measurements measurement)
        {
            if (!await ValidateDataAsync(measurement))
                throw new ArgumentException("Invalid measurement data");

            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();
            return measurement;
        }

        public async Task<List<Measurements>> GetUserMeasurementsAsync(int userId)
        {
            return await _context.Measurements
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.MeasuredAt)
                .ToListAsync();
        }

        public async Task<Measurements> GetLatestMeasurementAsync(int userId)
        {
            return await _context.Measurements
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.MeasuredAt)
                .FirstOrDefaultAsync();
        }
        // BodyComposition Operations
        public async Task<BodyCompositions> SaveBodyCompositionAsync(BodyCompositions bodyComp)
        {
            _context.BodyCompositions.Add(bodyComp);
            await _context.SaveChangesAsync();
            return bodyComp;
        }

        public async Task<List<BodyCompositions>> GetUserBodyCompositionsAsync(int userId)
        {
            return await _context.BodyCompositions
                .Where(bc => bc.MeasurementId == userId)
                .OrderByDescending(bc => bc.CalculatedAt)
                .ToListAsync();
        }

        // 4. latestRecord değişkenini kullan
        public async Task<Measurements> GetLatestMeasurementAsync(int userId)
        {
            // latestRecord ile en son ölçümü getir
        }

        // 5. filteredMeasurements değişkenini kullan
        public async Task<List<Measurements>> GetMeasurementsByTypeAsync(int userId, MeasurementType type)
        {
            // filteredMeasurements ile type'a göre filtrele
        }

        // 6. savedComposition değişkenini kullan
        public async Task<BodyCompositions> SaveBodyCompositionAsync(BodyCompositions bodyComp)
        {
            // savedComposition ile kaydetme işlemi yap
        }

        // 7. entityValidation değişkenini kullan
        public async Task<User> UpdateUserAsync(User user)
        {
            // entityValidation ile user null kontrolü yap
        }

        // 8. asyncOperation değişkenini kullan
        public async Task<User> CreateUserAsync(User user)
        {
            // asyncOperation ile Add işlemini sakla
        }

        // 9. queryResult değişkenini kullan
        public async Task<User> FindUserByNameAsync(string name)
        {
            // queryResult ile isim araması yap
        }
    }
}
