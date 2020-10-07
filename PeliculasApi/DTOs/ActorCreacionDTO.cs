using Microsoft.AspNetCore.Http;
using PeliculasApi.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTOs
{
    public class ActorCreacionDTO: ActorPatchDTO
    {
        
        [PesoImagenValidacion(PesoMaximoMegaBytes:6)]
        public IFormFile Foto { get; set; }



    }
}
