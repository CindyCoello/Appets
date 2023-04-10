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
    public class ProcedenciaController : BaseController
    {

        private readonly ProcedenciaRepository _procedenciaRepository;


        public ProcedenciaController(ProcedenciaRepository procedenciaRepository)
        {
            _procedenciaRepository = procedenciaRepository;
        }
        [SessionManager("Listado Procedencias")]
        public IActionResult Index()
        {
            return View();
        }

        [SessionManager("Listado Procedencias")]
        public IActionResult ProcedList()
        {   
            IEnumerable<tbProcedencia> lista = _procedenciaRepository.ProcedenciaList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    proc_Id = x.proc_Id,
                    proc_Descripcion = x.proc_Descripcion

                })
                .OrderBy(x => x.proc_Id)
            });
        }

        [SessionManager("Crear Procedencias")]
        [HttpPost("procedencia/listado-procedencias", Name = "SaveProcedencia")]

        public IActionResult EditarProcdencia(ProcedenciaViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.proc_Id == 0)
                {

                    result = _procedenciaRepository.Insert(model.proc_Descripcion);
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
                    result = _procedenciaRepository.Update(model.proc_Id, model.proc_Descripcion);
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
                    model.proc_Id = 0;
                    ViewBag.espc_Descripcion = 0;
                    ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                    return View(model);

            }
            return RedirectToAction("Index");


        }

        [SessionManager("Editar Procedencias")]
        public IActionResult Getprocedencia(int id)
        {
            var procedencia = _procedenciaRepository.Find(id);
            if (procedencia == null)
            {
                return View(nameof(Index));
            }

            var model = new ProcedenciaViewModel();
            ViewBag.proc_Id = id;
            model.proc_Id = id;
            model.proc_Descripcion = procedencia.proc_Descripcion;
            return AjaxResult(procedencia, true);
        }

    }
}
