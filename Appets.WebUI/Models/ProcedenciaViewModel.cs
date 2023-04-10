using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class ProcedenciaViewModel
    {
        [Key]
        [Display(Name = "Id Procedencia")]
        public int proc_Id { get; set; }

        [Display(Name = "Descripcion Procedencia")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string proc_Descripcion { get; set; }

    }
    
}
