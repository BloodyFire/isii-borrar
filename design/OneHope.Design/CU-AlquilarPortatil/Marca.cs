﻿using System.ComponentModel.DataAnnotations;

namespace OneHope.Design
{
    public class Marca
    {
        //Constructores
        public Marca() { }

        public Marca(int iD, string nombre) 
        {
            ID = iD;
            Nombre = nombre;
        }

        //Atributos
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "La marca no puede tener mas de 75 caracteres.")]
        public string Nombre { get; set;}

        public IList<Portatil> Portatiles { get; set; } = new List<Portatil>();

        //Metodos
        public override bool Equals(object? obj)
        {
            return obj is Marca proc && ID == proc.ID &&
                Nombre == proc.Nombre;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Nombre);
        }

    }
}
