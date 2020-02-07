using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace Etapa1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Las siguientes dos instrucciones asignan un delegado al evento de ProcessExit que es cuando
            //termina la ejecución del sistema, si no se remueve alguno de los delegados, el evento será
            //ejecutado tantas veces como delegados tenga asignados, en este ejemplo serian 2
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.WriteTitle("Terminando la ejecución 2");
            //La siguiente instrucción remueve el delegado al evento, removiendo solo se ejecutaria 1
            AppDomain.CurrentDomain.ProcessExit -= AccionDelEvento;
            
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
            var dic = engine.GetDiccionarioObjetos();
             engine.ImprimirDiccionario(dic, true);
            
            var rep = new Reporteador(engine.GetDiccionarioObjetos());
            // var rep = new Reporteador(null);
            rep.GetListaEvaluacion();
            var evalList = rep.GetListaEvaluacion();
            var listaAsig = rep.GetListaAsignatura();
            var listaEvalPorAsig = rep.GetDiccEvaluacionesPorAsignatura();
            var listaPromXAsig = rep.GetPromedioAlumnosPorAsignatura();

        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("Terminando la ejecución");
        }

        /// <SUMMARY>
        /// Comentarios del metodo
        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de la escuela");

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
