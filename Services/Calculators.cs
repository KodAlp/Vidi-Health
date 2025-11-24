using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vidi_Health.Models;
namespace Vidi_Health.Services
{
    internal class Calculators
    {
        public class BmrCalculatorService : Interfaces.IBmrCalculatorService
        {
            [DllImport("MathEngine.dll")]
            private static extern double MifflinStJeorMale(double weight, double height, int age);

            [DllImport("MathEngine.dll")]
            private static extern double MifflinStJeorFemale(double weight, double height, int age);


            [DllImport("MathEngine.dll")]
            private static extern double CunningHamFormula(double LeanbodyMass);

            [DllImport("MathEngine.dll")]
            private static extern double CalculateTDEE(double bmr, int activitylevel);


            public double CalculateBMR(App_User_Features.The_User user, App_User_Features.The_Measurements measurement)
            {
                try
                {
                    if (measurement.Weight <= 0 || measurement.Height <= 0 || user.Age == -1)
                        throw new ArgumentException("Please give proper measurements for the calculation!");

                    if (user.Gender == Gender.Male)
                    {
                        double result = MifflinStJeorMale(measurement.Weight, measurement.Height, user.Age);
                        return result;
                    }
                    else if (user.Gender == Gender.Female)
                    {
                        double result = MifflinStJeorFemale(measurement.Weight, measurement.Height, user.Age);
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

            public double CalculateBMRWithBodyFat(App_User_Features.The_Measurements measurement, double bodyfat)
            {
                try
                {
                    double result;

                    double Lean_body_mass = measurement.Weight - (measurement.Weight * bodyfat / 100);
                    result = CunningHamFormula(Lean_body_mass);
                    return result;

                }
                catch (Exception e)
                {
                    throw new ArgumentException("Unexpected variables please try again.", e);
                }
            }

            public double CalculateTDEE(double bmr, ActivityLevel activityLevel)
            {
                try
                {
                    double result_bmr;
                    result_bmr = CalculateTDEE(bmr, (int)activityLevel);
                    return result_bmr;
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Unexpected error on calculating the calories please try later", e);
                }

            }

            public double CalculateCaloricNeeds(App_User_Features.The_Measurements measurement, App_User_Features.The_User user, double body_fatpercentage)
            {
                try
                {
                    if (body_fatpercentage != -1)
                        return CalculateBMRWithBodyFat(measurement, body_fatpercentage);

                    else
                        return CalculateBMR(user, measurement);
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
            private static extern double NavyMale(double waist, double neck, double height);

            [DllImport("MathEngine.dll")]
            private static extern double NavyFemale(double waist, double neck, double hip, double height);

            [DllImport("MathEngine.dll")]
            private static extern double JP3Male(double chest, double abdomen, double thigh, int age);

            [DllImport("MathEngine.dll")]
            private static extern double JP3Female(double triceps, double abdomen, double suprailiac, int age);


            [DllImport("MathEngine.dll")]
            private static extern double JP7Male(double sum7points, int age);

            [DllImport("MathEngine.dll")]
            private static extern double JP7Female(double sum7points, int age);

            public double CalculateBodyFat(App_User_Features.The_Measurements measurement, App_User_Features.The_User user)
            {
                double bf;
                if (measurement.Type == MeasurementType.NavyFormula)
                    bf = CalculateNavyFormula(measurement, user);
                else if (measurement.Type == MeasurementType.JacksonPollock3)
                    bf = CalculateJacksonPollock3(measurement, user);
                else if (measurement.Type == MeasurementType.JacksonPollock7)
                    bf = CalculateJacksonPollock7(measurement, user);
                else
                    throw new ArgumentException("Invalid measurement type");
                return bf;
            }

            //C++ backend NavyFormula
            public double CalculateNavyFormula(App_User_Features.The_Measurements measurement, App_User_Features.The_User user)
            {
                double result;
                try
                {
                    if (!measurement.NeckCircumference.HasValue || !measurement.WaistCircumference.HasValue)
                        throw new ArgumentException("For navy formula need waist and neck measurements!");

                    if (user.Gender == Gender.Male)
                    {
                        result = NavyMale(measurement.WaistCircumference.Value,
                            measurement.NeckCircumference.Value, measurement.Height);

                    }
                    else if (user.Gender == Gender.Female)
                    {
                        if (!measurement.HipCircumference.HasValue)
                            throw new ArgumentException("For navy formula for femal need a proper hip measurement.");
                        result = NavyFemale(measurement.WaistCircumference.Value,
                            measurement.NeckCircumference.Value,
                            measurement.HipCircumference.Value,
                            measurement.Height
                            );
                    }
                    else
                    {
                        throw new ArgumentException("Gender is not defined, please choose a proper gender.");
                    }

                    return Math.Max(0, Math.Min(50, result));
                }
                catch
                {

                    return 0.0;//Must fill here about unexpected   errors.
                }
            }

            //C++ backend JP3 Formula
            public double CalculateJacksonPollock3(App_User_Features.The_Measurements measurement, App_User_Features.The_User user)
            {
                try
                {

                    if (user.Gender == Gender.Male)
                    {
                        double result;
                        if (!measurement.ChestSkinfold.HasValue || !measurement.AbdominalSkinfold.HasValue ||
                            !measurement.ThighSkinfold.HasValue || user.Age == -1)
                            throw new ArgumentException("Please satisfy the measurements first.");


                        result = JP3Male(measurement.ChestSkinfold.Value, measurement.AbdominalSkinfold.Value,
                            measurement.ThighSkinfold.Value, user.Age);
                        return result;
                    }
                    else if (user.Gender == Gender.Female)
                    {
                        double result;
                        if (!measurement.TricepsSkinfold.HasValue || !measurement.AbdominalSkinfold.HasValue ||
                           !measurement.SuprailiacSkinfold.HasValue || user.Age == -1)
                            throw new ArgumentException("Please satisfy the measurements first.");

                        result = JP3Female(measurement.TricepsSkinfold.Value, measurement.AbdominalSkinfold.Value,
                            measurement.SuprailiacSkinfold.Value, user.Age);
                        return result;

                    }
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
            public double CalculateJacksonPollock7(App_User_Features.The_Measurements measurement, App_User_Features.The_User user)
            {
                try
                {
                    double result, jp7;
                    if (!measurement.TricepsSkinfold.HasValue || !measurement.AbdominalSkinfold.HasValue ||
                        !measurement.ThighSkinfold.HasValue || !measurement.SuprailiacSkinfold.HasValue ||
                        !measurement.SubscapularSkinfold.HasValue || !measurement.MidaxillarySkinfold.HasValue
     ||
                        !measurement.ChestSkinfold.HasValue || user == null)

                        throw new Exception("Please give proper measurements for calculation.");


                    jp7 = measurement.TricepsSkinfold.Value + measurement.AbdominalSkinfold.Value + measurement.ThighSkinfold.Value +
                        measurement.SuprailiacSkinfold.Value + measurement.SubscapularSkinfold.Value + measurement.MidaxillarySkinfold.Value
                        + measurement.ChestSkinfold.Value;
                    if (user.Gender == Gender.Female)
                    {
                        result = JP7Female(jp7, user.Age);
                        return result;
                    }
                    else if (user.Gender == Gender.Male)
                    {
                        result = JP7Male(jp7, user.Age);
                        return result;
                    }
                    else
                    {
                        throw new ArgumentException("Please choose proper gender");
                    }
                }
                catch
                {
                    throw new ArgumentException("Unexpected error on calculation please try again later."); ;
                }
            }
        }

    }
}
