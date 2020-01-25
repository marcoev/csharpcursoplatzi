using System;
using System.Collections.Generic;
using CoreEscuela.Entidades;
using static System.Console;

namespace Etapa1
{
    class Program
    {
        static void Main(string[] args)
        {
            var escuela = new Escuela("Platzi Academy", 2012, tipo: TiposEscuela.PreEscolar, ciudad: "Bogota");
            // Curso[] arregloCurso = {
            //     new Curso(){ Nombre = "101" },
            //     new Curso(){
            //         Nombre = "201"
            //     },
            //     new Curso(){
            //         Nombre = "301"
            //     }
            // }

            escuela.Pais = "Colombia";
            escuela.TipoEscuela = TiposEscuela.Primaria;
            escuela.Ciudad = "Bogota";

            // escuela.Cursos = new Curso[]{
            //     new Curso(){ Nombre = "101" },
            //     new Curso(){ Nombre = "201" },
            //     new Curso(){ Nombre = "301" }
            // };

            escuela.Cursos = new List<Curso>{
                new Curso(){ Nombre = "101" },
                new Curso(){ Nombre = "201" },
                new Curso(){ Nombre = "301" }
            };

            escuela.Cursos.Add(new Curso() { Nombre = "102", Jornada = TiposJornada.Mañana });

            var otraColeccion = new List<Curso>{
                new Curso(){ Nombre = "401" },
                new Curso(){ Nombre = "501" },
                new Curso() { Nombre = "501", Jornada = TiposJornada.Tarde },
                new Curso(){ Nombre = "502" }
            };

            Curso tmp = new Curso { Nombre = "101-Vacacional", Jornada = TiposJornada.Noche };
            //otraColeccion.Clear;
            escuela.Cursos.AddRange(otraColeccion);
            escuela.Cursos.Add(tmp);
            ImprimirCursosEscuela(escuela);

            //Predicate<Curso>cursoAEliminar= fnDelegaEliminarCurso;
            //Enviar una funcion como parametro
            //escuela.Cursos.RemoveAll(cursoAEliminar);

            //A partir de C# 6 se infiere que es una funcion, y no es necesario hacer la declaración anterior
            // escuela.Cursos.RemoveAll(fnDelegaEliminarCurso);

            //Ahora con un delegado
            escuela.Cursos.RemoveAll(delegate (Curso cur)
            {
                return cur.Nombre == "301";
            });

            //Ahora con expresion lambda, queda mas corto el codigo
            escuela.Cursos.RemoveAll((Curso cur) => cur.Nombre == "501" && cur.Jornada == TiposJornada.Tarde);

            WriteLine("Curso.Hash " + tmp.GetHashCode());
            escuela.Cursos.Remove(tmp);
            ImprimirCursosEscuela(escuela);


            /*
            Console.WriteLine(escuela);
            System.Console.WriteLine("=========");
            ImprimirCurso(escuela.Cursos);
            */
        }

        private static bool fnDelegaEliminarCurso(Curso obj)
        {
            return obj.Nombre == "301";
        }

        /// <SUMMARY>
        /// Comentarios del metodo
        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            WriteLine("====================");
            WriteLine("Cursos de la escuela");
            WriteLine("====================");

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
