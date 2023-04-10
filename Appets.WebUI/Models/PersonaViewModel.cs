
using System;
using System.ComponentModel.DataAnnotations;


namespace Appets.WebUI.Models
{
    public class PersonaViewModel
    {
        [Key]
        [Display(Name = "Id Persona")]
        public int per_Id { get; set; }

        [Display(Name = "Identidad")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string per_Identidad { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string per_Nombres { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string per_Apellidos { get; set; }

        [Display(Name = "Edad")]
        public int? per_Edad { get; set; }


        [Display(Name = "Fecha Nacimiento")]
        public DateTime? per_FechaNacimiento { get; set; }


        [Display(Name = "Domicilio")]
        public string per_Domicilio { get; set; }

        [Display(Name = "Teléfono")]
        public string per_Telefono { get; set; }

        [Display(Name = "Correo")]
        public string per_Correo { get; set; }

        [Display(Name = "Es Adoptante")]
        public bool per_EsAdoptante { get; set; }

        [Display(Name = "Es Donante")]
        public bool per_EsDonante { get; set; }

        [Display(Name = "Es Empleado")]
        public bool per_EsEmpleado { get; set; }

        [Display(Name = "Es Voluntario")]
        public bool per_EsVoluntario { get; set; }
    }
    
}
