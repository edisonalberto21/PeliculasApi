using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Validaciones
{
    public class PesoImagenValidacion:ValidationAttribute
    {
        private readonly int pesoMaximoMegaBytes;

        public PesoImagenValidacion(int PesoMaximoMegaBytes)
        {
            pesoMaximoMegaBytes = PesoMaximoMegaBytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formfile = value as IFormFile;

            if (formfile == null)
            {
                return ValidationResult.Success;
            }

            if (formfile.Length > pesoMaximoMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {pesoMaximoMegaBytes}mb");
            }

            return ValidationResult.Success;

        }
    }
}
