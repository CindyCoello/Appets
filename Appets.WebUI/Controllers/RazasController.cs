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
    public class RazasController : BaseController
    {
        private readonly RazasRepository _razasRepository;
        private readonly EspeciesRepository _especiesRepository;


        public RazasController(RazasRepository razasRepository,
            EspeciesRepository especiesRepository)
        {
            _razasRepository = razasRepository;
            _especiesRepository = especiesRepository;
        }

        //[SessionManager("Listado Razas")]
        public IActionResult Index()
        {
            var model = new RazaViewModel();
            model.LlenarLista(_especiesRepository.EspeciesList());
            return View(model);
        }


        //[SessionManager("Listado Razas")]
        public IActionResult RazacList()
        {
           
            IEnumerable<UDP_tbRazas_SelectResult> lista = _razasRepository.RazasList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    raza_Id = x.raza_Id,
                    raza_Descripcion = x.raza_Descripcion,
                    espc_Id = x.espc_Descripcion


        })
                .OrderBy(x => x.raza_Id)
            }); 
        }

        //[SessionManager("Crear Razas")]
        [HttpPost("raza/listado-razas", Name = "SaveRaza")]

        public IActionResult EditarProcdencia(RazaViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.raza_Id == 0)
                {

                    result = _razasRepository.Insert(model.raza_Descripcion,model.espc_Id);
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
                    result = _razasRepository.Update(model.raza_Id, model.raza_Descripcion,model.espc_Id);
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
                model.raza_Id = 0;
                ViewBag.raza_Descripcion = 0;
                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");

        }

        //[SessionManager("Editar Razas")]
        public IActionResult GetRaza(int id)
        {
            var razas = _razasRepository.Find(id);
            if (razas == null)
            {
                return View(nameof(Index));
            }

            var model = new RazaViewModel();
            ViewBag.raza_Id = id;
            model.raza_Id = id;
            model.raza_Descripcion = razas.raza_Descripcion;
            return AjaxResult(razas, true);
        }
    }
}
