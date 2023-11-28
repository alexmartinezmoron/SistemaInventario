using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorios
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
                
        private readonly ApplicationDbContext _db;
        public IAlmacenRepositorio Almacen { get; private set; }  
        public ICategoriaRepositorio Categoria { get; private set; }


        // inicializamos nuestro db y nuestros repositorios
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Almacen = new AlmacenRepositorio(_db);
            Categoria = new CategoriaRepositorio(_db);
        }


        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
