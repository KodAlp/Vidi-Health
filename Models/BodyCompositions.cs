namespace Vidi_Health.Models
{
    public class BodyCompositions
    {
        //Hesaplanan Yağ Oranı
        public double BodyFatPercentage { get; set; }

        //Yağsız Vücut Kitlesi (KG)
        public double LeanBodyMass { get; set; } 

        //Yağ miktarı (KG)
        public double FatMass { get; set; } 

        //Hesaplanma tarihi / Calculating date For tracking progress
        public DateTime CalculatedAt { get; set; } = DateTime.Now;

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
            JacksonPollock7 = 2
        }
    }
}
