using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class VoluntarioViewModel
    {
        [Key]
     
        [Display(Name = "Id Voluntario")]
        public int volun_Id { get; set; }

        [Display(Name = "Nombre Voluntario")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public int per_Id { get; set; }

        public SelectList VoluntarioList { get; set; }

        [Display(Name = "Fecha Ingreso")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime volun_FechaIngreso { get; set; }

        public void LlenarListas(IEnumerable<UDP_tbPersonas_EsVoluntarioResult> personas )
        {
            VoluntarioList = new SelectList(personas, "per_Id", "per_Nombres");
        }

    }
    
}
