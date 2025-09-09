using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
namespace Vidi_Health.Services
{
    public class BodyFatCalculatorService : IBodyFatCalculatorService
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

        double CalculateBodyFat(Measurements measurment, User user);

        //C++ backend NavyFormula
        public double CalculateNavyFormula(Measurements measurement, User user)
        {
            double result;
            try
            {
                if (!measurement.NeckCircumference.HasValue || !measurement.WaistCircumference.HasValue)
                    throw new ArgumentException("For navy formula need waist and neck measurements!");

                if (user.gender == Gender.Male)
                {
                    result = NavyMale(measurement.WaistCircumference.Value,
                        measurement.NeckCircumference.Value, measurement.Height);

                }
                else if (user.gender == Gender.Female)
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
        public double CalculateJacksonPollock3(Measurements measurement, User user)
        {
            try
            {

                if (user.gender == Gender.Male)
                {
                    double result;
                    if (!measurement.ChestSkinfold.HasValue || !measurement.AbdominalSkinfold.HasValue ||
                        !measurement.ThighSkinfold.HasValue || user.age == -1)
                        throw new ArgumentException("Please satisfy the measurements first.");


                    result = JP3Male(measurement.ChestSkinfold.Value, measurement.AbdominalSkinfold.Value,
                        measurement.ThighSkinfold.Value, user.age);
                    return result;
                }
                else if (user.gender == Gender.Female)
                {
                    double result;
                    result = JP3Male(measurement.TricepsSkinfold.Value, measurement.AbdominalSkinfold.Value,
                        measurement.SuprailiacSkinfold.Value, user.age);
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
        public double CalculateJacksonPollock7(Measurements measurement, User user)
        {
            try
            {
                if (measurement == null || user == null)
                    throw new ArgumentNullException("Please give proper measurements for calculation.");
                double result, jp7;

                jp7 = measurement.TricepsSkinfold.Value + measurement.AbdominalSkinfold.Value + measurement.ThighSkinfold.Value +
                    measurement.SuprailiacSkinfold.Value + measurement.SubscapularSkinfold.Value + measurement.MidaxillarySkinfold.Value
                    + measurement.ChestSkinfold.Value;
                if (user.gender == Gender.Female)
                {
                    result = JP7Female(jp7, user.age);
                    return result;
                }
                else if (user.gender == Gender.Male)
                {
                    result = JP7Male(jp7, user.age);
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
