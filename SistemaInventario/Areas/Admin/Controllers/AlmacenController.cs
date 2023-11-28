using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

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

        //Upsert post 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Almacen alamacen)
        {
            if (ModelState.IsValid)
            {
                if (alamacen.Id == 0)
                {
                    await _unidadTrabajo.Almacen.Agregar(alamacen);
                    TempData[DS.Exitosa] = "Almacen creado Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Almacen.Actualizar(alamacen);
                    TempData[DS.Exitosa] = "Almacen actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al crear el Almacen";
            return View(alamacen);
            
        }

       



        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Almacen.ObtenerTodos();
            return Json(new { data = todos });
        }

        // nota "data" es como lo va referenciar luego JS

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entityType = _unidadTrabajo.Almacen;
            var tableName = entityType.GetType().Name;
            /// tableName = almacenRepositorio

            var nombre = "Almacen";
            var almacenDb = await _unidadTrabajo.Almacen.Obtener(id);
            if (almacenDb == null)
            {
                return Json(new { success = false, message = $"Error al borrar {nombre}" });
            }
            _unidadTrabajo.Almacen.Remover(almacenDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = $"{nombre} borrado exitosamente" });
        }

        // Vamos a crear un metodo el cual nos valide si ya existe un almacen con el mismo nombre en la BBDD y nos retorne un json con true o false

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Almacen.ObtenerTodos();
            if (id == 0) // es decir si es crear en lugar de editar
            {
                valor = lista.Any(b => b.Name.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Name.ToLower().Trim() == nombre.ToLower().Trim() && b.Id !=0); // si fuese editar podria conincidir que si hay una con el mismo nombre,
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
