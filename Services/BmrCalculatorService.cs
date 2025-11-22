using System.Runtime.InteropServices;
using Vidi_Health.Models;

namespace Vidi_Health.Services
{
    public class BmrCalculatorService : IBmrCalculatorService
    {
        [DllImport("MathEngine.dll")]
        private static extern double MifflinStJeorMale(double weight, double height, int age);

        [DllImport("MathEngine.dll")]
        private static extern double MifflinStJeorFemale(double weight, double height, int age);


        [DllImport("MathEngine.dll")]
        private static extern double CunningHamFormula(double LeanbodyMass);

        [DllImport("MathEngine.dll")]
        private static extern double CalculateTDEE(double bmr, int activitylevel);


        public double CalculateBMR(User user, Measurements measurement)
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

        public double CalculateBMRWithBodyFat(Measurements measurement,double bodyfat)
        {
            try
            {
                double result;
                
                double Lean_body_mass = measurement.Weight - (measurement.Weight * bodyfat /100);
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

        public double CalculateCaloricNeeds(Measurements measurement, User user,double  body_fatpercentage)
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
}
