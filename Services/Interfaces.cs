using Vidi_Health.Models;

namespace Vidi_Health.Services
{
    //Interface Classları Birleştirildi, Tek bir dosya üzerinden kontrol yapılacak, arayüz kullanılmasıınnı sebeb, olası bir sql 
    //değişikliğine gidilirse isimlerin aynı kalmasıdır.
    internal class Interfaces
    {
        public interface IBmrCalculatorService 
        {
            float CalculateBMR(App_User_Features.The_User user);
            float CalculateBMRWithBodyFat(App_User_Features.The_User user, float bodyFat);
            float CalculateTDEE(float bmr, App_User_Features.The_User user);
            float CalculateCaloricNeeds(App_User_Features.The_User user, float bodyfat_percentage);
        }

        public interface IBodyFatCalculatorService
        {
            public float CalculateBodyFat(App_User_Features.The_User user);
            float CalculateNavyFormula(App_User_Features.The_User user);
            float CalculateJacksonPollock3(App_User_Features.The_User user);
            float CalculateJacksonPollock7(App_User_Features.The_User user);
        }
        public interface IDataBaseService 
        {
            // User operations
            Task<App_User_Features.The_User> CreateUserAsync(App_User_Features.The_User user);
            Task<App_User_Features.The_User?> GetUserByIdAsync(int userId);
            Task<List<App_User_Features.The_User>> GetAllUsersAsync();
            Task<App_User_Features.The_User> UpdateUserAsync(App_User_Features.The_User user);
            Task<bool> DeleteUserAsync(int userId);

            // Measurement operations
            Task<App_User_Features.The_Measurements> AddMeasurementAsync(App_User_Features.The_Measurements measurements);
            Task<List<App_User_Features.The_Measurements>> GetUserMeasurementsAsync(int userId);
            Task<App_User_Features.The_Measurements?> GetLatestMeasurementAsync(int userId);

            // BodyComposition operations
            Task<App_User_Features.The_Complex_Measurements> SaveComplexAsync(App_User_Features.The_Complex_Measurements complex);
            Task<List<App_User_Features.The_Complex_Measurements>> GetUserComplexAsync(int userId);

            // Database operations
            Task InitializeDatabaseAsync();
            Task<bool> ValidateDataAsync<T>(T entity);

        }

    }
}
