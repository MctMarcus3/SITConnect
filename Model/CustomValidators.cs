using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SkiaSharp;

namespace SITConnect.Model
{
    public class CustomValidators
    {
        public class ValidateDateRange : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime? dt = (DateTime?)value;

                if (dt >= DateTime.Now.AddYears(-150) && dt <= DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Invalid Date of Birth");
                }
            }
        }
        public class MaxFileSizeAttribute : ValidationAttribute
        {
            private readonly int _maxFileSize;
            public MaxFileSizeAttribute(int maxFileSize)
            {
                _maxFileSize = maxFileSize;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var file = value as IFormFile;
                if (file != null)
                {
                    if (file.Length > _maxFileSize)
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }

                return ValidationResult.Success;
            }

            public string GetErrorMessage()
            {
                return $"Maximum allowed file size is { _maxFileSize} bytes.";
            }
        }
        public class ValidateImage : ValidationAttribute
        {
            private readonly string[] _validImageMimes;

            public ValidateImage(string[] validImageMimes)
            {
                _validImageMimes = validImageMimes;
            }
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var file = (IFormFile)value;
                if (file == null)
                   return new ValidationResult(GetErrorMessage());
                if (!_validImageMimes.Contains(file.ContentType.ToString())) 
                   return new ValidationResult(GetErrorMessage());
                if(SKCodec.Create(file.OpenReadStream(), out var result) == null || result != SKCodecResult.Success)
                   return new ValidationResult(GetErrorMessage());
                return ValidationResult.Success;
            }


            public string GetErrorMessage()
            {
                return $"Image invalid or must be of type {String.Join(",", _validImageMimes.Select(filemime => String.Join(",", MimeTypes.GetMimeTypeExtensions(filemime))))}.";
            }
        }
        
    }
}
