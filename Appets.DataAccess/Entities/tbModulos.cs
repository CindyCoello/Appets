﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Appets.DataAccess.Entities
{
    public partial class tbModulos
    {
        public tbModulos()
        {
            tbModuloPantallas = new HashSet<tbModuloPantallas>();
        }

        public int mod_Id { get; set; }
        public int comp_Id { get; set; }
        public string mod_Nombre { get; set; }

        public virtual tbComponentes comp { get; set; }
        public virtual ICollection<tbModuloPantallas> tbModuloPantallas { get; set; }
    }
}