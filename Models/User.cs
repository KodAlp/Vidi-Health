using System.ComponentModel.DataAnnotations;
namespace Vidi_Health.Models
{
    public class User
    {
        //Primary Key
        [Key]
        public int Id  { get; set; }

        [Required]
        [MaxLength(80)] // Can you maximum 80 characters for the name

    }
}
