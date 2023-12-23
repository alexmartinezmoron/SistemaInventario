using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.Modelos;

namespace SistemaInventario.Areas.Admin.ViewModels
{
    public class ProductoVM
    {
        [ValidateNever]
        public Producto Producto { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoriaLista { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MarcaLista { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> PadreLista { get; set; }
    }
}
