using System;

namespace CoreEscuela.Entidades
{
    public class Evaluacion
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }
        public Alumno Alumno { get; set; }
        public Asignatura Asignatura { get; set; }
        public float Calificacion { get; set; }

        public Evaluacion() => UniqueId = Guid.NewGuid().ToString();

    }
}