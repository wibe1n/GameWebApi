using System;
using System.ComponentModel.DataAnnotations;

public class CheckTheDateAttribute : ValidationAttribute {
    public string GetErrorMessage() => 
    "Date needs to be from the past";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime dateTime = (DateTime)validationContext.ObjectInstance;
        if(DateTime.Compare(dateTime, DateTime.UtcNow) > 0){
            return new ValidationResult(GetErrorMessage());
        }
        return ValidationResult.Success;
    }



    
}