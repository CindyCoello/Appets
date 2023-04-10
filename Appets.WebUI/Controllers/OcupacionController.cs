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
    public class OcupacionController : BaseController
    {

        private readonly OcupacionRepository _ocupacionRepository;


        public OcupacionController(OcupacionRepository ocupacionRepository)
        {
            _ocupacionRepository = ocupacionRepository;
        }

        //[SessionManager("Listado Cargos")]
        public IActionResult Index()
        {
            return View();
        }



        //[SessionManager("Listado Cargos")]
        public ActionResult OcupacionList()
             {
                  
               IEnumerable<tbOcupaciones> lista = _ocupacionRepository.OcupacionList();
                return Json(new
                {

                    data = lista.Select(x => new
                    {
                        ocup_Id = x.ocup_Id,
                        ocup_Descripcion = x.ocup_Descripcion,
                   

                    })
                    .OrderBy(x => x.ocup_Id)
                });
             }



        [HttpPost("ocupacion/listado-ocupacion", Name = "SaveOcupacion")]

        public IActionResult EditarOcupacion(OcupacionViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.ocup_Id == 0)
                {

                    result = _ocupacionRepository.Insert(model.ocup_Descripcion);
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
                    result = _ocupacionRepository.Update(model.ocup_Id, model.ocup_Descripcion);
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
                model.ocup_Id = 0;
                ViewBag.ocup_Descripcion = 0;
                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }



        //[SessionManager("Editar Cargos")]
        public IActionResult GetOcupacion(int id)
        {
            var ocupacion = _ocupacionRepository.Find(id);
            if (ocupacion == null)
            {
                return View(nameof(Index));
            }

            var model = new OcupacionViewModel();
            ViewBag.ocup_Id = id;
            model.ocup_Id = id;
            model.ocup_Descripcion = ocupacion.ocup_Descripcion;
            return AjaxResult(ocupacion, true);
        }


    }
}
