using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLibros.Contexto;
using WebApiLibros.Entidades;

namespace WebApiLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LibroController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.Include(x => x.Autor).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var resultado = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id ==
            id);
            if (resultado == null)
            { return NotFound(); }
            return resultado;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new
            {
                id =
            libro.Id
            }, libro);
         }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var resultado = context.Libros.FirstOrDefault(x => x.Id ==
            id);
            if (resultado == null)
            { return NotFound(); }
            context.Libros.Remove(resultado);
            context.SaveChanges();
            return resultado;
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro value)
        {
            if (id != value.Id)
            {
                BadRequest();
            }
            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }



    }
}
