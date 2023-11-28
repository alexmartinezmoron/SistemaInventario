using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorios.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorios
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Categoria categoria)
        {
            var acategorianDB = _db.Categorias.FirstOrDefault(a => a.Id == categoria.Id);

            if (acategorianDB != null)
            {
                acategorianDB.Name = categoria.Name;
                acategorianDB.Estado = categoria.Estado;
                _db.SaveChanges();
            }
        }
    }
}
