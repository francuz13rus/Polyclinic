using System.ComponentModel.DataAnnotations;

namespace Polyclinic.Data_Transfer_Objects
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
