using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class FichaAdopcionViewModel
    {
        [Key]
        [Display(Name = "Id")]
        public int ficha_Id { get; set; }

        [Display(Name = "Nombre Mascota")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int masc_Id { get; set; }
        public SelectList MascotaList { get; set; }

        [Display(Name = "Nombre Adoptante")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int per_Id { get; set; }
        public SelectList AdoptanteList { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public System.DateTime ficha_FechaRegistro { get; set; }

        public void LlenarListas(IEnumerable<UDP_tbMascotas_SelectResult> mascotas)
        {
            MascotaList = new SelectList(mascotas, "masc_Id", "masc_Nombre");
        }


        public void LlenarLista(IEnumerable<UDP_tbPersonas_EsAdoptanteResult> personas)
        {
            AdoptanteList = new SelectList(personas, "per_Id", "per_Nombres");
        }
    }
    
}







