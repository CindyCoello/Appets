using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appets.DataAccess.Entities;
using Appets.DataAccess.Repositories;
using Appets.WebUI.Attribute;
using Appets.WebUI.Extensions;
using Appets.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Appets.WebUI.Controllers
{
    public class ModuloPantallasController : BaseController
    {
        private readonly ModuloPantallaRepository _modulosPantallasRepository;
        private readonly ModuloRepository _modulosRepository;
        public ModuloPantallasController(ModuloPantallaRepository modulosPantallasRepository,
            ModuloRepository modulosRepository)
        {
            _modulosPantallasRepository = modulosPantallasRepository;
            _modulosRepository = modulosRepository;
        }
        [SessionManager("Listado Pantallas")]
        public IActionResult Index()
        {
            var model = new ModuloPantallasViewModel();
            model.LlenarLista(_modulosRepository.ModuloList());
            return View(model);
        }

        [SessionManager("Listado Pantallas")]
        public IActionResult ListModuloP()
        {
            IEnumerable<UDP_tbModuloPantallas_SelectResult> lista = _modulosPantallasRepository.ListModuloPantallas();

            return Json(new
            {


                data = lista.Select(x => new {

                    modpan_Id = x.modpan_Id,
                    mod_Id = x.mod_Nombre,
                    modpan_Nombre = x.modpan_Nombre

                })
                .OrderBy(x => x.modpan_Id)
            });
        }

        [SessionManager("Crear Pantallas")]
        [HttpPost("modulopantallas/listado-modulopantallas", Name = "SaveModuloPantallas")]
        public IActionResult EditarModulos(ModuloPantallasViewModel model)
        {
            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.modpan_Id == 0)
                {
                    result = _modulosPantallasRepository.Insert(model.mod_Id, model.modpan_Nombre);
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
                    result = _modulosPantallasRepository.Update(model.modpan_Id, model.mod_Id, model.modpan_Nombre);
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
                model.modpan_Id = 0;
                ViewBag.modpan_Id = 0;
                ViewBag.mod_Id = 0;
                ViewBag.modpan_Nombre = 0;
                ShowAlert("Por favor ingrese lo solicitado", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }

        [SessionManager("Editar Pantallas")]
        public IActionResult GetPantalla(int id)
        {
            var pantalla = _modulosPantallasRepository.Find(id);
            if (pantalla == null)
            {
                return View(nameof(Index));
            }

            var model = new ModuloPantallasViewModel();
            ViewBag.modpan_Id = id;
            model.modpan_Id = id;
            model.mod_Id = (int)pantalla.mod_Id;
            model.modpan_Nombre = pantalla.modpan_Nombre;
            return AjaxResult(pantalla, true);
        }
    }
}