using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Areas.Admin.Models
{
    public class UserRoleModel
    {
        [Required(ErrorMessage="Zəhmət olmasa vəzifə adını daxil edin")]
        public string rolename { get; set; }
    }
}
