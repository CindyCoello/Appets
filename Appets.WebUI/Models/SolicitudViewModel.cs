using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class SolicitudViewModel
    {
        [Key]
        [Display(Name = "Id")]
        public int solic_Id { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string solic_Correo { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string solic_NombreCompleto { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime solic_Fecha { get; set; }

        [Display(Name = "Nombre Mascota")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int masc_Id { get; set; }

        public SelectList MascotaList { get; set; }


        public void LlenarListas(IEnumerable<UDP_tbMascotas_SelectResult> mascotas)
        {
            MascotaList = new SelectList(mascotas, "masc_Id", "masc_Nombre");
        }
    }

}


