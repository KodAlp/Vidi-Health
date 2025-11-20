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
        public string name { get; set; }

        [Required]
        public DateTime dateOfBirth { get; set; }

        [Required]
        public Gender gender { get; set; }
        [Required]
        public ActivityLevel activityLevel { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public Ethnicity your_ethnicity { get; set; }

        public List <Measurements> measurements { get; set; } = new();
        public List <BodyCompositions> bodyCompositions { get; set; } = new();
        public int age => DateTime.Now.Year - dateOfBirth.Year;
    }
    //Checkbox again
    public enum Gender
    {
        Male,
        Female
    }
    //ethnicity(need to add a warning about ethnicity actually effects the results
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
    //For calculaating daily intake
    public enum ActivityLevel
    {
        Sedentary =0, //1.2 Little or no exercise
        LightlyActive =1, //1.375 Light exercise/sports 1-3 days/week
        ModeratelyActive =2, //1.55 Moderate exercise/sports 3-5 days/week
        VeryActive =3, //1.725 Hard exercise/sports 6-7 days a week
        SuperActive = 4 //1.9 Very hard exercise/sports & physical job or 2x training
    }


}
