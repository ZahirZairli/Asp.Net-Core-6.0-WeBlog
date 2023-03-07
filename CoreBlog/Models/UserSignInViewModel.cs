using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage ="İstifadəçi adınızı daxil edin")]
        public string username { get; set; }
        [Required(ErrorMessage = "Şifrənizi daxil edin")]
        public string password { get; set; }
    }
}
