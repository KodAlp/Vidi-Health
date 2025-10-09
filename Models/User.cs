using System.ComponentModel.DataAnnotations;

namespace Vidi_Health.Models
{
    public class User
    {
        //Primary Key
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)] // Can you maximum 80 characters for the name
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }
        [Required]
        public ActivityLevel ActivityLevel { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public Ethnicity Your_ethnicity { get; set; }

        public List<Measurements> Measurements { get; set; } = new();
        public List<BodyCompositions> BodyCompositions { get; set; } = new();
        public int Age => DateTime.Now.Year - DateOfBirth.Year;
    }

    public enum Gender
    {
        Male,
        Female
    }
    public enum Ethnicity
    {
        Caucasian =0,
        Asian =1,
        African =2,
        Hispanic =3,
        MiddleEastern =4,
        NativeAmerican =5,
        PacificIslander =6,
        SouthAsian =7,
        EastAsian =8,
        Mixed =9,
    }
    public enum ActivityLevel
    {
        Sedentary =0, //1.2 Little or no exercise
        LightlyActive =1, //1.375 Light exercise/sports 1-3 days/week
        ModeratelyActive =2, //1.55 Moderate exercise/sports 3-5 days/week
        VeryActive =3, //1.725 Hard exercise/sports 6-7 days a week
        SuperActive = 4 //1.9 Very hard exercise/sports & physical job or 2x training
    }


}
