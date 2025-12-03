using System.Runtime.InteropServices;
using Vidi_Health.Models;

namespace Vidi_Health.Services
{
    internal class Calculators
    {
        public static bool is_user_valid(App_User_Features.The_User user)
        {
            App_User_Features.The_Complex_Measurements.Calculation_type Process = user.user_complexmeasure.Method;
          
                if (Process == App_User_Features.The_Complex_Measurements.Calculation_type.CalculateBmr)
                {
                    if ((int)user.User_Info.Gender_ != 2 || user.user_measurement.Weight != -1 || user.user_measurement.Height != -1 || user.User_Info.Age == -1)
                        return true;
                    return false;
                }

                else if (Process == App_User_Features.The_Complex_Measurements.Calculation_type.CalculateBmrWithBodyFat)
                {
                    if (user.user_measurement.Weight != -1)
                        return true;
                    return false;
                }

                else if (Process == App_User_Features.The_Complex_Measurements.Calculation_type.TDEE)
                {
                    if (user.user_complexmeasure.BMR != -1 || (int)user.User_Info.ActivityLevel_ <= 4 || (int)user.User_Info.ActivityLevel_ >= 0)
                        return true;
                    return false;
                }

                else if (Process == App_User_Features.The_Complex_Measurements.Calculation_type.CaloricNeeds)
                {
                    if (user.user_complexmeasure.BodyFatPercentage != -1)
                        return true;
                    return false;
                }

                // A && B == (A && B) oluyor
                else if (Process == App_User_Features.The_Complex_Measurements.Calculation_type.NavyFormula)
                {
                    if (user.user_measurement.Neck_Measure != -1 || (int)user.User_Info.Gender_ != -1 ||
                        user.user_measurement.Waist_Measure != -1 || user.user_measurement.Hip_Measure != -1 && user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Female)
                        return true;
                return false;        
                }
               
                else if (Process == App_User_Features.The_Complex_Measurements.Calculation_type.JacksonPollock3)
                {
                    if (user.user_measurement.SkinfoldAbdominal != -1 ||user.User_Info.Age == -1 || 
                        user.user_measurement.SkinfoldChest != -1 && user.user_measurement.SkinfoldThigh!= -1 && user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Male||
                        user.user_measurement.SkinfoldTriceps != -1 && user.user_measurement.SkinfoldSuprailiac != -1 && user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Female
                        )
                        return true;
                    return false;
                }
            
                else if(Process == App_User_Features.The_Complex_Measurements.Calculation_type.JacksonPollock7) 
                {
                if (user.user_measurement.SkinfoldTriceps != -1 || user.user_measurement.SkinfoldAbdominal != -1 ||
                    user.user_measurement.SkinfoldThigh != -1 || user.user_measurement.SkinfoldSuprailiac != -1 ||
                    user.user_measurement.SkinfoldSubscapular != -1 || user.user_measurement.SkinfoldMixadilary != -1 ||
                    user.user_measurement.SkinfoldChest != -1 || user.User_Info.Age != 1)
                    
                    return true;
                return false;
                }
          
            return false;
        }
        public class BmrCalculatorService : Interfaces.IBmrCalculatorService
        {
            [DllImport("MathEngine.dll")]
            private static extern float MifflinStJeorMale(float weight, float height, int age);

            [DllImport("MathEngine.dll")]
            private static extern float MifflinStJeorFemale(float weight, float height, int age);


            [DllImport("MathEngine.dll")]
            private static extern float CunningHamFormula(float LeanbodyMass);

            [DllImport("MathEngine.dll")]
            private static extern float CalculateTDEE(float bmr, int activitylevel);


            public float CalculateBMR(App_User_Features.The_User user)
            {
                try
                {
                    user.user_complexmeasure.Method = App_User_Features.The_Complex_Measurements.Calculation_type.CalculateBmr;

                    if (is_user_valid(user) == false)
                        throw new ArgumentException("Please give proper measurements for the calculation!");

                    if (user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Male)
                    {
                        float result = MifflinStJeorMale(user.user_measurement.Weight,user.user_measurement.Height, user.User_Info.Age);
                        return result;
                    }
                    else if (user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Female)
                    {
                        float result = MifflinStJeorFemale(user.user_measurement.Weight, user.user_measurement.Height, user.User_Info.Age);
                        return result;
                    }
                    else
                    {
                        throw new ArgumentException("Calculation Error please try again later.");
                    }
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Unexpected error in Bmmr calculation", e);
                }
            }

            public float CalculateBMRWithBodyFat(App_User_Features.The_User user, float bodyfat)
            {
                try
                {
                    user.user_complexmeasure.Method = App_User_Features.The_Complex_Measurements.Calculation_type.CalculateBmrWithBodyFat;

                    if (is_user_valid(user) == true)
                    {
                        float result;

                        float Lean_body_mass = user.user_measurement.Weight - (user.user_measurement.Weight * bodyfat / 100);
                        result = CunningHamFormula(Lean_body_mass);
                        return result;
                    }
                    return 0;
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Unexpected variables please try again.", e);
                }
            }
            //değiştirilecek
            public float CalculateTDEE(float bmr, App_User_Features.The_User user)
            {
                try
                {
                    float result_bmr;
                    result_bmr = CalculateTDEE(bmr, Convert.ToInt32(user.User_Info.ActivityLevel_));
                    return result_bmr;
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Unexpected error on calculating the calories please try later", e);
                }

            }

            public float CalculateCaloricNeeds(App_User_Features.The_User user, float body_fatpercentage)
            {
                try
                {
                    if (body_fatpercentage != -1)
                        return CalculateBMRWithBodyFat(user, body_fatpercentage);

                    else
                        return CalculateBMR(user);
                }
                catch
                {
                    throw new ArgumentException("Please choose the calculations correctly");
                }
            }

        }

        public class BodyFatCalculatorService : Interfaces.IBodyFatCalculatorService
        {
            [DllImport("MathEngine.dll")]
            private static extern float NavyMale(float waist, float neck, float height);

            [DllImport("MathEngine.dll")]
            private static extern float NavyFemale(float waist, float neck, float hip, float height);

            [DllImport("MathEngine.dll")]
            private static extern float JP3Male(float chest, float abdomen, float thigh, int age);

            [DllImport("MathEngine.dll")]
            private static extern float JP3Female(float triceps, float abdomen, float suprailiac, int age);


            [DllImport("MathEngine.dll")]
            private static extern float JP7Male(float sum7points, int age);

            [DllImport("MathEngine.dll")]
            private static extern float JP7Female(float sum7points, int age);

            public float CalculateBodyFat(App_User_Features.The_User user)
            {

                float bf;
                if (user.user_complexmeasure.Method == App_User_Features.The_Complex_Measurements.Calculation_type.NavyFormula)
                    bf = CalculateNavyFormula(user);
                else if (user.user_complexmeasure.Method == App_User_Features.The_Complex_Measurements.Calculation_type.JacksonPollock3)
                    bf = CalculateJacksonPollock3(user);
                else if (user.user_complexmeasure.Method == App_User_Features.The_Complex_Measurements.Calculation_type.JacksonPollock7)
                    bf = CalculateJacksonPollock7(user);
                else
                    throw new ArgumentException("Invalid measurement type");
                return bf;
            }

            //C++ backend NavyFormula
            public float CalculateNavyFormula(App_User_Features.The_User user)
            {
                try
                {
                    user.user_complexmeasure.Method = App_User_Features.The_Complex_Measurements.Calculation_type.NavyFormula;

                    if (user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Male)
                    {  
                        if(is_user_valid(user) == true)
                             return NavyMale(user.user_measurement.Waist_Measure,user.user_measurement.Neck_Measure, user.user_measurement.Height);
                        return 0;
                    }
                    else if (user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Female)
                    {
                        if (is_user_valid(user) == true)
                            return NavyFemale(user.user_measurement.Waist_Measure,
                            user.user_measurement.Neck_Measure,
                            user.user_measurement.Hip_Measure,
                            user.user_measurement.Height
                            );
                        return 0;
                    }
                    else
                    {
                        throw new ArgumentException("Gender is not defined, please choose a proper gender.");
                    
                    }
                }
                catch
                {
                    return 0;//Must fill here about unexpected   errors.
                }
            }

            //C++ backend JP3 Formula
            //Erkek chest, abdominal , thigh, age
            //Kadın Triceps abdominal,Supra, age
            public float CalculateJacksonPollock3(App_User_Features.The_User user)
            {
                try
                {
                    user.user_complexmeasure.Method = App_User_Features.The_Complex_Measurements.Calculation_type.JacksonPollock3;

                    if (is_user_valid(user) != true)
                        throw new Exception("Lütfen değerleri  kontrol ediniz.");

                    if (user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Male)
                         return JP3Male(user.user_measurement.SkinfoldChest, user.user_measurement.SkinfoldAbdominal,
                            user.user_measurement.SkinfoldThigh, user.User_Info.Age);
                       
                    else if (user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Female)
                        return JP3Female(user.user_measurement.SkinfoldTriceps, user.user_measurement.SkinfoldAbdominal,
                            user.user_measurement.SkinfoldSuprailiac, user.User_Info.Age);                     
                    else
                    {
                        throw new ArgumentException("Please Choose proper gender");
                    }

                }
                catch
                {
                    throw new ArgumentException("Unexpected error while calculating bodyfat. Please try again later");
                }
            }

            //C++ backend JP7 Formula
            public float CalculateJacksonPollock7(App_User_Features.The_User user)
            {
                try
                {
                    user.user_complexmeasure.Method = App_User_Features.The_Complex_Measurements.Calculation_type.JacksonPollock7;

                    if(is_user_valid(user) != true)
                        throw new Exception("Please give proper measurements for calculation.");

                    float jp7;
                    jp7 = user.user_measurement.SkinfoldTriceps + user.user_measurement.SkinfoldAbdominal + user.user_measurement.SkinfoldThigh +
                        user.user_measurement.SkinfoldSuprailiac + user.user_measurement.SkinfoldSubscapular + user.user_measurement.SkinfoldMixadilary
                        + user.user_measurement.SkinfoldChest;
                    if (user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Female)
                        return JP7Female(jp7, user.User_Info.Age);

                    else if (user.User_Info.Gender_ == App_User_Features.The_Personal_Info.Gender.Male)
                        return JP7Male(jp7, user.User_Info.Age);
                    
                    else
                        throw new ArgumentException("Please choose proper gender");
                    
                }
                catch
                {
                    throw new ArgumentException("Unexpected error on calculation please try again later."); ;
                }
            }
        }

    }
}
