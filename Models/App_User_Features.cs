using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Vidi_Health.Models
{
    public class App_User_Features
    {

        public enum MeasurementType
        {
            NavyFormula = 0,        // Tape measure method
            JacksonPollock3 = 1,    // 3-point caliper
            JacksonPollock7 = 2     // 7-point caliper
        }

        public class The_User
        {
            [Key] //Account ID
            public static int Id { get; set; }

            [Required,NotNull,MaxLength(45)]
            public string Name { get; set; }

            [Required, NotNull, MaxLength(60)]
            public string Password { get; set; }

            public DateTime AccountCreation {  get; set; }

            [Required]
            public string Mail_Adress { get; set; }

            public The_Measurements user_measurement = new(Id);
        
            public The_Complex_Measurements user_complexmeasure = new (Id);

            public The_Personal_Info User_Info = new (Id);
        }

        public  class The_Personal_Info(int Id) 
        {
             int Id = Id;
            public DateTime DateOfBirth { get; set; }        
        
            public int Age { get; set; }

            public Ethnicity User_Ethnic {  get; set; }

            public Gender Gender_ { get; set; }

            public ActivityLevel ActivityLevel_ { get; set; }


            public enum Gender
            {
                Female =0,
                Male =1,
                Empty = 2,

            }

            public enum Ethnicity
            {
                Caucasian = 0,
                Asian = 1,
                African = 2,
                Hispanic = 3,
                MiddleEastern = 4,
                NativeAmerican = 5,
                PacificIslander = 6,
                SouthAsian = 7,
                EastAsian = 8,
                Mixed = 9,
            }

            public enum ActivityLevel
            {
                Sedentary = 0, //1.2 Little or no exercise
                LightlyActive = 1, //1.375 Light exercise/sports 1-3 days/week
                ModeratelyActive = 2, //1.55 Moderate exercise/sports 3-5 days/week
                VeryActive = 3, //1.725 Hard exercise/sports 6-7 days a week
                SuperActive = 4 //1.9 Very hard exercise/sports & physical job or 2x training
                
            }
        }


        public class The_Measurements(int Id) 
        {
            public int Id = Id;

            public MeasurementType Type { get; set; }
            //System.Half Floatin bir küçüğü, 2 byte ve onalık gösterim sağlayabiliyor
            [Required,NotNull]
            public float  Weight { set; get; }
        
            [Required, NotNull]
            public float Height { set; get; }
            
            [Required, NotNull] 
            public float Waist_Measure { set; get; }

            [Required, NotNull]
            public float Neck_Measure { set; get; }
            
            [Required, NotNull]
            public float Hip_Measure { set; get; }

            [Required, NotNull]
            public float SkinfoldChest { set; get; }

            [Required, NotNull]
            public float SkinfoldAbdominal {get; set;}

            [Required, NotNull]
            public float SkinfoldThigh {  set; get; }
            
            [Required,NotNull]
            public float SkinfoldTriceps { set; get; }

            [Required, NotNull]
            public float SkinfoldSuprailiac { set; get; }

            [Required, NotNull]
            public float SkinfoldSubscapular { set; get; }
            [Required,NotNull]
            public float SkinfoldMixadilary { set; get; }

            public DateTime MeasureTime { set; get; }



        }

        public class The_Complex_Measurements(int Id )
        {
            public int Id = Id;
            //Hesaplanan Yağ Oranı
            public double BodyFatPercentage { get; set; }

            //Yağsız Vücut Kitlesi (KG)
            public double LeanBodyMass { get; set; }

            //Yağ miktarı (KG)
            public double FatMass { get; set; }


            //Bazal Metabolizma Hızı / Basal Metabolic Rate
            public double BMR { get; set; }

            //Günlük gerekli enerji miktarı / Total Daily Energy Expenditure
            public double TDEE { get; set; }

            public Calculation_type Method { get; set; }

            //Database access
            public int MeasurementId { get; set; }

            //Checkbox index taking
            public enum Calculation_type
            {
                NavyFormula = 0,
                JacksonPollock3 = 1,
                JacksonPollock7 = 2,
                TDEE =3,
                CalculateBmr = 4,
                CaloricNeeds = 5,
                CalculateBodyfat = 6,
                CalculateBmrWithBodyFat = 7
            }

        }
    }
}
