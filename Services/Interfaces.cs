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
            //DataBaseControl
            Task <bool> Ensure_Database_Is_Valid(string databaseName);
            // User operations
            Task CreateUserAsync(string Name , string Mail, string password);
            Task<App_User_Features.The_User?> GetUserByIdAsync(int userId);
            //float value = hesaplanan şey , operation neyin yapıldığı(boy hesaplama, kilo hesaplama vs.)
            Task<App_User_Features.The_User> UpdateUserAsync(App_User_Features.The_User user,float value ,int operation );
            Task<bool> DeleteUserAsync(int userId);

            // Measurement operations
            Task<App_User_Features.The_Measurements> AddMeasurementAsync(App_User_Features.The_User user, int operationnumber);
            Task<List<App_User_Features.The_Measurements>> Get_User_All_Info_Async(int userId);
            
            //measurement id nin kullanılacağı task
            Task<App_User_Features.The_Measurements?> GetLatestMeasurementAsync(int userId,int measuringdate);

            // BodyComposition operations
            Task<App_User_Features.The_Complex_Measurements> SaveComplexAsync(App_User_Features.The_User user, int operationnumber);
        
            // Database operations
            Task InitializeDatabaseAsync();
            Task<bool> ValidateDataAsync<T>(T entity);

            /*Ardanın C frameworkü alınınca yapılacak
            Task<bool> Encrypt_Data_For_Safety();
            Task<bool> Crypt_Data_For_User();*/
        }

    }
}
