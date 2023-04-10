using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class DonanteViewModel
    {
        [Key]

        [Display(Name = "Id Donante")]
        public int don_Id { get; set; }

        [Display(Name = "Nombre Donante")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public int per_Id { get; set; }
        public SelectList DonanteList { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Donación")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]

        public System.DateTime don_Fecha { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string don_Descripcion { get; set; }


        public void LlenarListas(IEnumerable<UDP_tbPersonas_EsDonanteResult> personas)
        {
            DonanteList = new SelectList(personas, "per_Id", "per_Nombres");
        }

    }
    
}
