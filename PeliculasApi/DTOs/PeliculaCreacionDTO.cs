using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PeliculasApi.Validaciones;

namespace PeliculasApi.DTOs
{
    public class PeliculaCreacionDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        [PesoImagenValidacion(PesoMaximoMegaBytes:4)]
        public IFormFile Poster { get; set; }
    }
}
