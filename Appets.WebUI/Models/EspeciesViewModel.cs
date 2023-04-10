using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class EspeciesViewModel
    {
        [Key]
        [Display(Name = "Id Especie")]
        public int espc_Id { get; set; }

        [Display(Name = "Nombre Especie")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string espc_Descripcion { get; set; }


    }
    
}
