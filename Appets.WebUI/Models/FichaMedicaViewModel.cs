
using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Appets.WebUI.Models
{
    public class FichaMedicaViewModel
    {
        [Key]

        [Display(Name = "Id Donante")]
        public int medic_Id { get; set; }

        [Display(Name = "Nombre Mascota")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public int masc_Id { get; set; }
        public SelectList Fichalist { get; set; }
        [Display(Name = "Esterialización")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public bool medic_Esterilizacion { get; set; }

        [Display(Name = "Personalidad")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string medic_Personalidad { get; set; }

        [Display(Name = "Salud Cuidado")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string medic_SaludCuidado { get; set; }
        [Display(Name = "Informacón Adicional")]
        
        public string medic_InformacionAdicional { get; set; }



        public void LlenarListas(IEnumerable<UDP_tbMascotas_SelectResult> mascotas )
        {
            Fichalist = new SelectList(mascotas, "masc_Id", "masc_Nombre");
        }
    }
    
}
