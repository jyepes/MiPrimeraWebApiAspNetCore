using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraWebApiCore.Models
{
    public class LibroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
    }
}
