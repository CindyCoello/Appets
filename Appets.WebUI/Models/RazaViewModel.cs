using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class RazaViewModel
    {
        [Key]
        [Display(Name = "Id Raza")]
        public int raza_Id { get; set; }

        [Display(Name = "Nombre Raza")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string raza_Descripcion { get; set; }
        [Display(Name = "Nombre Especie")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public int espc_Id { get; set; }

        public SelectList EspecieList { get; set; }
        public void LlenarLista(IEnumerable<tbEspecies> especies)
        {
            EspecieList = new SelectList(especies, "espc_Id", "espc_Descripcion");
        }
    }
    
}
