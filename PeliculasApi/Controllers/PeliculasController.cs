using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Servicios;

namespace PeliculasApi.Controllers
{
    [Route("api/peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext contex;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext contex,IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.contex = contex;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<PeliculasDTO>>> Get()
        {
            var peliculas = await contex.Peliculas.ToListAsync();
            return mapper.Map<List<PeliculasDTO>>(peliculas);
        }

        [HttpGet("{id}", Name = "obtenerPelicula")]

        public async Task<ActionResult<PeliculasDTO>> Get (int id)
        {
            var pelicula = await contex.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if(pelicula == null)
            {
                return NotFound();
            }
            return mapper.Map<PeliculasDTO>(pelicula);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);


            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    pelicula.Poster = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, peliculaCreacionDTO.Poster.ContentType);
                }
            }

            contex.Add(pelicula);
            await contex.SaveChangesAsync();
            var peliculaDTO = mapper.Map<PeliculasDTO>(pelicula);
            return new CreatedAtRouteResult("obtenerPelicula", new { id = pelicula.Id }, peliculaDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {

            var peliculasDB = await contex.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if (peliculasDB == null)
            {
                return NotFound();
            }

            peliculasDB = mapper.Map(peliculaCreacionDTO, peliculasDB);

            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    peliculasDB.Poster = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, peliculasDB.Poster, peliculaCreacionDTO.Poster.ContentType);
                }
            }

            await contex.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();

            }

            var entidadDB = await contex.Peliculas.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }

            var entidadDTO = mapper.Map<PeliculaPatchDTO>(entidadDB);

            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);

            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(entidadDTO, entidadDB);

            await contex.SaveChangesAsync();

            return NoContent();


        }

        [HttpDelete("{id}")]

        //  [EnableCors("AllowOrigin")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await contex.Peliculas.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            contex.Remove(new Pelicula() { Id = id });
            await contex.SaveChangesAsync();

            return NoContent();

        }



    }
}
