﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Appets.DataAccess.Entities
{
    public partial class tbComponentes
    {
        public tbComponentes()
        {
            tbModulos = new HashSet<tbModulos>();
        }

        public int comp_Id { get; set; }
        public string comp_Nombre { get; set; }

        public virtual ICollection<tbModulos> tbModulos { get; set; }
    }
}