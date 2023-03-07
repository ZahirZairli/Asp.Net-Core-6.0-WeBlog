using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class UserSignUpViewModel
    {
        [Required(ErrorMessage ="Zəhmət olmasa adınızı və soyadınızı daxil edin")]
        [Display(Name = "Tam ad")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa şifrə daxil edin")]
        [Display(Name = "Şifrə")]
        public string Password { get; set; }

        [Display(Name = "Təkrar şifrə")]
        [Compare("Password",ErrorMessage ="Daxil etdiyiniz şifrələr eyni deyil")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Mail")]
        [Required(ErrorMessage = "Zəhmət olmasa mail daxil edin")]
        public string Mail { get; set; }

        [Display(Name = "İstifadəçi adı")]
        [Required(ErrorMessage = "Zəhmət olmasa bir istifadəçi adı daxil edin")]
        public string UserName { get; set; }

    }
}
