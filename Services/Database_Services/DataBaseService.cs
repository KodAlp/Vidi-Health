using Microsoft.Data.Sqlite;
using Vidi_Health.Models;

namespace Vidi_Health.Services.Database_Services
{
    public class App_DataBase_Configurations : Interfaces.IDataBaseService
    {

        string connectionString = "Data Source = User_Datas.db";
        public void Create_The_Tables() 
        {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    var cmd1 = connection.CreateCommand();
                    cmd1.CommandText = @"CREATE TABLE IF NOT EXISTS  Users
                    (
                     Id INTEGER PRIMARY KEY AUTOINCREMENT,
                     Name TEXT NOT NULL,
                     Mail TEXT UNIQUE,
                     Password TEXT UNIQUE,
                     CreationTime DATETIME)";

                    var cmd2 = connection.CreateCommand();

                    cmd2.CommandText = @"CREATE TABLE IF NOT EXISTS Measurements
                    (
                    Measurement_id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    Measurement_time  DATETIME KEY
                    Weight REAL KEY, 
                    Height REAL KEY,
                    Waist REAL KEY,
                    Neck  REAL KEY,
                    Hip REAL KEY,
                    Chest REAL KEY,
                    Abdominal REAL KEY,
                    Thigh REAL KEY,
                    Triceps REAL KEY,
                    Suprailiac REAL KEY,
                    Subskapular REAL KEY,
                    Mixodilary REAL KEY)";
            
                    var cmd3 = connection.CreateCommand();

                    cmd3.CommandText = @"CREATE TABLE IF NOT EXISTS Complex_Measurements
                    (
                    Measurement_id2 INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    Measurement_time  DATETIME KEY,
                    BodyFat REAL KEY,
                    LeanBodyMass REAL KEY,
                    FatMass REAL KEY,
                    Bmr REAL KEY,
                    TDEE REAL KEY,
                    Measurement_type STRING KEY,
                    )";
                
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();

                     connection.Close();
                }



        }


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
    
        public enum Operations
        {
            Name = 1,
            Mail = 2,
            Password = 3,
            AccountCreation = 4,
            user_measurement = 5,
            user_complex = 6,
            user_info = 7,
        }


        public enum MeasureOperations
        {
            Weight_Change = 0,
            Height_Change = 1,
            Waist_Measure_Change = 2,
            Neck_measure_Change = 3,
            Hip_measure_Change = 4,
            SkinFold_Chest_Change = 5,
            SkinFold_Abdominal_Change = 6,
            SkinFold_Thigh_Change = 7,
            SkinFold_Triceps_Change = 8,
            Skinfold_Suprailiac_Change = 9,
            Skinfold_Subscapular_Change = 10,
            Skinfold_Mixadilary_Change = 11
        }

        public enum Complex_MeasureOperations
        {
            Body_Fat_Calculation = 0,
            LeanBodyMass_Calculation = 1,
            Fat_Mass_Calculation = 2,
            BMR_Calculation = 3,
            TDEE_Calculation = 4,
        }

        public enum User_Info_Operations
        {

            Date_Of_Birth_Change = 0,
            Age_Change = 1,
            Ethnicity_Change = 2,
            Gender_Change = 3,
            Activity_Level_Change = 4,
        }

        //User veritabanına yazılacak Kişi nesnesi, main_operasyon ilk işlem,
        //alt operasyon ise içinde çağrılan fonkda kullanılcak işlem,
        //value ise her eğeri alabilmesi için object
        public  App_User_Features.The_User UpdateUser(App_User_Features.The_User user,int main_operation,int alt_operation ,object value)
        {
            switch (main_operation) 
            {
                //bunlara fonkları zamanla eklenecek
                case (int)Operations.Name:
                    break;
                case (int)Operations.Mail:
                    break;
                case (int)Operations.Password:
                    break;
                case (int)Operations.AccountCreation:
                    break;

                case (int)Operations.user_measurement:
                    user = Update_The_measurement(user, alt_operation,value);
                    break;
                case (int)Operations.user_complex:
                    user = Update_Complex_Measurements(user, alt_operation, value);
                    break;
                case (int)Operations.user_info:
                    user = Update_User_Info(user, alt_operation,value);
                    break;
            }
            return user;
        }


        //Update user içinde kullanılan fonklar
        public App_User_Features.The_User Update_The_measurement(App_User_Features.The_User user, int operation, Object value)
        {
            float value_ = (float)value;
            switch (operation)
            {

                case (int)MeasureOperations.Height_Change:
                    user.user_measurement.Height = value_;
                    break;

                case (int)MeasureOperations.Weight_Change:
                    user.user_measurement.Weight = value_;
                    break;

                case (int)MeasureOperations.Hip_measure_Change:
                    user.user_measurement.Hip_Measure = value_;
                    break;

                case (int)MeasureOperations.Waist_Measure_Change:
                    user.user_measurement.Waist_Measure = value_;
                    break;

                case (int)MeasureOperations.Neck_measure_Change:
                    user.user_measurement.Neck_Measure = value_;
                    break;


                case (int)MeasureOperations.SkinFold_Chest_Change:
                    user.user_measurement.SkinfoldChest = value_;
                    break;

                case (int)MeasureOperations.SkinFold_Abdominal_Change:
                    user.user_measurement.SkinfoldAbdominal = value_;
                    break;

                case (int)MeasureOperations.SkinFold_Thigh_Change:
                    user.user_measurement.SkinfoldThigh = value_;
                    break;

                case (int)MeasureOperations.SkinFold_Triceps_Change:
                    user.user_measurement.SkinfoldTriceps = value_;
                    break;

                case (int)MeasureOperations.Skinfold_Suprailiac_Change:
                    user.user_measurement.SkinfoldSuprailiac = value_;
                    break;

                case (int)MeasureOperations.Skinfold_Subscapular_Change:
                    user.user_measurement.SkinfoldSubscapular = value_;
                    break;

                case (int)MeasureOperations.Skinfold_Mixadilary_Change:
                    user.user_measurement.SkinfoldMixadilary = value_;
                    break;
                default:
                    Console.WriteLine("Hata!");
                    break;
            }
            return user;
        }
        
        public App_User_Features.The_User Update_Complex_Measurements(App_User_Features.The_User user, int operation, Object value)
        {
            float value_ = (float)value;
            switch (operation) 
            {
                case (int)Complex_MeasureOperations.Body_Fat_Calculation:
                    user.user_complexmeasure.BodyFatPercentage = value_; 
                    break;
                case (int)Complex_MeasureOperations.LeanBodyMass_Calculation:
                    user.user_complexmeasure.LeanBodyMass = value_;
                    break;
                case (int)Complex_MeasureOperations.Fat_Mass_Calculation:
                    user.user_complexmeasure.LeanBodyMass = value_;
                    break;
                case (int)Complex_MeasureOperations.BMR_Calculation:
                    user.user_complexmeasure.BMR = value_;
                    break;
                case (int)Complex_MeasureOperations.TDEE_Calculation:
                    user.user_complexmeasure.TDEE = value_;
                    break;
                default:
                    Console.WriteLine("Hata");
                    break;
                
            }return user;
        }
        
        public App_User_Features.The_User Update_User_Info(App_User_Features.The_User user , int operation,Object value) 
        {
            switch (operation) 
            {
                case (int)User_Info_Operations.Date_Of_Birth_Change:
                    user.User_Info.DateOfBirth = Convert.ToDateTime(value);
                    break;
                case (int)User_Info_Operations.Age_Change:
                    user.User_Info.Age = Convert.ToInt32((Object)value);
                    break;
                case (int)User_Info_Operations.Ethnicity_Change:
                    user.User_Info.User_Ethnic = (App_User_Features.The_Personal_Info.Ethnicity)value;
                    break;
                case (int)User_Info_Operations.Gender_Change:
                    user.User_Info.Gender_ = (App_User_Features.The_Personal_Info.Gender)value;
                    break;
                case (int)User_Info_Operations.Activity_Level_Change:
                    user.User_Info.ActivityLevel_ = (App_User_Features.The_Personal_Info.ActivityLevel)value;
                    break;
                default:
                    Console.WriteLine("Hata!");
                    break;
            }
            return user;
        }
        //        
        public async Task<bool> DeleteUserAsync(int userId)
        {
            
            using (var connection = new SqliteConnection(connectionString)) 
            { 
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Users where Id = @ID";
                    command.Parameters.AddWithValue("@ID", userId);

                    int deletedrow =command.ExecuteNonQuery();
                    Console.WriteLine($"deleted row: {deletedrow}");

                }
                connection.Close();
                return true;
            }
        }

        //tamamlanacak
        public async Task<List<App_User_Features.The_User>> Get_User_All_Info_Async(int userId) 
        {
            List<App_User_Features.The_User> Userss_ = [];

            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();
                var command1 = connection.CreateCommand();
                var command2 = connection.CreateCommand();
                var command3 = connection.CreateCommand();

                command1.CommandText = "SELECT Id,Name,Mail,Password,CreationTime FROM Users";
                command2.CommandText = "SELECT Measurement_id, Measurement_time, Weight,Height,Waist,Neck," +
                    "Hip,Chest,Abdominal,Thigh,Triceps,Suprailiac, Subskapular,Mixodilary FROM Measurements";
                command3.CommandText = "SELECT Measurement_id2 ,Measurement_time,BodyFat,LeanBodyMass, FatMass , Bmr,TDEE,Measurement_type ";


            }
            return Userss_; 
        
        }

        //measurement id nin kullanılacağı task
        public async Task<App_User_Features.The_User> GetLatestMeasurementAsync(int userId) 
        {
            App_User_Features.The_User user = new();

            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();

                var command1 = connection.CreateCommand();
                var command2 = connection.CreateCommand();

                command1.CommandText = "SELECT Measurement_id, Measurement_time, Weight,Height,Waist," +
                    "Neck, Hip,Chest,Abdominal,Thigh,Triceps,Suprailiac, Subskapular,Mixodilary FROM Measurements where Id = @ID";
                command2.CommandText = "SELECT Measurement_id2 ,Measurement_time,BodyFat,LeanBodyMass, FatMass , Bmr,TDEE,Measurement_type where Id = @ID";

                command1.Parameters.AddWithValue("@ID", userId);
                command2.Parameters.AddWithValue("@ID", userId);

                //reader kısmı eklenecek




                connection.Close();
            }
            return user;
        }
        // Database operations
        public void InitializeDatabase()
        {
            Create_The_Tables();
        }

        /*Ardanın C frameworkü alınınca yapılacak
        Task<bool> Encrypt_Data_For_Safety();
        Task<bool> Crypt_Data_For_User();*/


    }
}
