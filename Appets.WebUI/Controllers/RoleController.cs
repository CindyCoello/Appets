using Appets.DataAccess.Entities;
using Appets.DataAccess.Repositories;
using Appets.WebUI.Attribute;
using Appets.WebUI.Extensions;
using Appets.WebUI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Controllers
{
    public class RoleController : BaseController
    {

        private readonly ModuloRepository _moduloRepository;
        private readonly ComponenteRepository _componenteRepository;
        private readonly ModuloPantallaRepository _moduloPantallaRepository;
        private readonly RolesRepository _rolesRepository;
        private readonly UsuariosRepository _usuariosRepository;
        private readonly IMapper _mapper;

          public RoleController(ModuloRepository moduloRepository,
                ComponenteRepository componenteRepository,
                ModuloPantallaRepository moduloPantallaRepository,
                RolesRepository rolesRepository,
                UsuariosRepository usuariosRepository,
                IMapper mapper)

          {
                _moduloRepository = moduloRepository;
                _componenteRepository = componenteRepository;
                _moduloPantallaRepository = moduloPantallaRepository;
                _rolesRepository = rolesRepository;
                _usuariosRepository = usuariosRepository;
                _mapper = mapper;
          }
        [SessionManager("Listado de Roles")]
       
       
       
        [HttpGet("seguridad/lista-roles")]

        public IActionResult Roles()
        {
            return View();
        }

        [SessionManager("Crear Roles")]
        public ActionResult AgregarRol()
        {
            var model = new RoleViewModel();

            model.LoadTreeViewData(_componenteRepository.ComponentesList(),
                _moduloRepository.ModuloList(),
                _moduloPantallaRepository.ListModuloPantallas());
            ViewBag.rol_Id = 0;
            return View(nameof(EditarRol), model);
        }



        public ActionResult CreateRole()
        {
            IEnumerable<UDP_tbUsuarios_SelectResult> listRoles = _usuariosRepository.UsuariosList();
            return Json(new
            {
                data = listRoles.Select(x => new
                {
                    rol_Id = x.rol_Id,
                    rol_Nombre = x.rol_Nombre,
                    rol_EsActivo = x.rol_Id

                })
                  .OrderBy(x => x.rol_Id)
            });


        }
        [SessionManager("Listado de Roles")]
        public ActionResult ListRoles()
        {
            IEnumerable<tbRoles> listRoles = _rolesRepository.RolesList();
            return Json(new
            {
                data = listRoles.Select(x => new
                {
                    rol_Id = x.rol_Id,
                    rol_Nombre = x.rol_Nombre,
                    rol_EsActivo = x.rol_EsActivo

                })
                .OrderBy(x => x.rol_Id)
            });


        }


        [SessionManager("Editar Roles")]
        public ActionResult EditarRol(int id)
        {
            var result = _rolesRepository.RolesList()
                .FirstOrDefault(x => x.rol_Id == id);

            if (result == null)
            {
                return View(nameof(Index));
            }


            var model = new RoleViewModel();
            ViewBag.rol_Id = id;
            model.rol_Id = id;
            model.rol_Nombre = result.rol_Nombre;
            model.rol_EsActivo = result.rol_EsActivo;
            model.LoadList(_moduloRepository.ListModuloPantallas(id)
                .Select(x => x.modpan_Id));
            model.LoadTreeViewData(_componenteRepository.ComponentesList(),
               _moduloRepository.ModuloList(),
               _moduloPantallaRepository.ListModuloPantallas());
            model.ParseTreeViewData();
            return View(model);
        }


        [HttpPost("Seguridad/role", Name = "SaveRole")]
       

        public ActionResult EditarRol(RoleViewModel model)
        {
            int result = 0;
            model.ParseTreeViewData();
            var role = _mapper.Map<tbRoles>(model);
                       
            if (model.rol_Id == 0)
            {
                if (!model.ModuleIdList.Any())
                {
                    goto error;
                    
                }


                result = _rolesRepository.Insert(role, model.ModuleIdList);
                if (result != -1)
                {
                    ShowAlert("Rol ingresado Correctamente", AlertMessageType.Success);
                    return RedirectToAction("Roles");

                }

                else
                {
                    
                    goto error;
                }


                 error:
                    model.LoadTreeViewData(_componenteRepository.ComponentesList(),
                        _moduloRepository.ModuloList(),
                        _moduloPantallaRepository.ListModuloPantallas());
                    ViewBag.rol_Id = 0;
                    ShowAlert("Por favor Seleccione al menos una pantalla.", AlertMessageType.Info);
                    //return RedirectToAction(nameof(EditarRol));
                    return View(model);

            }
            else
            {
                result = _rolesRepository.Update(role, model.ModuleIdList);
                if (result != -1)
                {
                    ShowAlert("Rol actualizado Correctamente", AlertMessageType.Success);
                   

                }
                return RedirectToAction("Roles");
            } 
               
        }

    }
}

