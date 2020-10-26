using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PeliculasApi.Validaciones;

namespace PeliculasApi.DTOs
{
    public class PeliculaCreacionDTO:PeliculaPatchDTO
    {
       
       
        [PesoImagenValidacion(PesoMaximoMegaBytes:4)]
        public IFormFile Poster { get; set; }
    }
}
