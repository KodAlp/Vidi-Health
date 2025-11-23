using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Vidi_Health.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Vidi_Health.Services.Database_Services
{
    public class DataBaseService(DietContext _context) : IDataBaseService
    {
        private string connectionstring = "Data Soruce = verilerin.db";
        public async Task InitializeDatabaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }

        //It will have information about User,Id,Mail,
        
        /*Measurements, Kilo , Boy, Etnik köken, Cinsiyet
        NeckCircumference ,WaistCircumference , HipCircumference
        ChestSkinfold, AbdominalSkinfold,ThighSkinfold
        TricepsSkinfold,SuprailiacSkinfold,MidaxillarySkinfold
        SubscapularSkinfold,MeasuredAt*/
        
        /*BodyCompositions Bf Percentage
         LeanBodyMass, FatMass , BMR, TDEE
        Measurement Id*/
        public void CreateTable_ForUser() 
        {
            using (var connection = new SqliteConnection(connectionstring))
            {
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS USER(
                ID Integer PRIMARY KEY AUTOINCREMENT,
                NAME TEXT NOT NULL,
                GENDER TEXT NOT NULL,
                ETHNICITY TEXT NOT NULL,
                BIRTHATE TEXT NOT NULL)";


                string createTableQuery2 = @"
                CREATE TABLE IF NOT EXISTS MEASUREMENTS(
                ID Integer PRIMARY KEY AUTOINCREMENT
                Weight Integer KEY  NULL



                )";
                connection.Open();
                


                using (var command1 = connection.CreateCommand())
                {
                    command1.CommandText = createTableQuery;
                    command1.ExecuteNonQuery();
                }
            }
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

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Measurements)
                .Include(u => u.BodyCompositions)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Validation method
        public Task<bool> ValidateDataAsync<T>(T entity)
        {
            if (entity is User user)
            {
                return Task.FromResult(
                    !string.IsNullOrEmpty(user.Name) &&
                    user.DateOfBirth != default(DateTime) &&
                    user.DateOfBirth < DateTime.Now
                );
            }
            if (entity is Measurements measurement)
            {
                return Task.FromResult(measurement.Height > 0 && measurement.Weight > 0);
            }
            return Task.FromResult(true);
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

        public async Task<Measurements?> GetLatestMeasurementAsync(int userId)
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
            // Önce kullanıcının measurement ID'lerini bul
            var measurementIds = await _context.Measurements
                .Where(m => m.UserId == userId)
                .Select(m => m.Id)
                .ToListAsync();

            // Sonra bu measurement'lara ait body composition'ları getir
            return await _context.BodyCompositions
                .Where(bc => measurementIds.Contains(bc.MeasurementId))
                .OrderByDescending(bc => bc.CalculatedAt)
                .ToListAsync();
        }


        // 5. filteredMeasurements değişkenini kullan
        public async Task<List<Measurements>> GetMeasurementsByTypeAsync(int userId, MeasurementType type)
        {
            // Adım 1: UserId kontrolü
            if (userId <= 0)
                throw new ArgumentException("Geçersiz user ID");

            // Adım 2: measurementQuery - tüm user ölçümlerini getir
            var measurementQuery = _context.Measurements
                .Where(m => m.UserId == userId);

            // Adım 3: filteredMeasurements - type'a göre filtrele
            List<Measurements> filteredMeasurements = await measurementQuery
                .Where(m => m.Type == type)
                .OrderByDescending(m => m.MeasuredAt)
                .ToListAsync();

            // Adım 4: Sonuç kontrolü
            if (filteredMeasurements.Count == 0)
                throw new ArgumentException($"Bu user için {type} tipinde ölçüm bulunamadı");

            return filteredMeasurements;
        }


        // 9. queryResult değişkenini kullan
        public async Task<User?> FindUserByNameAsync(string name)
        {
            // Adım 1: İsim kontrolü
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("İsim boş olamaz");

            // Adım 2: queryResult - veritabanında isim araması
            User? queryResult = await _context.Users
                .Where(u => u.Name == name)
                .FirstOrDefaultAsync() ?? throw new ArgumentException($"{name} isimli kullanıcı bulunamadı");

            // Adım 4: Kullanıcıyı döndür
            return queryResult;
        }
    }
}
