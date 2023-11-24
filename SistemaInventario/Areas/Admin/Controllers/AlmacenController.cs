using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;

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
