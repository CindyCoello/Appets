using Appets.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Appets.WebUI.Models
{
    public class UsuariosViewModel
    {
        public EditarUsuario EditarUsuario { get; set; }

        public CambiarContraseña CambiarContraseña { get; set; }

        public UsuariosViewModel()
        {
            EditarUsuario = new EditarUsuario();
            CambiarContraseña = new CambiarContraseña();
        }

    }

    public class LoginViewModel
    {
        [Display (Name="Numero de Identidad")]
        [Required(ErrorMessage ="El Campo {0} es requerido")]
        [StringLength (13, ErrorMessage ="La longitud maxima es de {1}")]
        public string usu_Identidad { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [StringLength(15, ErrorMessage = "La longitud maxima es de {1}")]
        public string usu_Contraseña { get; set; }
    }


    public class EditarUsuario
    {


        [Display(Name = "Id Usuario")]
        public int? usu_Id { get; set; }

        [Display(Name = "Numero de Identidad")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        [StringLength(13, ErrorMessage = "La longitud maxima es de {1}")]
        public string usu_Identidad { get; set; }


        [Display(Name = "Primer Nombre")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string usu_PrimerNombre { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string usu_PrimerApellido { get; set; }

        [Display(Name = "Segundo Nombre")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string usu_SegundoNombre { get; set; }

        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string usu_SegundoApellido { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public string usu_Telefono { get; set; }

        [Display(Name = "Contraseña")]
        public string usu_Contraseña { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]

        public int rol_Id { get; set; }
        public SelectList Usuariolist { get; set; }
        [Display(Name = "Es Activo")]
        [Required(ErrorMessage = "El Campo {0} es requerido")]
        public bool usu_EsActivo { get; set; }


        public void LlenarLista(IEnumerable<tbRoles> roles)
        {
            Usuariolist = new SelectList(roles, "rol_Id", "rol_Nombre");
        }


    }


    public class CambiarContraseña
    {
        [Required]
        public int usu_Id { get; set; }
        public string usu_Identidad { get; set; }

        [DataType(DataType.Password)]

        [Display(Name = "Nueva Contraseña")]
        [Required(ErrorMessage = "Ingrese su nueva contraseña")]
        [StringLength(40, ErrorMessage = "La Contraseña debe tener un maximo de {1} caracteres.")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage ="La contraseña debe tener como minimo 8 caracteres, 1 letra mayuscula,1 letra minuscula y 1 numero.")]

        public string NuevaContraseña { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña.")]
        [Required(ErrorMessage = "Escriba nuevamente su contraseña")]
        [StringLength(40, ErrorMessage = "La Contraseña debe tener un maximo de {1} caracteres.")]
        [Compare(nameof(NuevaContraseña),ErrorMessage ="Las contraseñas no coinciden")]
        public string ConfirmarContraseña { get; set; }

    }
}
