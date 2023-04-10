using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class OcupacionViewModel
    {
        [Key]
        [Display(Name = "Id ")]
        public int ocup_Id { get; set; }

        [Display(Name = "Ocupación")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string ocup_Descripcion { get; set; }

    }
    
}
