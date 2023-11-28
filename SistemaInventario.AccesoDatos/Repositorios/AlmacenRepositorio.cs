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
    internal class AlmacenRepositorio : Repositorio<Almacen>, IAlmacenRepositorio
    {

        private readonly ApplicationDbContext _db;

        public AlmacenRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Almacen almacen)
        {
            var almacenDB = _db.Almacenes.FirstOrDefault(a => a.Id == almacen.Id);

            if (almacenDB != null)
            {
                almacenDB.Name = almacen.Name;
                almacenDB.Description = almacen.Description;
                almacenDB.Estado = almacen.Estado;
                _db.SaveChanges();
            }
        }
    }
}
