using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;

namespace CoreEscuela
{
    public sealed class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public EscuelaEngine()
        {

        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
                out int conteoEvaluaciones,
                out int conteoCursos,
                out int conteoAsignaturas,
                out int conteoAlumnos,
                bool traeEvaluaciones = true,
                bool traeAlumnos = true,
                bool traeAsignaturas = true,
                bool traeCursos = true
            )
        {
            conteoEvaluaciones = conteoCursos = conteoAsignaturas = conteoAlumnos = 0;
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);

            if (traeCursos)
            {
                listaObj.AddRange(Escuela.Cursos);
                conteoCursos = Escuela.Cursos.Count;
            }


            foreach (var curso in Escuela.Cursos)
            {
                if (traeAsignaturas)
                {
                    listaObj.AddRange(curso.Asignaturas);
                    conteoAsignaturas+=curso.Asignaturas.Count;
                }
                if (traeAlumnos)
                {
                    listaObj.AddRange(curso.Alumnos);
                    conteoAlumnos+=curso.Alumnos.Count;
                }

                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }

            return listaObj.AsReadOnly();
        }        
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
                out int conteoEvaluaciones
            )
        {
            return GetObjetosEscuela (out conteoEvaluaciones, out int dummy, out dummy, out dummy);
        }   
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            return GetObjetosEscuela (out int dummy, out dummy, out dummy, out dummy);
        }        
        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, tipo: TiposEscuela.PreEscolar, ciudad: "Bogota");
            Escuela.Pais = "Colombia";
            Escuela.TipoEscuela = TiposEscuela.Primaria;
            Escuela.Ciudad = "Bogota";
            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }
        public Double GetCalificacionAleatoria(Double minimum = 0.0, Double maximum = 5.0)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        public Dictionary<string, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos(){
            var diccionario = new Dictionary<string, IEnumerable<ObjetoEscuelaBase>>();
            diccionario.Add("Escuela", new[] {Escuela});
            diccionario.Add("Cursos", Escuela.Cursos);
            return diccionario;

        }
        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipe", "Eusebio", "Farid", "Marco", "Alvaro", "Donald" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Esparza", "Trump", "Toledo", "Villa" };
            string[] nombre2 = { "Fredo", "Beto", "Rick", "Murty", "Silvana", "Teodoro", "" };

            var listaAlumno = from n1 in nombre1
                              from n2 in nombre2
                              from a1 in apellido1
                              select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumno.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }
        #region Metodos de carga
        private void CargarEvaluaciones()
        {
            string[] exams = { "Parcial 1", "Parcial 2", "Parcial 3", "Examen final", "Examen regularizacion" };

            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        Random random = new Random(System.Environment.TickCount);

                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluacion()
                            {
                                Alumno = alumno,
                                Asignatura = asignatura,
                                Calificacion = (float)(5 * random.NextDouble()),
                                Nombre = $"{asignatura.Nombre} - {exams[i]}"
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }

            }
        }
        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                List<Asignatura> listaAsignaturas = new List<Asignatura>(){
                    new Asignatura{Nombre="Matemáticas"},
                    new Asignatura{Nombre="Educación Física"},
                    new Asignatura{Nombre="Castellano"},
                    new Asignatura{Nombre="Ciencas Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }
        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>{
                new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                new Curso(){ Nombre = "201", Jornada = TiposJornada.Mañana  },
                new Curso(){ Nombre = "301", Jornada = TiposJornada.Mañana  },
                new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde  },
                new Curso() { Nombre = "501", Jornada = TiposJornada.Tarde }
            };

            Random rnd = new Random();
            foreach (var curso in Escuela.Cursos)
            {
                int cantidadRandom = rnd.Next(5, 20);
                curso.Alumnos = GenerarAlumnosAlAzar(cantidadRandom);
            }
        }
        #endregion
    }

}