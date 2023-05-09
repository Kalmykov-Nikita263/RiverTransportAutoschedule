using System.ComponentModel.DataAnnotations;

namespace RiverTransportAutoschedule.Models;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Логин")]
    public string UserLogin { get; set; }

    [Required]
    [UIHint("Password")]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Display(Name = "Запомнить меня?")]
    public bool RememberMe { get; set; }
}