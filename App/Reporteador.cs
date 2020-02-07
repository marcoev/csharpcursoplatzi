using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicEscObj)
        {
            if (dicEscObj is null){
                throw new ArgumentNullException(nameof(dicEscObj));
            }
            else
            {
                _diccionario = dicEscObj;    
            }
            
        }

        public IEnumerable<Evaluacion> GetListaEvaluacion(){
            //var lista = _diccionario.GetValueOrDefault(LlaveDiccionario.Escuela);
            IEnumerable<Evaluacion> res = null;
            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluacion, out IEnumerable<ObjetoEscuelaBase> lista)){
                res = lista.Cast<Evaluacion>();
            }
            return res;
        }
        // public IEnumerable<Asignatura> GetListaAsignatura(){
        public IEnumerable<string> GetListaAsignatura(out IEnumerable<Evaluacion> listaEvaluaciones){
            listaEvaluaciones = GetListaEvaluacion();
            //utilizamos linq para regresar la lista donde la calificacion sea mayor a 3
            // return from Evaluacion ev in listaEvaluaciones
            //     where ev.Calificacion>3.0f
            //     select ev.Asignatura;
            // return (from Evaluacion ev in listaEvaluaciones
            //     select ev.Asignatura).Distinct();
            return (from Evaluacion ev in listaEvaluaciones
                select ev.Asignatura.Nombre).Distinct();
        }
        public IEnumerable<string> GetListaAsignatura(){            
            return GetListaAsignatura(out var dummy);
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDiccEvaluacionesPorAsignatura(){
            var dicResp = new Dictionary<string, IEnumerable<Evaluacion>>();
            var listaAsignaturas = GetListaAsignatura(out var listaEval);

            foreach (var asignatura in listaAsignaturas)
            {
                var evalAsig = from eval in listaEval
                               where eval.Asignatura.Nombre == asignatura
                               select eval;                                
                dicResp.Add(asignatura, evalAsig);
            }
            return dicResp;
        }

        public Dictionary<string, IEnumerable<object>> GetPromedioAlumnosPorAsignatura(){
            var resp = new Dictionary<string, IEnumerable<object>>();
            var diccEvalPorAsig = GetDiccEvaluacionesPorAsignatura();

            foreach (var asignaturConEvaluacion in diccEvalPorAsig)
            {
                //linq solo permite regresar un valor, es por ello que se crea un objeto anonimo el cual contendrá mas de un valor por registro
                var dummy = from eval in asignaturConEvaluacion.Value
                            select new {
                                eval.Alumno.UniqueId,
                                AlumnoNombre = eval.Alumno.Nombre,
                                EvaluacionNombre = eval.Nombre,
                                eval.Calificacion
                                };

            }

            return resp;
        }
    }
}