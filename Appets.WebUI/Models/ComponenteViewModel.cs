using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class ComponenteViewModel
    {

        [Key]
        [Display(Name = "Id Componente")]
        public int comp_Id { get; set; }
        [Display(Name = "Nombre Componente")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string comp_Nombre { get; set; }

    }

}
