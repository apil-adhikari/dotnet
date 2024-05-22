using PlayerInfoCrudWithValidation.Models.ModelsValidation;
using System.ComponentModel.DataAnnotations;

namespace PlayerInfoCrudWithValidation.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; } // Primary key
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required!")]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Sex is required!")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Nationalaity is required")]
        [StringLength(50, ErrorMessage = "Nationality cannot exceed 50 characters!")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a 10-digit phone number.")]
        public string PhoneNumber { get; set; }

        //[NotMapped]
        //public string ProfilePicture { get; set; } // Change name to ProfilePicture

        // Consider this app is for football team player ask the position
        [Required(ErrorMessage = "Player position is required")]
        public string PlayerPosition { get; set; }
    }
}
