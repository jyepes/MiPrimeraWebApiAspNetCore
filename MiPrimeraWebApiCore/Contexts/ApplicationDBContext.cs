using Microsoft.EntityFrameworkCore;
using MiPrimeraWebApiCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraWebApiCore.Contexts
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}
