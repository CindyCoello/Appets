using Appets.DataAccess.Entities;
using Appets.DataAccess.Repositories;
using Appets.WebUI.Attribute;
using Appets.WebUI.Extensions;
using Appets.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Controllers
{
    public class UsuariosController : BaseController
    {
        private readonly UsuariosRepository _usuariosRepository;
        private readonly RolesRepository _rolesRepository;

        public UsuariosController(UsuariosRepository usuariosRepository, RolesRepository rolesRepository)
        {
            _usuariosRepository = usuariosRepository;
            _rolesRepository = rolesRepository;
        }
        [SessionManager("Listado de Usuarios")]
        public IActionResult Index()
        {
            var model = new UsuariosViewModel();
            model.EditarUsuario.LlenarLista(_rolesRepository.RolesList());

            return View(model);
        }

        //public IActionResult EditarUsuario()
        //{
        //    var model = new UsuariosViewModel();
        //    return View(model);
        //}

        [SessionManager("Crear Usuarios")]
        public IActionResult AgregarUsuario()
        {
            var model = new UsuariosViewModel();
            model.EditarUsuario.LlenarLista(_rolesRepository.RolesList());
            ViewBag.usu_Id = 0;
            return View(nameof(EditarUsuario),model);
        }
       
        public ActionResult UserList()
        {
           
            IEnumerable<UDP_tbUsuarios_SelectResult> lista = _usuariosRepository.UsuariosList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    usu_Id = x.usu_Id,
                    usu_Identidad = x.usu_Identidad,
                    usu_PrimerNombre = x.usu_PrimerNombre + " "+ x.usu_SegundoNombre +" "+ x.usu_PrimerApellido + " " + x.usu_SegundoApellido,
                    usu_Telefono = x.usu_Telefono,
                    usu_Contraseña = x.usu_Contraseña,
                    rol_Id = x.rol_Nombre,
                    usu_EsActivo = x.usu_EsActivo
                })
                .OrderBy(x => x.usu_Id)
            });
        }


        public IActionResult CambiarContraseña(CambiarContraseña model)
        {

            if (ModelState.IsValid)
            {
                var result = new tbUsuarios();

                    ViewBag.usu_Id = model.usu_Id;
                    ViewBag.usu_Identidad = model.usu_Identidad;
                    result = _usuariosRepository.ActualizarContraseña(model.NuevaContraseña, model.usu_Id, model.usu_Identidad);
                    if (result == null)
                    {
                        ShowAlert("Contraseña Actualizada Correctamente", AlertMessageType.Success);
                        return RedirectToAction("Index");

                    }

                    else
                    {

                        goto error;
                    }

                error:
                    model.usu_Id = 0;
                    ViewBag.usu_Id = 0;

                    ShowAlert("Por favor ingrese lo solicitado.", AlertMessageType.Info);
                    return View(model);

            }
 
            return RedirectToAction("Index");
        }




        //[HttpPost("usuarios/listado-usuarios", Name = "EditarUsuario")]

        public IActionResult EditarUsuario1(EditarUsuario model)
        {

            
            if (ModelState.IsValid)
            {
                int result = 0;
                ViewBag.usu_Id = model.usu_Id;
                if (model.usu_Id == 0)
                {

                    result = _usuariosRepository.Insert(
                        model.usu_Identidad,
                        model.usu_PrimerNombre,
                        model.usu_PrimerApellido,
                        model.usu_SegundoNombre,
                        model.usu_SegundoApellido,
                        model.usu_Telefono,
                        model.usu_Contraseña,
                        model.rol_Id);

                    if (result != -1)
                    {
                        ShowAlert("Registro Ingresado Correctamente", AlertMessageType.Success);
                        return RedirectToAction("Index");

                    }

                    else
                    {

                        goto error;
                    }

                }
                else
                {
                    result = _usuariosRepository.Update(
                        ViewBag.usu_Id,
                        model.usu_Identidad,
                        model.usu_PrimerNombre,
                        model.usu_PrimerApellido,
                        model.usu_SegundoNombre,
                        model.usu_SegundoApellido,
                        model.usu_Telefono,
                        model.rol_Id);
                    if (result != -1)
                    {
                        ShowAlert("Registro actualizado Correctamente", AlertMessageType.Success);
                        return RedirectToAction("Index");

                    }
                    else
                    {

                        goto error;
                    }
                }
            error:
                model.usu_Id = 0;
                ViewBag.usu_Id = 0;

                ShowAlert("Por favor ingrese lo solicitado.", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }


        [SessionManager("Editar Usuarios")]
        public IActionResult EditarUsuario(int id)
        {
            var usuario = _usuariosRepository.Find(id);
            if (usuario == null)
            {
                return View(nameof(Index));
            }

            var model = new UsuariosViewModel();
            
            ViewBag.usu_Id = id;
            model.CambiarContraseña.usu_Id= id;
            model.CambiarContraseña.usu_Identidad = usuario.usu_Identidad;
            model.EditarUsuario.usu_Id = id;
            model.EditarUsuario.usu_Identidad = usuario.usu_Identidad;
            model.EditarUsuario.usu_PrimerNombre = usuario.usu_PrimerNombre;
            model.EditarUsuario.usu_PrimerApellido = usuario.usu_PrimerApellido;
            model.EditarUsuario.usu_SegundoNombre = usuario.usu_SegundoNombre;
            model.EditarUsuario.usu_SegundoApellido = usuario.usu_SegundoApellido;
            model.EditarUsuario.usu_Telefono = usuario.usu_Telefono;
            model.EditarUsuario.rol_Id = usuario.rol_Id;
            model.EditarUsuario.LlenarLista(_rolesRepository.RolesList());
            return View(model);
        }

    }
}
