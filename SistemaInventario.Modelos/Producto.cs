using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El numero de Serie es Requerido")]
        [MaxLength(60, ErrorMessage = "Numero de Serie debe ser Maximo 60 caracteres")]
        public string NumeroSerie { get; set; }

        [Required(ErrorMessage = "Descripcion es Requerido")]
        [MaxLength(100, ErrorMessage = "La descripcion debe ser MAximo 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Precio es Requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Costo es Requerido")]
        public double Costo { get; set; }

        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "Estado requerido")]
        public bool Estado { get; set; }

        
        /// Relaciones
        
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }        

        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        // Recursividad
        public int? PadreId { get; set; }

        //Navegacion
        public virtual Producto Padre { get; set; }
    }
}
