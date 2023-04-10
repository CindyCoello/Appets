using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class ModuloViewModel
    {
        [Key]
        [Display(Name = "Id")]
        public int mod_Id { get; set; }

        [Display(Name = "Id componente")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int comp_Id { get; set; }

        public SelectList ComponenteList { get; set; }

        [Display(Name = "Nombre Modulo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string mod_Nombre { get; set; }

        public void LlenarListas(IEnumerable<tbComponentes> componentes)
        {
            ComponenteList = new SelectList(componentes, "comp_Id", "comp_Nombre");
        }
    }

}


