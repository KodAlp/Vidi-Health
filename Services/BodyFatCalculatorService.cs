using Vidi_Health.Models;
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

        public double CalculateBodyFat(Measurements measurement, User user)
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
        public double CalculateNavyFormula(Measurements measurement, User user)
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
        public double CalculateJacksonPollock3(Measurements measurement, User user)
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
        public double CalculateJacksonPollock7(Measurements measurement, User user)
        {
            try
            {
                double result, jp7;
                if (!measurement.TricepsSkinfold.HasValue || !measurement.AbdominalSkinfold.HasValue || 
                    !measurement.ThighSkinfold.HasValue || !measurement.SuprailiacSkinfold.HasValue || 
                    !measurement.SubscapularSkinfold.HasValue || !measurement.MidaxillarySkinfold.HasValue
 || 
                    !measurement.ChestSkinfold.HasValue ||  user == null)

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
