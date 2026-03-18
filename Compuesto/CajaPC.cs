using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compuesto
{
    
    public class CajaPC : ComponentePC
    {
        private List<ComponentePC> _hijos;

        public CajaPC(string nombre, string descripcion)
            : base(nombre, descripcion)
        {
            _hijos = new List<ComponentePC>();
        }

        public override void AgregarHijo(ComponentePC c)
        {
            _hijos.Add(c);
        }

        public override IList<ComponentePC> ObtenerHijos()
        {
            return _hijos.ToArray();
        }

        
        public override double ObtenerPrecio
        {
            get
            {
                double total = 0;
                foreach (var hijo in _hijos)
                    total += hijo.ObtenerPrecio;
                return total;
            }
        }

        public override void Mostrar(int nivel = 0)
        {
            string indent = new string(' ', nivel * 4);
            Console.WriteLine();
            Console.WriteLine($"{indent}📦 === {Nombre} === ");
            Console.WriteLine($"{indent}   {Descripcion}");
            Console.WriteLine($"{indent}   Componentes:");

            foreach (var hijo in _hijos)
                hijo.Mostrar(nivel + 1);

            Console.WriteLine($"{indent}   ─────────────────────────────────────");
            Console.WriteLine($"{indent}   💰 Precio total del paquete: ${ObtenerPrecio:F2} MXN");
        }
    }
}
