using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Validaciones
{
    public class TipoArchivoValidacion:ValidationAttribute
    {
        private readonly string[] tiposvalidos;

        public TipoArchivoValidacion(string[] tiposvalidos)
        {
            this.tiposvalidos = tiposvalidos;
        }

        
    }
}
