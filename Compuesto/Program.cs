using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compuesto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║     OASIS PC — Catálogo de Equipos       ║");
            Console.WriteLine("║   Patrón de Diseño: Composite            ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            
            CajaPC gamaBasica = new CajaPC(
                "Gama Básica — Oasis Entry",
                "Ideal para ofimática, navegación y tareas cotidianas.");

            gamaBasica.AgregarHijo(new Pieza(
                "CPU", "Intel Core i3-12100 3.3 GHz", "Intel", 2800.00));
            gamaBasica.AgregarHijo(new Pieza(
                "RAM", "Kingston 8 GB DDR4 3200 MHz", "Kingston", 550.00));
            gamaBasica.AgregarHijo(new Pieza(
                "Disco Duro", "Seagate Barracuda 1 TB HDD", "Seagate", 700.00));
            gamaBasica.AgregarHijo(new Pieza(
                "Tarjeta Madre", "ASUS Prime H510M-E", "ASUS", 1200.00));
            gamaBasica.AgregarHijo(new Pieza(
                "Fuente de Poder", "EVGA 450W 80+ White", "EVGA", 600.00));
            gamaBasica.AgregarHijo(new Pieza(
                "Gabinete", "Cougar MX340 Mid-Tower", "Cougar", 450.00));

            
            CajaPC gamaMedia = new CajaPC(
                "Gama Media — Oasis Pro",
                "Balance óptimo entre rendimiento y precio. Streaming, trabajo remoto, multimedia.");

            gamaMedia.AgregarHijo(new Pieza(
                "CPU", "AMD Ryzen 5 5600X 3.7 GHz", "AMD", 4500.00));
            gamaMedia.AgregarHijo(new Pieza(
                "RAM", "Corsair Vengeance 16 GB DDR4 3600 MHz", "Corsair", 1100.00));
            gamaMedia.AgregarHijo(new Pieza(
                "SSD", "Samsung 870 EVO 500 GB SATA", "Samsung", 900.00));
            gamaMedia.AgregarHijo(new Pieza(
                "Disco Duro", "WD Blue 1 TB HDD", "Western Digital", 680.00));
            gamaMedia.AgregarHijo(new Pieza(
                "GPU", "NVIDIA GTX 1650 4 GB GDDR6", "ASUS", 3200.00));
            gamaMedia.AgregarHijo(new Pieza(
                "Tarjeta Madre", "MSI B550-A PRO", "MSI", 1800.00));
            gamaMedia.AgregarHijo(new Pieza(
                "Fuente de Poder", "Corsair CV650 650W 80+ Bronze", "Corsair", 900.00));
            gamaMedia.AgregarHijo(new Pieza(
                "Gabinete", "NZXT H510 Flow", "NZXT", 1200.00));

            
            CajaPC gamaAlta = new CajaPC(
                "Gama Alta — Oasis Ultra",
                "Máximo rendimiento. Gaming 4K, diseño 3D, edición de video y cargas de IA.");

            gamaAlta.AgregarHijo(new Pieza(
                "CPU", "Intel Core i9-13900K 3.0 GHz 24 núcleos", "Intel", 12000.00));
            gamaAlta.AgregarHijo(new Pieza(
                "RAM", "G.Skill Trident Z5 32 GB DDR5 6000 MHz", "G.Skill", 3500.00));
            gamaAlta.AgregarHijo(new Pieza(
                "SSD NVMe", "Samsung 990 Pro 1 TB PCIe 4.0", "Samsung", 2200.00));
            gamaAlta.AgregarHijo(new Pieza(
                "SSD NVMe secundario", "WD Black SN850X 2 TB", "Western Digital", 3100.00));
            gamaAlta.AgregarHijo(new Pieza(
                "GPU", "NVIDIA RTX 4080 Super 16 GB GDDR6X", "ASUS ROG", 22000.00));
            gamaAlta.AgregarHijo(new Pieza(
                "Tarjeta Madre", "ASUS ROG Strix Z790-E Gaming WiFi", "ASUS", 6500.00));
            gamaAlta.AgregarHijo(new Pieza(
                "Cooler", "Corsair H150i Elite LCD 360 mm AIO", "Corsair", 3800.00));
            gamaAlta.AgregarHijo(new Pieza(
                "Fuente de Poder", "Seasonic FOCUS GX-1000W 80+ Gold", "Seasonic", 2600.00));
            gamaAlta.AgregarHijo(new Pieza(
                "Gabinete", "Lian Li O11 Dynamic EVO XL", "Lian Li", 3200.00));

            
            CajaPC catalogo = new CajaPC(
                "Catálogo Oasis PC 2026",
                "Línea completa de equipos ensamblados para todo tipo de usuario.");

            catalogo.AgregarHijo(gamaBasica);
            catalogo.AgregarHijo(gamaMedia);
            catalogo.AgregarHijo(gamaAlta);

            
            catalogo.Mostrar();

            
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║              RESUMEN DE PRECIOS          ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine($"║  Gama Básica  : ${gamaBasica.ObtenerPrecio,10:F2} MXN      ║");
            Console.WriteLine($"║  Gama Media   : ${gamaMedia.ObtenerPrecio,10:F2} MXN      ║");
            Console.WriteLine($"║  Gama Alta    : ${gamaAlta.ObtenerPrecio,10:F2} MXN      ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine($"║  TOTAL CATÁLOGO: ${catalogo.ObtenerPrecio,9:F2} MXN      ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
