using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Escuela
    {
        string nombre;
        public string Nombre { 
            get { return nombre; }
            set { nombre = value.ToUpper(); }
        }

        public int AñoDeCreacion { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public TiposEscuela TipoEscuela { get; set; }
        // public Curso[] Cursos { get; set; }
        public List<Curso> Cursos { get; set; }

        public Escuela(string nombre, int año) => (Nombre, AñoDeCreacion) = (nombre, año);

        public Escuela(string nombre, int año, TiposEscuela tipo, string pais = "", string ciudad = ""){
            //Asignación por tuplas
            (Nombre, AñoDeCreacion) = (nombre, año);
            this.Pais = pais;
            this.Ciudad = ciudad;
        }

        public override string ToString(){
            return $"Nombre: {Nombre}, Tipo: {TipoEscuela} \nPais: {Pais}, Ciudad: {Ciudad}";
        }
    }
}