using System.Runtime.InteropServices;
using Vidi_Health.Models;

namespace Vidi_Health.Services
{
    public interface IBodyFatCalculatorService
    {
        public double CalculateBodyFat(Measurements measurement, User user);
        double CalculateNavyFormula(Measurements measurement, User user);
        double CalculateJacksonPollock3(Measurements measurement, User user);
        double CalculateJacksonPollock7(Measurements measurement, User user);

    }
}

