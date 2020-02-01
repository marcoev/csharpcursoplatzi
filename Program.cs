using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace Etapa1
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            ImprimirCursosEscuela(engine.Escuela);

            Dictionary<int, string> diccionario = new Dictionary<int, string>();

            diccionario.Add(12, "Juan K");
            diccionario.Add(13, "Lorem Ipsum");

            foreach (var keyValPair in diccionario)
            {
                WriteLine($"Key: {keyValPair.Key} Valor: {keyValPair.Value}");
            }

            Printer.WriteTitle("Acceso a diccionario");
            WriteLine(diccionario[13]);
            diccionario[0] = "Pekerman";
            WriteLine(diccionario[0]);
            Printer.WriteTitle("Otro Diccionario");
            var dicc = new Dictionary<string, string>();
            // El resultado de la siguiente instruccion es que crea la llave si no existe, o la modifica
            dicc["luna"] = "Cuerpo celeste que gira alrededor de la tierra";
            WriteLine(dicc["luna"]);
            //La siguiente instruccion es valida ya que modifica el valor de la llave
            dicc["luna"] = "La protagonista de soy luna";
            WriteLine(dicc["luna"]);
            //en la siguiente instruccion da como resultado una excepcion, ya que esta intentando crear una llave que ya existe
            dicc.Add("luna","Otro valor de luna");
        }

        /// <SUMMARY>
        /// Comentarios del metodo
        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            //Printer.DibujarLinea();
            Printer.WriteTitle("Cursos de la escuela");
            //Printer.DibujarLinea(caracter:'*');

            // if (escuela != null && escuela.Cursos != null)
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    Console.WriteLine($"Curso {curso.Nombre}, Id {curso.UniqueId}");
                }
            }
        }

        private static void ImprimirCurso(Curso[] arregloCurso)
        {
            foreach (var item in arregloCurso)
            {
                Console.WriteLine($"{item.Nombre}, {item.UniqueId}");
            }
        }
    }
}
