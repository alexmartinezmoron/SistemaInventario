using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorios.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        IAlmacenRepositorio Almacen{ get; }

        Task Guardar();
    }
}
