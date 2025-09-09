using System.Runtime.InteropServices;
using Vidi_Health.Models;

namespace Vidi_Health.Services
{
    public interface IBodyFatCalculatorService
    {
        public double CalculateBodyFat(Measurements measurement, User user)
        {
            // Measurement type'a göre uygun methodu çağır
            switch (measurement.Type)
            {
                case MeasurementType.NavyFormula:
                    return CalculateNavyFormula(measurement, user);
                case MeasurementType.JacksonPollock3:
                    return CalculateJacksonPollock3(measurement, user);
                case MeasurementType.JacksonPollock7:
                    return CalculateJacksonPollock7(measurement, user);
                default:
                    throw new ArgumentException("Invalid measurement type");
            }
        }
        double CalculateNavyFormula(Measurements measurement, User user);
        double CalculateJacksonPollock3(Measurements measurement, User user);
        double CalculateJacksonPollock7(Measurements measurement, User user);

    }
}

