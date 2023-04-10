using Microsoft.AspNetCore.Mvc;
using Appets.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appets.WebUI.Models;
using Appets.WebUI.Extensions;
using Appets.DataAccess.Entities;
using Appets.WebUI.Attribute;

namespace Appets.WebUI.Controllers
{
    public class ComponenteController : BaseController
    {

        private readonly ComponenteRepository _componenteRepository;
      

        public ComponenteController(ComponenteRepository componenteRepository)
        {
            _componenteRepository = componenteRepository;
        }

        [SessionManager("Listado de Componentes")]
        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult CompList()
        {
          
            IEnumerable<tbComponentes> lista = _componenteRepository.ComponentesList();
            return Json(new
            {

                data = lista.Select(x => new
                {
                    comp_Id = x.comp_Id,
                    comp_Nombre = x.comp_Nombre
                    
                })
                .OrderBy(x => x.comp_Id)
            });
        }

        [SessionManager("Crear Componentes")]

        [HttpPost("componente/listado-componentes", Name = "SaveComponente")]

        public IActionResult EditarEspecie(ComponenteViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.comp_Id == 0)
                {

                    result = _componenteRepository.Insert(model.comp_Nombre);
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
                    result = _componenteRepository.Update(model.comp_Id, model.comp_Nombre);
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
                model.comp_Id = 0;
                ViewBag.comp_Nombre = 0;
                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }

        [SessionManager("Editar Componentes")]

        public IActionResult GetComponente(int id)
        {
            var componente = _componenteRepository.Find(id);
            if (componente == null)
            {
                return View(nameof(Index));
            }

            var model = new ComponenteViewModel();
            ViewBag.comp_Id = id;
            model.comp_Id = id;
            model.comp_Nombre = componente.comp_Nombre;
            return AjaxResult(componente, true);
        }
    }
}
