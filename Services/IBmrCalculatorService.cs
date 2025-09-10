using Vidi_Health.Models;

namespace Vidi_Health.Services
{
    public interface IBmrCalculatorService
    {
        double CalculateBMR(User user, Measurements measurement);
        double CalculateBMRWithBodyFat(Measurements measurement, double bodyFat);
        double CalculateTDEE(double bmr,ActivityLevel activityLevel);
        double CalculateCaloricNeeds(Measurements measurement, User user , double bodyfat_percentage);

    }
}   
