using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManagementService.Validators
{
  public class IsPesel : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      string errorMessage;
      string pesel;

      if (validationContext.DisplayName == null)
      {
        errorMessage = "Numer PESEL jest nieprawidłowy!";
      }
      else
        errorMessage = FormatErrorMessage(validationContext.DisplayName);
      if (value == null)
        return ValidationResult.Success;

      if (value is string)
        pesel = value.ToString();
      else
        return new ValidationResult("Typ pola przechowywujący PESEL nie jest tekstem!");

      if (pesel.Length != 11)
        return new ValidationResult(errorMessage);

      int[] weight = {1, 3, 7, 9, 1, 3, 7, 9,1,3};
      var k = 0;

      for (var i = 0; i < pesel.Length; i++)
      {
        int temp;

        if(!Int32.TryParse(pesel[i].ToString(), out temp))
          return new ValidationResult(errorMessage);

        if (i + 1 == pesel.Length)
        {
          if ((10 - k%10)%10 != temp)
            return new ValidationResult(errorMessage);

        }
        else
          k += temp*weight[i];
      }

      return ValidationResult.Success;
    }
  }
}