using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compuesto
{
    
    public class Pieza : ComponentePC
    {
        private double _precio;
        private string _fabricante;

        public Pieza(string nombre, string descripcion, string fabricante, double precio)
            : base(nombre, descripcion)
        {
            _precio = precio;
            _fabricante = fabricante;
        }

        public string Fabricante => _fabricante;

        
        public override void AgregarHijo(ComponentePC c) { }

        public override IList<ComponentePC> ObtenerHijos() => null;

        public override double ObtenerPrecio => _precio;

        public override void Mostrar(int nivel = 0)
        {
            string indent = new string(' ', nivel * 4);
            Console.WriteLine($"{indent}  🔩 [{Nombre}]");
            Console.WriteLine($"{indent}     Descripción : {Descripcion}");
            Console.WriteLine($"{indent}     Fabricante  : {_fabricante}");
            Console.WriteLine($"{indent}     Precio      : ${_precio:F2} MXN");
        }
    }
}
