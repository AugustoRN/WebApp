using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Repositorio
{
    interface IRepositorio<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}
