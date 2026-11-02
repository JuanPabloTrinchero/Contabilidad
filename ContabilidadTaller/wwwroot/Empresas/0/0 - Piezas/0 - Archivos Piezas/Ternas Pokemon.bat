/*
@echo off
title Script Hibrido Bat y C#
cd /d "%userprofile%\Desktop"

:: 1. Mostrar configuracion de red
echo === CONFIGURACION DE RED ===
ipconfig
echo.

cls
echo TERNAS DE INICIALES (Presione una tecla para iniciar el programa)
pause > nul

:: 2. Limpiar pantalla
cls

:: 3. Compilar el archivo actual (sin la bandera problematica /tp)
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe" /nologo /out:"mensaje.exe" "%~f0"

:: 4. Ejecutar el programa de C# generado si existe
if exist "mensaje.exe" (
    "mensaje.exe"
) else (
    echo [ERROR] No se pudo compilar el codigo C#. Asegurate de que .NET Framework 4.0 este instalado.
    pause
    exit /b
)

:: 5. Limpieza del ejecutable local y cierre silencioso
del "mensaje.exe"
pause > nul
exit /b
*/

// ==========================================
// A PARTIR DE AQUI ES CODIGO C# PURO
// ==========================================
using System;
using System.Collections.Generic; // REQUERIDO PARA USAR LISTAS

class Program {
    
    // Definicion de la estructura del objeto
    public class TipoPokemon
    {
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
        // EFECTIVIDAD DE ATAQUES
        public List<int> SuperEfectivo { get; set; } // X2
        public List<int> Neutro { get; set; } // X1
        public List<int> PocoEficaz { get; set; } // X1/2
        public List<int> Ineficaz { get; set; } // X0
    }

//-----------------------------------DATOS TIPOS-----------------------------------

    // TABLA DE DATOS DE TIPOS Y EFECTIVIDADES
    static List<TipoPokemon> TiposElementales = new List<TipoPokemon>()
    {
        new TipoPokemon()
        {
            IdTipo = 1,
            NombreTipo = "Acero",
            SuperEfectivo = new List<int> { 8, 9, 14 },
            Neutro = new List<int> { 3, 4, 6, 10, 11, 12, 13, 15, 16, 17, 18 },
            PocoEficaz = new List<int> { 1, 2, 5, 7 },
            Ineficaz = new List<int> {  }
        },
        new TipoPokemon()
        {
            IdTipo = 2,
            NombreTipo = "Agua",
            SuperEfectivo = new List<int> { 7, 14, 16 },
            Neutro = new List<int> { 1, 3, 5, 6, 8, 9, 10, 11, 13, 15, 17, 18 },
            PocoEficaz = new List<int> { 2, 4, 12 },
            Ineficaz = new List<int> {  }
        },
        new TipoPokemon()
        {
            IdTipo = 3,
            NombreTipo = "Bicho",
            SuperEfectivo = new List<int> { 12, 13, 15 },
            Neutro = new List<int> { 2, 3, 4, 5, 9, 11, 14, 16 },
            PocoEficaz = new List<int> { 1, 6, 7, 8, 10, 17, 18 },
            Ineficaz = new List<int> {  }
        },
        new TipoPokemon()
        {
            IdTipo = 4,
            NombreTipo = "Dragon",
            SuperEfectivo = new List<int> { 4 },
            Neutro = new List<int> { 2, 3, 5, 6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 },
            PocoEficaz = new List<int> { 1 },
            Ineficaz = new List<int> { 8 }
        },
        new TipoPokemon()
        {
            IdTipo = 5,
            NombreTipo = "Electrico",
            SuperEfectivo = new List<int> { 2, 18 },
            Neutro = new List<int> { 1, 3, 6, 7, 8, 9, 10, 11, 13, 14, 15, 17 },
            PocoEficaz = new List<int> { 4, 5, 12 },
            Ineficaz = new List<int> { 16 }
        },
        new TipoPokemon()
        {
            IdTipo = 6,
            NombreTipo = "Fantasma",
            SuperEfectivo = new List<int> { 6, 13 },
            Neutro = new List<int> { 1, 2, 3, 4, 5, 7, 8, 9, 10, 12, 14, 16, 17, 18 },
            PocoEficaz = new List<int> { 15 },
            Ineficaz = new List<int> { 11 }
        },
        new TipoPokemon()
        {
            IdTipo = 7,
            NombreTipo = "Fuego",
            SuperEfectivo = new List<int> { 1, 3, 9, 12 },
            Neutro = new List<int> { 5, 6, 8, 10, 11, 13, 15, 16, 17, 18 },
            PocoEficaz = new List<int> { 2, 4, 7, 14 },
            Ineficaz = new List<int> {  }
        },
        new TipoPokemon()
        {
            IdTipo = 8,
            NombreTipo = "Hada",
            SuperEfectivo = new List<int> { 4, 10, 15 },
            Neutro = new List<int> { 2, 3, 5, 6, 8, 9, 11, 12, 13, 14, 16, 18 },
            PocoEficaz = new List<int> { 1, 7, 17 },
            Ineficaz = new List<int> {  }
        },
        new TipoPokemon()
        {
            IdTipo = 9,
            NombreTipo = "Hielo",
            SuperEfectivo = new List<int> { 4, 12, 16, 18 },
            Neutro = new List<int> { 3, 5, 6, 8, 10, 11, 13, 14, 15, 17 },
            PocoEficaz = new List<int> { 1, 2, 7, 9 },
            Ineficaz = new List<int> {  }
        },
        new TipoPokemon()
        {
            IdTipo = 10,
            NombreTipo = "Lucha",
            SuperEfectivo = new List<int> { 1, 9, 11, 14, 15 },
            Neutro = new List<int> { 2, 4, 5, 7, 10, 12, 16 },
            PocoEficaz = new List<int> { 3, 8, 13, 17, 18 },
            Ineficaz = new List<int> { 6 }
        },
        new TipoPokemon()
        {
            IdTipo = 11,
            NombreTipo = "Normal",
            SuperEfectivo = new List<int> {  },
            Neutro = new List<int> { 2, 3, 4, 5, 7, 8, 9, 10, 11, 12, 13, 15, 16, 17, 18 },
            PocoEficaz = new List<int> { 1, 14 },
            Ineficaz = new List<int> { 6 }
        },
        new TipoPokemon()
        {
            IdTipo = 12,
            NombreTipo = "Planta",
            SuperEfectivo = new List<int> { 2, 14, 16 },
            Neutro = new List<int> { 5, 6, 8, 9, 10, 11, 13, 15 },
            PocoEficaz = new List<int> { 1, 3, 4, 7, 12, 17, 18 },
            Ineficaz = new List<int> {  }
        },
        new TipoPokemon()
        {
            IdTipo = 13,
            NombreTipo = "Psiquico",
            SuperEfectivo = new List<int> { 10, 17 },
            Neutro = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 14, 16, 18 },
            PocoEficaz = new List<int> { 1, 13 },
            Ineficaz = new List<int> { 15 }
        },
        new TipoPokemon()
        {
            IdTipo = 14,
            NombreTipo = "Roca",
            SuperEfectivo = new List<int> { 3, 7, 9, 18 },
            Neutro = new List<int> { 2, 4, 5, 6, 8, 11, 12, 13, 14, 15, 17 },
            PocoEficaz = new List<int> { 1, 10, 16 },
            Ineficaz = new List<int> {  }
        },
        new TipoPokemon()
        {
            IdTipo = 15,
            NombreTipo = "Siniestro",
            SuperEfectivo = new List<int> { 6, 13 },
            Neutro = new List<int> { 1, 2, 3, 4, 5, 7, 9, 11, 12, 14, 16, 17, 18 },
            PocoEficaz = new List<int> { 8, 10, 15 },
            Ineficaz = new List<int> {  }
        },
        new TipoPokemon()
        {
            IdTipo = 16,
            NombreTipo = "Tierra",
            SuperEfectivo = new List<int> { 1, 5, 7, 14, 17 },
            Neutro = new List<int> { 2, 4, 6, 8, 9, 10, 11, 13, 15, 16 },
            PocoEficaz = new List<int> { 3, 12 },
            Ineficaz = new List<int> { 18 }
        },
        new TipoPokemon()
        {
            IdTipo = 17,
            NombreTipo = "Veneno",
            SuperEfectivo = new List<int> { 8, 12 },
            Neutro = new List<int> { 2, 3, 4, 5, 7, 9, 10, 11, 13, 15, 18 },
            PocoEficaz = new List<int> { 6, 14, 16, 17 },
            Ineficaz = new List<int> { 1 }
        },
        new TipoPokemon()
        {
            IdTipo = 18,
            NombreTipo = "Volador",
            SuperEfectivo = new List<int> { 3, 10, 12 },
            Neutro = new List<int> { 2, 4, 6, 7, 8, 9, 11, 13, 15, 16, 17, 18 },
            PocoEficaz = new List<int> { 1, 5, 14 },
            Ineficaz = new List<int> {  }
        }
    };

//-----------------------------------MAIN-----------------------------------

    static void Main() {
        
	RepetirMenu(TrioDeIniciales);

    }

//-----------------------------------METODOS-----------------------------------

static TipoPokemon ElegirTipoPokemon(int id)
{
    TipoPokemon tipo = new TipoPokemon() { IdTipo = -1 };
    foreach (TipoPokemon t in TiposElementales)
    {
        if (t.IdTipo == id)
        {
            tipo = t;
            break;
        }
    }
    return tipo;
}

//----------------------------------------------------------------------

static List<TipoPokemon> Tipo_SuperEfectivoContra(int Tipo)
{
    TipoPokemon tipo = ElegirTipoPokemon(Tipo);
    List<TipoPokemon> tiposDebiles = new List<TipoPokemon>();

    foreach (int i in tipo.SuperEfectivo)
    {
        foreach (TipoPokemon t in TiposElementales)
        {
            if (i == t.IdTipo)
            {
                tiposDebiles.Add(t);
            }
        }
    }
    return tiposDebiles;
}

//----------------------------------------------------------------------

static bool PrimerTipo_EsSuperEfectivoContra_SegundoTipo(TipoPokemon primerTipo, TipoPokemon segundoTipo)
{
    bool esSuperEfectivo = false;
    if (primerTipo.SuperEfectivo.Contains(segundoTipo.IdTipo))
    {
        esSuperEfectivo = true;
    }
    return esSuperEfectivo;
}

//----------------------------------------------------------------------

static void TrioDeIniciales()
{
    int cantTipos = TiposElementales.Count;

    TipoPokemon primero = new TipoPokemon() { IdTipo = 0 };
    TipoPokemon segundo = new TipoPokemon() { IdTipo = 0 };

    int primeraOp;
    bool primeroCorrecto = false;

    int contadorOpcion = 0;
    do
    {
        MenuOpciones(primero, segundo);

        if (Int32.TryParse(Console.ReadLine(), out primeraOp) && primeraOp > 0 && primeraOp <= cantTipos)
        {
            primero = ElegirTipoPokemon(primeraOp);
            primeroCorrecto = true;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("ALERTA: Ingrese una opcion valida!!!");
            Console.WriteLine();
        }

    } while (!primeroCorrecto);
    Console.Clear();

    int segundaOp;
    bool segundoCorrecto = false;
    do
    {
        MenuOpciones(primero, segundo);

        if (Int32.TryParse(Console.ReadLine(), out segundaOp) && segundaOp > 0 && segundaOp <= cantTipos)
        {
            segundo = ElegirTipoPokemon(segundaOp);
            segundoCorrecto = true;
        }
        else
        {
            Console.Write("Ingrese una opcion valida");
            Console.WriteLine();
        }

    } while (segundoCorrecto == false);
    Console.Clear();

    Console.WriteLine("TERNAS DE TIPOS PARA POKEMONS INICIALES");
    Console.WriteLine();
    Console.WriteLine("NOTA: Para elegir un tipo, ingrese un numero entre el 1 y el 18 y presione ENTER");
    Console.WriteLine();
    Console.WriteLine("Primer Tipo: " + primero.NombreTipo);
    Console.WriteLine("Segundo Tipo: " + segundo.NombreTipo);
    Console.WriteLine();
    Console.WriteLine("LISTO");

    foreach (TipoPokemon t in TiposElementales)
    {
        contadorOpcion++;
        if (t.IdTipo == primero.IdTipo || t.IdTipo == segundo.IdTipo)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine(contadorOpcion + " - " + t.NombreTipo);
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(contadorOpcion + " - " + t.NombreTipo);
            Console.ResetColor();
        }
    }

    Console.WriteLine("Pulse cualquier tecla para continuar...");
    Console.ReadKey();
    Console.Clear();

    Console.WriteLine("TIPOS SELECIONADOS");
    Console.WriteLine(primero.NombreTipo + " --> " + segundo.NombreTipo + " --> ??? ");
    Console.WriteLine();

    List<TipoPokemon> posiblesTerceros = new List<TipoPokemon>();

    if (PrimerTipo_EsSuperEfectivoContra_SegundoTipo(primero, segundo) == true)
    {
        List<TipoPokemon> efectivos = Tipo_SuperEfectivoContra(segundo.IdTipo);
        foreach (TipoPokemon tp in efectivos)
        {
            if (tp.SuperEfectivo.Contains(primero.IdTipo) == true)
            {
                posiblesTerceros.Add(tp);
            }
        }

        Console.WriteLine("RESULTADOS: Posibles Tercer Tipo");

        if (posiblesTerceros.Count == 0)
        {
            Console.Write("No hay tercer tipo posible");
            Console.Clear();
        }
        else
        {
            Console.WriteLine(primero.NombreTipo + " --> " + segundo.NombreTipo + " --> " + posiblesTerceros[0].NombreTipo);

            for (int i = 1; i < posiblesTerceros.Count; i++)
            {
                Console.WriteLine("                     " + posiblesTerceros[i].NombreTipo);
            }
        }
    }
    else
    {
        List<TipoPokemon> efectivos = Tipo_SuperEfectivoContra(primero.IdTipo);
        foreach (TipoPokemon tp in efectivos)
        {
            if (tp.SuperEfectivo.Contains(segundo.IdTipo) == true)
            {
                posiblesTerceros.Add(tp);
            }
        }

        Console.WriteLine("RESULTADOS: Posibles Tercer Tipo");

        if (posiblesTerceros.Count == 0)
        {
            Console.WriteLine("No hay tercer tipo posible");
        }
        else
        {
            Console.WriteLine(primero.NombreTipo + " --> " + segundo.NombreTipo + " --> " + posiblesTerceros[0].NombreTipo);

            for (int i = 1; i < posiblesTerceros.Count; i++)
            {
                Console.WriteLine("                     " + posiblesTerceros[i].NombreTipo);
            }
        }
    } 
}

//----------------------------------------------------------------------

static void MenuOpciones(TipoPokemon primero, TipoPokemon segundo)
        {
            Console.WriteLine("TERNAS DE TIPOS PARA POKEMONS INICIALES");
            Console.WriteLine();
            int cantTipos = TiposElementales.Count;

            Console.WriteLine("NOTA: Para elegir un tipo, ingrese un numero entre el 1 y el 18 y presione ENTER");
            Console.WriteLine();
            Console.WriteLine("Primer Tipo: " + primero.NombreTipo);
            Console.WriteLine("Segundo Tipo: " + segundo.NombreTipo);
            Console.WriteLine();

            if (segundo.IdTipo > 0)
            {
                Console.WriteLine("LISTO");
            }
            else if(primero.IdTipo > 0)
            {
                Console.WriteLine("(Seleccione el segundo tipo)");
            }
            else
            {
                Console.WriteLine("(Seleccione el primer tipo)");
            }

            int contadorOpcion = 0;
            foreach (TipoPokemon t in TiposElementales)
            {
                contadorOpcion++;
                if (t.IdTipo == primero.IdTipo || t.IdTipo == segundo.IdTipo)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(contadorOpcion + " - " + t.NombreTipo);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(contadorOpcion + " - " + t.NombreTipo);
                }
            }
            Console.WriteLine();
            Console.Write("Ingrese un numero de tipo y ENTER: ");
        }


//----------------------------------------------------------------------

static void RepetirMenu(Action function)
{
    ConsoleKey opcion;

    do
    {
        //---------------------------

        function();

        //---------------------------
        Console.WriteLine("\n[Presione la tecla ESC para terminar]");
        opcion = Console.ReadKey(true).Key;
        Console.Clear();

    } while (opcion != ConsoleKey.Escape);

    Console.WriteLine("Programa Terminado, gracias por probar! Atte: Trinche");
}


//----------------------------------------------------------------------



}