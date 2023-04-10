
using System.ComponentModel.DataAnnotations;


namespace Appets.WebUI.Models
{
    public class AlbergueViewModel
    {
        [Key]

        [Display(Name = "Id Albergue")]
        public int alberg_Id { get; set; }

        [Display(Name = "RTN")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string alberg_RTN { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string alberg_Nombre { get; set; }

        [Display(Name = "Ubicación")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string alberg_Ubicacion { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string alberg_Telefono { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string alberg_Correo { get; set; }

        [Display(Name = "Misión")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string alberg_Mision { get; set; }

        [Display(Name = "Información Adicional")]
        public string alberg_InformacionAdicion { get; set; }


    }
    
}
