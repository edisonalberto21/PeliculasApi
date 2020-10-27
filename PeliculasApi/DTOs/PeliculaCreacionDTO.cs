using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PeliculasApi.Validaciones;
using Microsoft.AspNetCore.Mvc;
using PeliculasApi.Helpers;

namespace PeliculasApi.DTOs
{
    public class PeliculaCreacionDTO:PeliculaPatchDTO
    {
       
       
        [PesoImagenValidacion(PesoMaximoMegaBytes:4)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIDs { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPeliculasCreacionDTO>>))]
        public List<ActorPeliculasCreacionDTO> Actores { get; set; }


    }
}
