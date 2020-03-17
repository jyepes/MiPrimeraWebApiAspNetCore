using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimeraWebApiCore.Models
{
    public class AutorCreacionDto
    {
        [Required]
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
