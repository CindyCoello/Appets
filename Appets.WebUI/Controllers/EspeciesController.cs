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
    public class EspeciesController : BaseController
    {

        private readonly EspeciesRepository _especiesRepository;

        public EspeciesController(EspeciesRepository especiesRepository)
        {
            _especiesRepository = especiesRepository;
        }

        //[SessionManager("Listado Especies")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListEspecies()
        {
            IEnumerable<tbEspecies> lista = _especiesRepository.EspeciesList();

            return Json(new
            {
                data = lista.Select(x => new {

                    espc_Id = x.espc_Id,
                    espc_Descripcion = x.espc_Descripcion

                })
                .OrderBy(x => x.espc_Id)
            });
        }

        //[SessionManager("Crear Especies")]
        [HttpPost("especies/listado-especies",Name = "SaveEspecie")]

        public IActionResult EditarEspecie(EspeciesViewModel model)
        {
            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.espc_Id == 0)
                {

                    result = _especiesRepository.Insert(model.espc_Descripcion);
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
                    result = _especiesRepository.Update(model.espc_Id, model.espc_Descripcion);
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
                    model.espc_Id = 0;
                    ViewBag.espc_Descripcion = 0;
                    ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                    return View(model);

            }
            return RedirectToAction("Index");
        }

        //[SessionManager("Editar Especies")]
        public IActionResult GetEspecie(int id)
        {
            var especie = _especiesRepository.Find(id);
            if(especie== null)
            {
                return View(nameof(Index));
            }

            var model = new EspeciesViewModel();
            ViewBag.espc_Id = id;
            model.espc_Id = id;
            model.espc_Descripcion = especie.espc_Descripcion;
            return AjaxResult(especie, true);
        }

        public IActionResult Eliminar(int id)
        {
            var result = _especiesRepository.Delete(id);
            return Json(result);
        }
    }
}

