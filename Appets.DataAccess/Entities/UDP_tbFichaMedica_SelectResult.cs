﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appets.DataAccess.Entities
{
    public partial class UDP_tbFichaMedica_SelectResult
    {
        public int medic_Id { get; set; }
        public int masc_Id { get; set; }
        public string masc_Nombre { get; set; }
        public bool medic_Esterilizacion { get; set; }
        public string medic_Personalidad { get; set; }
        public string medic_SaludCuidado { get; set; }
        public string medic_InformacionAdicional { get; set; }
    }
}
