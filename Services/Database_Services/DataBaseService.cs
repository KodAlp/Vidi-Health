using Microsoft.Data.Sqlite;
using Vidi_Health.Models;

namespace Vidi_Health.Services.Database_Services
{
    public class App_DataBase_Configurations : Interfaces.IDataBaseService
    {
        string connectionString = "Data Source = User_Datas.db";

        public async Task CreateUserAsync(string Name, string Mail, string password)
        {
            try
            {

                using (var connection = new SqliteConnection(connectionString))
                {

                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                            @"INSERT INTO Users (Name,Mail,Password) VALUES (@Name, @Mail, @Password)";

                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Mail", Mail);
                        command.Parameters.AddWithValue("@Password", password);

                        await command.ExecuteNonQueryAsync();

                    }
                    connection.Close();
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Hata Oluştu"+ ex.Message);
            }
        }
        
        //Spesifik özellikleri ekrana getirmek üzere aslında düzeltilebilir
        public async Task<App_User_Features.The_User?> GetUserByIdAsync(int Id)
        {
            try
            {
                App_User_Features.The_User user = new();

                int user_Id = Id;
                using (var connection2 = new SqliteConnection(connectionString))
                {
                   await connection2.OpenAsync();
                   string Query = "SELECT * FROM User_Id";

                    using (var command = new SqliteCommand())
                    {
                        command.CommandText = Query;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (Id == reader.GetInt32(0))
                                {
                                    //complexmeasure'ı alacak
                                    //personal infoyu alacak
                                    //measurementları alacak
                                    user.Name = reader.GetString(1);

                                }
                                else
                                {
                                    return null;
                                }
                            }
                            reader.Close();
                        } 
                    }
                    connection2.Close();
                    return user; 
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }        
       
        }


        //yarın tamamlanacak

        //float value = hesaplanan şey , operation neyin yapıldığı(boy hesaplama, kilo hesaplama vs.)
        Task<App_User_Features.The_User> UpdateUserAsync(App_User_Features.The_User user, float value, int operation);
        Task<bool> DeleteUserAsync(int userId);

        // Measurement operations
        Task<App_User_Features.The_Measurements> AddMeasurementAsync(App_User_Features.The_User user, int operationnumber);
        Task<List<App_User_Features.The_Measurements>> Get_User_All_Info_Async(int userId);

        //measurement id nin kullanılacağı task
        Task<App_User_Features.The_Measurements?> GetLatestMeasurementAsync(int userId, int measuringdate);

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
