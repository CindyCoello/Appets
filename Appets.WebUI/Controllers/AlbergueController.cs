using Appets.DataAccess.Entities;
using Appets.DataAccess.Repositories;
using Appets.WebUI.Extensions;
using Appets.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Controllers
{
    public class AlbergueController : BaseController
    {

        private readonly AlbergueRepository _albergueRepository;


        public AlbergueController(AlbergueRepository albergueRepository)
        {
            _albergueRepository = albergueRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AgregarAlbergue()
        {
            var model = new AlbergueViewModel();
            ViewBag.alberg_Id = 0;
            return View(nameof(EditarAlbergue), model);
        }

        public IActionResult AlbergList()
        {
            IEnumerable<tbAlbergue> lista = _albergueRepository.AlbergueList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    alberg_Id = x.alberg_Id,
                    alberg_RTN = x.alberg_RTN,
                    alberg_Nombre = x.alberg_Nombre,
                    alberg_Ubicacion = x.alberg_Ubicacion,
                    alberg_Telefono = x.alberg_Telefono,
                    alberg_Correo = x.alberg_Correo,
                    alberg_Mision = x.alberg_Mision,
                    alberg_InformacionAdicion = x.alberg_InformacionAdicion

                })
                .OrderBy(x => x.alberg_Id)
            });
        }


        [HttpPost("albergue/listado-albergues", Name = "SaveAlbergue")]

        public IActionResult EditarAlbergue(AlbergueViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.alberg_Id == 0)
                {

                    result = _albergueRepository.Insert(model.alberg_RTN,model.alberg_Nombre,model.alberg_Ubicacion,model.alberg_Telefono,model.alberg_Correo,model.alberg_Mision,model.alberg_InformacionAdicion);
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
                    result = _albergueRepository.Update(model.alberg_Id,model.alberg_RTN, model.alberg_Nombre, model.alberg_Ubicacion, model.alberg_Telefono, model.alberg_Correo, model.alberg_Mision, model.alberg_InformacionAdicion);
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
                model.alberg_Id = 0;
                ViewBag.alberg_RTN = 0;
                ViewBag.alberg_Nombre = 0;
                ViewBag.alberg_Ubicacion = 0;
                ViewBag.alberg_Telefono = 0;
                ViewBag.alberg_Correo = 0;
                ViewBag.alberg_Mision = 0;
                ViewBag.alberg_InformacionAdicion = 0;
                
                ShowAlert("Por favor ingrese lo solicitado", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }


        public IActionResult EditarAlbergue(int id)
        {
            var albergue = _albergueRepository.Find(id);
            if (albergue == null)
            {
                return View(nameof(Index));
            }

            var model = new AlbergueViewModel();
            ViewBag.alberg_Id = id;
            model.alberg_Id = id;
            model.alberg_RTN = albergue.alberg_RTN;
            model.alberg_Nombre = albergue.alberg_Nombre;
            model.alberg_Ubicacion = albergue.alberg_Ubicacion;
            model.alberg_Telefono = albergue.alberg_Telefono;
            model.alberg_Correo = albergue.alberg_Correo;
            model.alberg_Mision = albergue.alberg_Mision;
            model.alberg_InformacionAdicion = albergue.alberg_InformacionAdicion;
            return View(model);
        }

    }
}
