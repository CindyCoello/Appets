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
    public class ModuloController : BaseController
    {
        private readonly ComponenteRepository _componenteRepository;
        private readonly ModuloRepository _moduloRepository;
       
        public ModuloController(ModuloRepository moduloRepository,
           ComponenteRepository componenteRepository )
        {
            _moduloRepository = moduloRepository;
            _componenteRepository = componenteRepository;
        }
        //[SessionManager("Listado Modulos")]
        public IActionResult Index()
        {
            var model = new ModuloViewModel();
            model.LlenarListas(_componenteRepository.ComponentesList());
            return View(model);
        }


        //[SessionManager("Listado Modulos")]
        public IActionResult ModuloList()
        {
            IEnumerable<UDP_tbModulos_SelectResult> lista = _moduloRepository.ModuloList();
            return Json(new
            {

                data = lista.Select(x => new
                {
                    mod_Id = x.mod_Id,
                    comp_Id = x.comp_Nombre,
                    mod_Nombre = x.mod_Nombre

                })
                .OrderBy(x => x.mod_Id)
            }); 
        }


        //[SessionManager("Crear Modulos")]
        [HttpPost("modulos/listado-modulos", Name = "SaveModulo")]

        public IActionResult EditarEspecie(ModuloViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.mod_Id == 0)
                {

                    result = _moduloRepository.Insert(model.comp_Id , model.mod_Nombre);
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
                    result = _moduloRepository.Update(model.mod_Id,model.comp_Id, model.mod_Nombre);
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
                model.mod_Id = 0;
                ViewBag.comp_Id = 0;
                ViewBag.mod_Nombre = 0;
                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }


         //[SessionManager("Editar Modulos")]
        public IActionResult GetModulo(int id)
        {
            var modulo = _moduloRepository.Find(id);
            if (modulo == null)
            {
                return View(nameof(Index));
            }

            var model = new ModuloViewModel();
            ViewBag.mod_Id = id;
            model.mod_Id = id;
            model.comp_Id = modulo.comp_Id;
            model.mod_Nombre = modulo.mod_Nombre;
            return AjaxResult(modulo, true);
        }



    }
}
