using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;
using SistemaInventario.Modelos;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlmacenController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public AlmacenController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Almacen almacen = new Almacen();
            if (id == null) {

                // si el id es nulo enviamos un objeto tipo almacen para crearlo
                almacen.Estado = true;
                return View(almacen);
            }
            // por el contrario si el id esta en la base de datos buscamos y editamos
            almacen = await _unidadTrabajo.Almacen.Obtener(id.GetValueOrDefault());
            if (almacen == null)
            {
                return NotFound();
            }
            return View(almacen);
        }


        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Almacen.ObtenerTodos();
            return Json(new { data = todos });
        }

        // nota "data" es como lo va referenciar luego JS
        #endregion
    }
}
