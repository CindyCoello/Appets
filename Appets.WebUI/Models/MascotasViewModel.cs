using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Models
{
    public class MascotasViewModel
    {

        //[HiddenInput]
        //public int masc_Id { get; set; }

        //[Display]
        //[Required(ErrorMessage = "Seleccione una imagen de su computadora.")]
        //public IFormFile ImageFile { get; set; }

        public AccionMascota AccionMascota { get; set; }
        public CambiarImagen CambiarImagen { get; set; }

        public MascotasViewModel()
        {
            CambiarImagen = new CambiarImagen();
            AccionMascota = new AccionMascota();
        }


    }

    public class CambiarImagen
    {
        [HiddenInput]
        public int masc_Id { get; set; }
        public string MascotaPriview { get; set; }

        public string nombreMascota { get; set; }
        [Display]
        [Required(ErrorMessage = "Seleccione una imagen de su computadora.")]
        public IFormFile ImageFile { get; set; }
    }

    public class AccionMascota
    {

       
        [Display(Name = "Id")]
        public int? masc_Id { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string masc_Imagen { get; set; }

        [Display(Name = "Nombre Mascota")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string masc_Nombre { get; set; }

        [Display(Name = "Especie")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int espc_Id { get; set; }
        public SelectList Especielist { get; set; }

        [Display(Name = "Raza")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int raza_Id { get; set; }
        public SelectList Razalist { get; set; }

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int masc_Edad { get; set; }


        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string masc_Sexo { get; set; }


        [Display(Name = "Peso")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string masc_Peso { get; set; }

        [Display(Name = "Talla")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string masc_Talla { get; set; }


        [Display(Name = "Color")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string masc_Color { get; set; }

        [Display(Name = "Empleado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int emp_Id { get; set; }

        public SelectList Empleadolist { get; set; }
        [Display(Name = "Es Adoptado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool masc_EsAdoptado { get; set; }


        [Display(Name = "Es Reservado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool masc_EsReservado { get; set; }


        [Display(Name = "Historial")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string masc_HistorialDescripcion { get; set; }


        [Display(Name = "Procedencia")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int proc_Id { get; set; }
        public SelectList Procedencialist { get; set; }



        public void LlenarListas(IEnumerable<tbEspecies> especies)
        {
            Especielist = new SelectList(especies, "espc_Id", "espc_Descripcion");
        }

        public void LlenarListas(IEnumerable<UDP_tbRazas_SelectResult> razas)
        {
            Razalist = new SelectList(razas, "raza_Id", "raza_Descripcion");
        }

        public void LlenarListas(IEnumerable<UDP_tbPersonas_EsEmpleadoResult> personas)
        {
            Empleadolist = new SelectList(personas, "per_Id", "per_Nombres");
        }

        public void LlenarListas(IEnumerable<tbProcedencia> procedencias)
        {
            Procedencialist = new SelectList(procedencias, "proc_Id", "proc_Descripcion");
        }
    }






 
}
