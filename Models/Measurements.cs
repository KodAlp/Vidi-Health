using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vidi_Health.Models
{
    public class Measurements
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public MeasurementType Type { get; set; }

        [Required]
        public double Height { get; set; } // in cm will add feet later o

        [Required]
        public double Weight { get; set; } // in kg will add lbs later on

        // Navy Formula measurements
        public double? NeckCircumference { get; set; } // cm
        public double? WaistCircumference { get; set; } // cm
        public double? HipCircumference { get; set; } // cm (for females)

        // Caliper measurements (Jackson-Pollock)
        public double? ChestSkinfold { get; set; } // mm
        public double? AbdominalSkinfold { get; set; } // mm
        public double? ThighSkinfold { get; set; } // mm
        public double? TricepsSkinfold { get; set; } // mm
        public double? SuprailiacSkinfold { get; set; } // mm
        public double? MidaxillarySkinfold { get; set; } // mm
        public double? SubscapularSkinfold { get; set; } // mm

        public DateTime MeasuredAt { get; set; } = DateTime.Now;

        // Navigation Property
        public User User { get; set; }
        public enum MeasurementType
        {
            NavyFormula = 0,        // Tape measure method
            JacksonPollock3 = 1,    // 3-point caliper
            JacksonPollock7 = 2     // 7-point caliper
        }
    }
}
