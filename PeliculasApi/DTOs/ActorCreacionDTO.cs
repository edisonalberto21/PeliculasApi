using Microsoft.AspNetCore.Http;
using PeliculasApi.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTOs
{
    public class ActorCreacionDTO
    {
        [Required]

        [StringLength(120)]
        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }
        [PesoImagenValidacion(PesoMaximoMegaBytes:6)]
        public IFormFile Foto { get; set; }



    }
}
