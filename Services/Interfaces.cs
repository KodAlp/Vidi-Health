using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidi_Health.Models;

namespace Vidi_Health.Services
{
    //Interface Classları Birleştirildi, Tek bir dosya üzerinden kontrol yapılacak, aayüz kullanılmasıınnı sebeb, olası bir sql 
    //değişikliğine gidilirse isimlerin aynı kalmasıdır.
    internal class Interfaces
    {
        public interface IBmrCalculatorService 
        {
            double CalculateBMR(User user, Measurements measurement);
            double CalculateBMRWithBodyFat(Measurements measurement, double bodyFat);
            double CalculateTDEE(double bmr, ActivityLevel activityLevel);
            double CalculateCaloricNeeds(Measurements measurement, User user, double bodyfat_percentage);
        }

        public interface IBodyFatCalculatorService
        {
            public double CalculateBodyFat(Measurements measurement, User user);
            double CalculateNavyFormula(Measurements measurement, User user);
            double CalculateJacksonPollock3(Measurements measurement, User user);
            double CalculateJacksonPollock7(Measurements measurement, User user);
        }
        public interface IDataBaseService 
        {
            // User operations
            Task<User> CreateUserAsync(User user);
            Task<User?> GetUserByIdAsync(int userId);
            Task<List<User>> GetAllUsersAsync();
            Task<User> UpdateUserAsync(User user);
            Task<bool> DeleteUserAsync(int userId);

            // Measurement operations
            Task<Measurements> AddMeasurementAsync(Measurements measurement);
            Task<List<Measurements>> GetUserMeasurementsAsync(int userId);
            Task<Measurements?> GetLatestMeasurementAsync(int userId);

            // BodyComposition operations
            Task<BodyCompositions> SaveBodyCompositionAsync(BodyCompositions bodyComp);
            Task<List<BodyCompositions>> GetUserBodyCompositionsAsync(int userId);

            // Database operations
            Task InitializeDatabaseAsync();
            Task<bool> ValidateDataAsync<T>(T entity);

        }

    }
}
