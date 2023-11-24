using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Almacen
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre es Requerido")]
        [MaxLength(60, ErrorMessage ="Nombre debe ser MAximo 60 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Descripcion es Requerido")]
        [MaxLength(100, ErrorMessage = "La descripcion debe ser MAximo 100 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Estado requerido")]
        public bool Estado { get; set; }
    }
}
