﻿using System;
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

            var listaObjetos = engine.GetObjetosEscuela(traeEvaluaciones:true);
            // var listaILugar = from obj in listaObjetos
            //                     where obj is ILugar
            //                   select (ILugar) obj;
            //engine.Escuela.LimpiarLugar();
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
