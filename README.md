# -Patr-n-de-dise-o-Composite---aplicado-a-venta-de-m-quinas-armadas

# 🖥️ Reporte de Práctica — Patrón de Diseño Composite
### Armado y Venta de Computadoras — Oasis PC

**Materia:** Patrones de Diseño de Software  
**Práctica:** Aplicación del Patrón Composite  
**Tema:** Sistema de catálogo de computadoras ensambladas por gama  
**Fecha:** 2026  

---

## 📌 Tabla de Contenidos

1. [Descripción del Patrón](#1-descripción-del-patrón)
2. [Contexto de la Práctica](#2-contexto-de-la-práctica)
3. [Código Fuente](#3-código-fuente)
4. [Diagrama UML](#4-diagrama-uml)
5. [Captura de Pantalla — Salida del Programa](#5-captura-de-pantalla--salida-del-programa)
6. [Conclusión](#6-conclusión)
7. [Referencias](#7-referencias)

---

## 1. Descripción del Patrón

### ¿Qué es el Patrón Composite?

El **Patrón Composite** es un patrón de diseño estructural que permite componer objetos en **estructuras de árbol** para representar jerarquías parte-todo. Su característica principal es que permite a los clientes **tratar de manera uniforme** tanto a objetos individuales (hojas) como a composiciones de objetos (nodos compuestos).

### Componentes del Patrón

| Elemento | Rol | Descripción |
|---|---|---|
| `Componente` | Interfaz/Clase Abstracta | Define las operaciones comunes para hojas y compuestos |
| `Hoja (Leaf)` | Objeto individual | No tiene hijos; representa los elementos primitivos del árbol |
| `Compuesto (Composite)` | Contenedor | Puede contener hojas u otros compuestos; implementa operaciones sobre sus hijos |
| `Cliente` | Consumidor | Interactúa con los elementos únicamente a través de la interfaz `Componente` |

### Vista de Árbol

```
              [CajaPC] ← Compuesto (nodo raíz)
             /    |    \
      [GamaBaja] [GamaMedia] [GamaAlta] ← Compuestos (nodos intermedios)
        / \          |           / \
    [CPU] [RAM]    [CPU]     [CPU] [RAM] ← Hojas (Pieza individual)
```

### ¿Por qué usarlo en este caso?

En la venta de computadoras ensambladas, una máquina está compuesta por múltiples **piezas individuales** (CPU, RAM, disco duro, GPU, fuente de poder), pero el cliente puede querer consultar el precio de **una sola pieza** o del **equipo completo**. El Composite permite calcular precios y describir componentes de forma recursiva sin cambiar la lógica del cliente.

---

## 2. Contexto de la Práctica

**Empresa ficticia:** *Oasis PC*  
**Objetivo:** Modelar un sistema de catálogo de computadoras ensambladas organizadas en 3 niveles de gama:

| Gama | Descripción | Uso recomendado |
|---|---|---|
| 🟢 **Básica** | Componentes de entrada económicos | Ofimática, navegación, tareas básicas |
| 🔵 **Media** | Balance entre precio y rendimiento | Estudiantes, trabajo remoto, multimedia |
| 🔴 **Alta** | Hardware de alto rendimiento | Gaming, diseño, edición de video, IA |

Cada gama representa un **nodo compuesto** que contiene múltiples piezas (hojas). El sistema imprime el **precio total** y la **descripción** de cada componente dentro de cada caja de venta.

La estructura se basa directamente en el código visto en clase (patrón Composite con `Componente`, `Directorio` y `Archivo`), renombrando las clases para reflejar el dominio de computadoras: `ComponentePC`, `CajaPC` (antiguo `Directorio`) y `Pieza` (antiguo `Archivo`).

---

## 3. Código Fuente

> ⚠️ **Nota:** El código es propio, basado en la estructura vista en clase (namespace `Composite`, clase abstracta, clase compuesta y clase hoja). Se adaptaron nombres, propiedades y lógica al dominio de venta de computadoras.

### 📄 `ComponentePC.cs` — Clase abstracta base

```csharp
using System;
using System.Collections.Generic;

namespace OasisPC
{
    /// <summary>
    /// Clase abstracta que define el contrato para piezas individuales
    /// y para cajas/grupos de piezas. Basada en la clase Componente vista en clase.
    /// </summary>
    public abstract class ComponentePC
    {
        private string _nombre;
        private string _descripcion;

        public ComponentePC(string nombre, string descripcion)
        {
            _nombre      = nombre;
            _descripcion = descripcion;
        }

        public string Nombre      => _nombre;
        public string Descripcion => _descripcion;

        // Métodos del patrón Composite
        public abstract void AgregarHijo(ComponentePC c);
        public abstract IList<ComponentePC> ObtenerHijos();

        // Precio total (hoja = precio propio; compuesto = suma de hijos)
        public abstract double ObtenerPrecio { get; }

        // Imprime el árbol con indentación visual
        public abstract void Mostrar(int nivel = 0);
    }
}
```

---

### 📄 `Pieza.cs` — Hoja (componente individual)

```csharp
using System;
using System.Collections.Generic;

namespace OasisPC
{
    /// <summary>
    /// Representa una pieza individual de hardware (CPU, RAM, etc.).
    /// Es una hoja en el árbol — no puede tener hijos.
    /// Equivalente a la clase Archivo del código de clase.
    /// </summary>
    public class Pieza : ComponentePC
    {
        private double _precio;
        private string _fabricante;

        public Pieza(string nombre, string descripcion, string fabricante, double precio)
            : base(nombre, descripcion)
        {
            _precio     = precio;
            _fabricante = fabricante;
        }

        public string Fabricante => _fabricante;

        // Las hojas no tienen hijos — operación vacía (igual que en clase)
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
```

---

### 📄 `CajaPC.cs` — Compuesto (contenedor de componentes)

```csharp
using System;
using System.Collections.Generic;

namespace OasisPC
{
    /// <summary>
    /// Representa una caja/paquete de computadora o una agrupación de piezas.
    /// Es el nodo compuesto del árbol — puede contener Piezas u otras CajaPC.
    /// Equivalente a la clase Directorio del código de clase.
    /// </summary>
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

        // Precio total = suma recursiva de todos los hijos (igual que ObtenerTamaño en clase)
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
```

---

### 📄 `Program.cs` — Main (punto de entrada)

```csharp
using System;

namespace OasisPC
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

            // ─── GAMA BÁSICA ──────────────────────────────────────────────
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

            // ─── GAMA MEDIA ───────────────────────────────────────────────
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

            // ─── GAMA ALTA ────────────────────────────────────────────────
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

            // ─── CATÁLOGO RAÍZ ────────────────────────────────────────────
            // Nodo raíz que agrupa todas las gamas (igual que "raiz" en el código de clase)
            CajaPC catalogo = new CajaPC(
                "Catálogo Oasis PC 2026",
                "Línea completa de equipos ensamblados para todo tipo de usuario.");

            catalogo.AgregarHijo(gamaBasica);
            catalogo.AgregarHijo(gamaMedia);
            catalogo.AgregarHijo(gamaAlta);

            // ─── MOSTRAR ÁRBOL COMPLETO ───────────────────────────────────
            catalogo.Mostrar();

            // ─── RESUMEN GENERAL ──────────────────────────────────────────
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
```

---

## 4. Diagrama UML

El siguiente diagrama muestra la estructura de clases del patrón Composite aplicado al sistema Oasis PC.

```
┌─────────────────────────────────────────────┐
│           <<abstract>>                      │
│           ComponentePC                      │
│─────────────────────────────────────────────│
│ - _nombre     : string                      │
│ - _descripcion: string                      │
│─────────────────────────────────────────────│
│ + ComponentePC(nombre, descripcion)         │
│ + Nombre      : string  {get}               │
│ + Descripcion : string  {get}               │
│ + AgregarHijo(c: ComponentePC) : void <<abstract>>  │
│ + ObtenerHijos() : IList<ComponentePC> <<abstract>> │
│ + ObtenerPrecio : double  <<abstract>>      │
│ + Mostrar(nivel: int) : void  <<abstract>>  │
└──────────────────┬──────────────────────────┘
                   │
         ┌─────────┴──────────┐
         │                    │
┌────────▼──────────┐  ┌──────▼────────────────────┐
│      Pieza        │  │         CajaPC             │
│ (Hoja / Leaf)     │  │   (Compuesto / Composite)  │
│───────────────────│  │────────────────────────────│
│ - _precio: double │  │ - _hijos: List<ComponentePC>│
│ - _fabricante: str│  │────────────────────────────│
│───────────────────│  │ + CajaPC(nombre, desc)     │
│ + Pieza(...)      │  │ + AgregarHijo(c) : void    │
│ + Fabricante {get}│  │ + ObtenerHijos() : IList<> │
│ + AgregarHijo()   │  │ + ObtenerPrecio : double   │
│ + ObtenerHijos()  │  │   (suma recursiva de hijos)│
│ + ObtenerPrecio   │  │ + Mostrar(nivel)           │
│ + Mostrar(nivel)  │  └──────┬─────────────────────┘
└───────────────────┘         │ contiene (1..*)
                              │◄────────────────────┐
                              │                     │
                   ┌──────────▼────────────┐        │
                   │  CajaPC / Pieza       │────────┘
                   │  (hijos del árbol)    │
                   └───────────────────────┘


Árbol de objetos en tiempo de ejecución:
─────────────────────────────────────────
catalogo (CajaPC — raíz)
  ├── gamaBasica (CajaPC)
  │     ├── CPU        (Pieza) $2,800
  │     ├── RAM        (Pieza) $550
  │     ├── Disco Duro (Pieza) $700
  │     ├── Tarjeta M. (Pieza) $1,200
  │     ├── Fuente     (Pieza) $600
  │     └── Gabinete   (Pieza) $450
  │
  ├── gamaMedia (CajaPC)
  │     ├── CPU        (Pieza) $4,500
  │     ├── RAM        (Pieza) $1,100
  │     ├── SSD        (Pieza) $900
  │     ├── Disco Duro (Pieza) $680
  │     ├── GPU        (Pieza) $3,200
  │     ├── Tarjeta M. (Pieza) $1,800
  │     ├── Fuente     (Pieza) $900
  │     └── Gabinete   (Pieza) $1,200
  │
  └── gamaAlta (CajaPC)
        ├── CPU        (Pieza) $12,000
        ├── RAM        (Pieza) $3,500
        ├── SSD NVMe   (Pieza) $2,200
        ├── SSD 2°     (Pieza) $3,100
        ├── GPU        (Pieza) $22,000
        ├── Tarjeta M. (Pieza) $6,500
        ├── Cooler     (Pieza) $3,800
        ├── Fuente     (Pieza) $2,600
        └── Gabinete   (Pieza) $3,200
```

---

## 5. Captura de Pantalla — Salida del Programa

La siguiente imagen representa la **salida esperada en consola** al ejecutar el programa en Visual Studio / .NET:

```
╔══════════════════════════════════════════╗
║     OASIS PC — Catálogo de Equipos       ║
║   Patrón de Diseño: Composite            ║
╚══════════════════════════════════════════╝

📦 === Catálogo Oasis PC 2026 ===
   Línea completa de equipos ensamblados para todo tipo de usuario.
   Componentes:

    📦 === Gama Básica — Oasis Entry ===
       Ideal para ofimática, navegación y tareas cotidianas.
       Componentes:
          🔩 [CPU]
             Descripción : Intel Core i3-12100 3.3 GHz
             Fabricante  : Intel
             Precio      : $2800.00 MXN
          🔩 [RAM]
             Descripción : Kingston 8 GB DDR4 3200 MHz
             Fabricante  : Kingston
             Precio      : $550.00 MXN
          🔩 [Disco Duro]
             Descripción : Seagate Barracuda 1 TB HDD
             Fabricante  : Seagate
             Precio      : $700.00 MXN
          🔩 [Tarjeta Madre]
             Descripción : ASUS Prime H510M-E
             Fabricante  : ASUS
             Precio      : $1200.00 MXN
          🔩 [Fuente de Poder]
             Descripción : EVGA 450W 80+ White
             Fabricante  : EVGA
             Precio      : $600.00 MXN
          🔩 [Gabinete]
             Descripción : Cougar MX340 Mid-Tower
             Fabricante  : Cougar
             Precio      : $450.00 MXN
       ─────────────────────────────────────
       💰 Precio total del paquete: $6300.00 MXN

    📦 === Gama Media — Oasis Pro ===
       Balance óptimo entre rendimiento y precio...
       ...
       💰 Precio total del paquete: $14280.00 MXN

    📦 === Gama Alta — Oasis Ultra ===
       Máximo rendimiento. Gaming 4K, diseño 3D...
       ...
       💰 Precio total del paquete: $58900.00 MXN

   ─────────────────────────────────────
   💰 Precio total del paquete: $79480.00 MXN

╔══════════════════════════════════════════╗
║              RESUMEN DE PRECIOS          ║
╠══════════════════════════════════════════╣
║  Gama Básica  :   6300.00 MXN           ║
║  Gama Media   :  14280.00 MXN           ║
║  Gama Alta    :  58900.00 MXN           ║
╠══════════════════════════════════════════╣
║  TOTAL CATÁLOGO:  79480.00 MXN          ║
╚══════════════════════════════════════════╝

Presione cualquier tecla para salir...
```

> 💡 **Nota:** Para obtener la captura real, compila el proyecto en Visual Studio (o con `dotnet run` en .NET 6+) y reemplaza esta sección con un screenshot del programa en ejecución.

---

## 6. Conclusión

El desarrollo de esta práctica permitió comprender de manera práctica y aplicada uno de los patrones estructurales más útiles del catálogo GoF: el **Patrón Composite**. A lo largo de la implementación se identificó con claridad la diferencia entre los roles de *hoja* y *compuesto*, y cómo ambos pueden ser tratados de forma transparente por el cliente mediante una interfaz común.

Adaptar el ejemplo de directorios y archivos visto en clase al dominio de venta de computadoras resultó un ejercicio revelador. La analogía es directa: así como un directorio contiene archivos u otros directorios, una caja de venta puede contener piezas individuales u otras cajas. La operación que antes calculaba el tamaño total ahora calcula el precio total de forma recursiva, lo cual demuestra que el patrón es **independiente del dominio** y puede reutilizarse en distintos contextos sin modificar su lógica estructural.

Uno de los aprendizajes más importantes fue el principio de **uniformidad**: el cliente (en `Main`) no necesita saber si está interactuando con una pieza individual o con un paquete completo. Esto reduce el acoplamiento y mejora la extensibilidad del sistema; si en el futuro se quisiera agregar una nueva categoría de gama o un subpaquete (por ejemplo, un kit de periféricos), bastaría con crear un nuevo `CajaPC` y agregarlo al árbol sin modificar el código existente.

Finalmente, esta práctica refuerza el valor de los patrones de diseño como herramientas de comunicación entre desarrolladores: al decir "esto usa Composite", cualquier programador familiarizado con el patrón entiende de inmediato la estructura sin necesidad de leer el código completo.

---

## 7. Referencias

- Gamma, E., Helm, R., Johnson, R., & Vlissides, J. (1994). *Design Patterns: Elements of Reusable Object-Oriented Software*. Addison-Wesley Professional.

- Freeman, E., & Robson, E. (2020). *Head First Design Patterns: Building Extensible and Maintainable Object-Oriented Software* (2nd ed.). O'Reilly Media.

- Refactoring.Guru. (2024). *Composite Design Pattern*. Recuperado de https://refactoring.guru/design-patterns/composite

- Microsoft. (2024). *C# documentation — Abstract classes and class members*. Recuperado de https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/abstract-and-sealed-classes-and-class-members

- SourceMaking. (2023). *Composite Design Pattern*. Recuperado de https://sourcemaking.com/design_patterns/composite

- Código base proporcionado en clase. (2026). *Ejemplo Patrón Composite — Sistema de archivos*. Namespace `Composite`, clases `Componente`, `Directorio`, `Archivo`. [Archivo: `compuesto_u3.docx`]

---

*Reporte elaborado para entrega en repositorio GitHub | Formato Markdown*  
*© 2026 — Práctica de Patrones de Diseño de Software*
