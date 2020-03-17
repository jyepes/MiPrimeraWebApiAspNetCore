using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimeraWebApiCore.Contexts;
using MiPrimeraWebApiCore.Entities;
using MiPrimeraWebApiCore.Models;

namespace MiPrimeraWebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDBContext _context, IMapper mapper)
        {
            context = _context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDto>>> Get()
        {
            var autores =  await context.Autores.Include(x => x.Libros).ToListAsync();
            var autoresDto = mapper.Map<List<AutorDto>>(autores);
            return autoresDto;
        }

        [HttpGet("{id}", Name ="ObtenerAutor")]
        public async Task<ActionResult<AutorDto>> Get(int id)
        {
            var autor = await context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            var autorDto = mapper.Map<AutorDto>(autor);

            return autorDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]AutorCreacionDto autorCreacion)
        {
            var autor = mapper.Map<Autor>(autorCreacion);
            context.Autores.Add(autor);
            await context.SaveChangesAsync();
            var autorDto = mapper.Map<AutorDto>(autor);
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autorDto.Id }, autorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]AutorCreacionDto autorActualizacion)
        {

            var autor = mapper.Map<Autor>(autorActualizacion);
            autor.Id = id;
            context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// Actualización parcial (solo algunos campos)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id,[FromBody] JsonPatchDocument<AutorCreacionDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            var autorDto = mapper.Map<AutorCreacionDto>(autor);
            patchDocument.ApplyTo(autorDto, ModelState);

            mapper.Map(autorDto, autor);

            var isValid = TryValidateModel(autor);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            var autorId = await context.Autores.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (autorId == default(int))
            {
                return NotFound();
            }

            context.Autores.Remove(new Autor{Id = autorId });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}