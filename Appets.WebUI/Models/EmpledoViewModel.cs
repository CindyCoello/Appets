using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class EmpleadoViewModel
    {
        [Key]
        [Display(Name = "Id")]
        public int emp_Id { get; set; }

        [Display(Name = "Nombre Empleado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int per_Id { get; set; }

        public SelectList EmpleadoList { get; set; }

        [Display(Name = "Ocupación")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ocup_Id { get; set; }

        public SelectList OcupacionList { get; set; }

        public void LlenarListas(IEnumerable<UDP_tbPersonas_EsEmpleadoResult> personas)
        {
            EmpleadoList = new SelectList(personas, "per_Id", "per_Nombres");
        }

        public void LlenarLista(IEnumerable<tbOcupaciones> ocupaciones)
        {
            OcupacionList = new SelectList(ocupaciones, "ocup_Id", "ocup_Descripcion");
        }

    }

}




