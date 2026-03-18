using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compuesto
{
   
    public abstract class ComponentePC
    {
        private string _nombre;
        private string _descripcion;

        public ComponentePC(string nombre, string descripcion)
        {
            _nombre = nombre;
            _descripcion = descripcion;
        }

        public string Nombre => _nombre;
        public string Descripcion => _descripcion;

        
        public abstract void AgregarHijo(ComponentePC c);
        public abstract IList<ComponentePC> ObtenerHijos();

        
        public abstract double ObtenerPrecio { get; }

        
        public abstract void Mostrar(int nivel = 0);
    }
}
