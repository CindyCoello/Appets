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
    public class VoluntarioController : BaseController
    {
        private readonly VoluntarioRepository _voluntarioRepository;
        private readonly PersonaRepository _personaRepository;


        public VoluntarioController(VoluntarioRepository voluntarioRepository, 
            PersonaRepository personaRepository)
        {
            _voluntarioRepository = voluntarioRepository;
            _personaRepository = personaRepository;
        }

        //[SessionManager("Listado Voluntarios")]
        public IActionResult Index()
        {
            var model = new VoluntarioViewModel();
            model.LlenarListas(_personaRepository.PersonaListado3());
            return View(model);
        }
        [SessionManager("Listado Voluntarios")]
        public IActionResult Eliminar(int id)
        {
            var result = _voluntarioRepository.Delete(id);
            return Json(result);
        }

        //[SessionManager("Listado Voluntarios")]
        public IActionResult VoluntList()
        {
            IEnumerable<UDP_tbVoluntarios_SelectResult> lista = _voluntarioRepository.VoluntarioList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    volun_Id = x.volun_Id,
                    per_Id = x.per_Nombres + ' ' + x.per_Apellidos,
                    volun_FechaIngreso = x.volun_FechaIngreso.ToString("yyyy-MM-dd")


                })
                .OrderBy(x => x.volun_Id)
            });
        }

        //[SessionManager("Crear Voluntarios")]
        [HttpPost("voluntario/listado-voluntarios", Name = "SaveVoluntario")]

        public IActionResult EditarVoluntario(VoluntarioViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.volun_Id == 0)
                {

                    result = _voluntarioRepository.Insert(model.per_Id, model.volun_FechaIngreso);
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
                    result = _voluntarioRepository.Update(model.volun_Id, model.per_Id, model.volun_FechaIngreso);
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
                model.volun_Id = 0;
                ViewBag.per_Id = 0;
                ViewBag.volun_FechaIngreso = 0;
                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }

        //[SessionManager("Editar Voluntarios")]
        public IActionResult GetVoluntarios(int id)
        {
            var voluntario = _voluntarioRepository.Find(id);
            if (voluntario == null)
            {
                return View(nameof(Index));
            }

            var model = new VoluntarioViewModel();
            ViewBag.volun_Id = id;
            model.volun_Id = id;
            model.per_Id = voluntario.per_Id;
            model.volun_FechaIngreso = voluntario.volun_FechaIngreso;
            return AjaxResult(voluntario, true);
        }

    }
}

