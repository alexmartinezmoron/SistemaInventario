using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ProductoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
       
            return View();
        }

        //Upsert post 



        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todos });
        }

        // nota "data" es como lo va referenciar luego JS

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entityType = _unidadTrabajo.Producto;
            var tableName = entityType.GetType().Name;
            /// tableName = almacenRepositorio

            var nombre = "Producto";
            var modelDb = await _unidadTrabajo.Producto.Obtener(id);
            if (modelDb == null)
            {
                return Json(new { success = false, message = $"Error al borrar {nombre}" });
            }
            _unidadTrabajo.Producto.Remover(modelDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = $"{nombre} borrado exitosamente" });
        }

        // Vamos a crear un metodo el cual nos valide si ya existe un almacen con el mismo nombre en la BBDD y nos retorne un json con true o false

        [ActionName("ValidarSerie")]
        public async Task<IActionResult> ValidarSerie(string serie, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Producto.ObtenerTodos();
            if (id == 0) // es decir si es crear en lugar de editar
            {
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim() && b.Id !=0); // si fuese editar podria conincidir que si hay una con el mismo nombre,
                                                                                                        // por ello indicamos que no tenga el mismo Id
            }
            if (valor == true)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }

        #endregion
    }
}
