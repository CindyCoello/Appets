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
    public class DonanteController : BaseController
    {

        private readonly DonanteRepository _donanteRepository;
        private readonly PersonaRepository _personaRepository;


        public DonanteController(DonanteRepository donanteRepository, PersonaRepository personaRepository)
        {
            _donanteRepository = donanteRepository;
            _personaRepository = personaRepository;
        }

        [SessionManager("Listado Donantes")]
        public IActionResult Index()
        {
            var model = new DonanteViewModel();
            model.LlenarListas(_personaRepository.PersonaListado2());
            return View(model);
          
        }

        [SessionManager("Listado Donantes")]
        public IActionResult DonanteList()
        {
            IEnumerable<UDP_tbDonantes_SelectResult> lista = _donanteRepository.DonanteList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    don_Id = x.don_Id,
                    per_Id = x.per_Nombres + ' '+ x.per_Apellidos,
                    don_Fecha = x.don_Fecha.ToString("yyyy-MM-dd"),
                    don_Descripcion = x.don_Descripcion

                })
                .OrderBy(x => x.don_Id)
            });
        }


        [HttpPost("donante/listado-donantes", Name = "SaveDonante")]

        public IActionResult EditarDonante(DonanteViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.don_Id == 0)
                {

                    result = _donanteRepository.Insert(model.per_Id,model.don_Fecha,model.don_Descripcion);
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
                    result = _donanteRepository.Update(model.don_Id, model.per_Id, model.don_Fecha, model.don_Descripcion);
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
                model.don_Id = 0;
                ViewBag.per_Id = 0;
                ViewBag.don_Fecha = 0;
                ViewBag.don_Descripcion = 0;
                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }

        [SessionManager("Editar Donantes")]
        public IActionResult GetDonantes(int id)
        {
            var donante = _donanteRepository.Find(id);
            if (donante == null)
            {
                return View(nameof(Index));
            }

            var model = new DonanteViewModel();
            ViewBag.don_Id = id;
            model.don_Id = id;
            model.per_Id = donante.per_Id;
            model.don_Fecha = donante.don_Fecha;
            model.don_Descripcion = donante.don_Descripcion;
            return AjaxResult(donante, true);
        }

    }


}

