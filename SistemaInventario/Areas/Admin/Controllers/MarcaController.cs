using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Marca model = new Marca();
            if (id == null) {

                // si el id es nulo enviamos un objeto tipo almacen para crearlo
                model.Estado = true;
                return View(model);
            }
            // por el contrario si el id esta en la base de datos buscamos y editamos
            model = await _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        //Upsert post 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Marca model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _unidadTrabajo.Marca.Agregar(model);
                    TempData[DS.Exitosa] = "Marca creado Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Marca.Actualizar(model);
                    TempData[DS.Exitosa] = "Marca actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al crear la Marca";
            return View(model);
            
        }

       



        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos });
        }

        // nota "data" es como lo va referenciar luego JS

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entityType = _unidadTrabajo.Marca;
            var tableName = entityType.GetType().Name;
            /// tableName = almacenRepositorio

            var nombre = "Marca";
            var modelDb = await _unidadTrabajo.Marca.Obtener(id);
            if (modelDb == null)
            {
                return Json(new { success = false, message = $"Error al borrar {nombre}" });
            }
            _unidadTrabajo.Marca.Remover(modelDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = $"{nombre} borrado exitosamente" });
        }

        // Vamos a crear un metodo el cual nos valide si ya existe un almacen con el mismo nombre en la BBDD y nos retorne un json con true o false

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Marca.ObtenerTodos();
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
