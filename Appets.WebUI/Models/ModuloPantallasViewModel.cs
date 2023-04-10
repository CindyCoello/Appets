using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class ModuloPantallasViewModel
    {

            [Key]
            [Display(Name = "Id")]
            public int modpan_Id { get; set; }
            [Display(Name = "Modulo")]
            [Required(ErrorMessage = "El campo {0} es requerido")]
            public int mod_Id { get; set; }
            public SelectList ModulosList { get; set; }
            [Display(Name = "Pantalla")]
            [Required(ErrorMessage = "El campo {0} es requerido")]
            public string modpan_Nombre { get; set; }

            public void LlenarLista(IEnumerable<UDP_tbModulos_SelectResult> modulos)
            {
                ModulosList = new SelectList(modulos, "mod_Id", "mod_Nombre");
            }
        

    }

}


