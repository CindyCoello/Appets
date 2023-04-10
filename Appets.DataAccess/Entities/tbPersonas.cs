﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Appets.DataAccess.Entities
{
    public partial class tbPersonas
    {
        public tbPersonas()
        {
            tbDonantes = new HashSet<tbDonantes>();
            tbFichaAdopcion = new HashSet<tbFichaAdopcion>();
            tbVoluntarios = new HashSet<tbVoluntarios>();
        }

        public int per_Id { get; set; }
        public string per_Identidad { get; set; }
        public string per_Nombres { get; set; }
        public string per_Apellidos { get; set; }
        public int? per_Edad { get; set; }
        public DateTime? per_FechaNacimiento { get; set; }
        public string per_Domicilio { get; set; }
        public string per_Telefono { get; set; }
        public string per_Correo { get; set; }
        public bool per_EsAdoptante { get; set; }
        public bool per_EsDonante { get; set; }
        public bool per_EsEmpleado { get; set; }
        public bool per_EsVoluntario { get; set; }
        public bool EsEliminado { get; set; }

        public virtual ICollection<tbDonantes> tbDonantes { get; set; }
        public virtual ICollection<tbFichaAdopcion> tbFichaAdopcion { get; set; }
        public virtual ICollection<tbVoluntarios> tbVoluntarios { get; set; }
    }
}