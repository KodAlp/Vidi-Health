namespace Vidi_Health.Models
{
    public class BodyCompositions
    {
        public double BodyFatPercentage { get; set; }
        public double LeanBodyMass { get; set; } // in kg

        public double FatMass { get; set; } // in kg

        public DateTime CalculatedAt { get; set; } = DateTime.Now;

        public double BMR { get; set; } // Basal Metabolic Rate
        public double TDEE { get; set; } // Total Daily Energy Expenditure

        public Calculation_type calculation { get; set; }

        public int MeasurementId { get; set; }

        public enum Calculation_type
        {
            NavyFormula = 0,
            JacksonPollock3 = 1,
            JacksonPollock7 = 2
        }
    }
}
