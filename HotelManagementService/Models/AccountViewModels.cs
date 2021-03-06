﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementService.Models
{
  public class ExternalLoginConfirmationViewModel
  {
    [Required]
    [Display(Name = "Nazwa użytkownika")]
    public string UserName { get; set; }
  }

  public class ManageUserViewModel
  {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Aktualne hasło")]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} musi posiadać conajmniej {2} znaków.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Nowe hasło")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Powtórz nowe hasło")]
    [Compare("NewPassword", ErrorMessage = "Hasła nie pasują do siebie.")]
    public string ConfirmPassword { get; set; }
  }

  public class LoginViewModel
  {
    [Required]
    [Display(Name = "Nazwa użytkownika")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    public string Password { get; set; }

    [Display(Name = "Pamiętaj mnie?")]
    public bool RememberMe { get; set; }
  }

  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "Nazwa użytkownika")]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "{0} musi posiadać conajmniej {2} znaków.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Nowe hasło")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Powtórz hasło")]
    [Compare("Password", ErrorMessage = "Hasła nie pasują do siebie.")]
    public string ConfirmPassword { get; set; }

    public EmployeeModels Employee { get; set; }
  }
}
