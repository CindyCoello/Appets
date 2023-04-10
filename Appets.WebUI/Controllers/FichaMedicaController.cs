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
    public class FichaMedicaController : BaseController
    {

        private readonly FichaMedicaRepository _fichaMedicaRepository;
        private readonly MascotasRepository _mascotasRepository;


        public FichaMedicaController(FichaMedicaRepository fichaMedicaRepository,
            MascotasRepository mascotasRepository)
        {
            _fichaMedicaRepository = fichaMedicaRepository;
            _mascotasRepository = mascotasRepository;
        }

        //[SessionManager("Listado Ficha Medica")]
        public IActionResult ListFicha()
        {
           
            IEnumerable<UDP_tbFichaMedica_SelectResult> lista = _fichaMedicaRepository.FichaList();
            return Json(new
            {


                data = lista.Select(x => new {

                    medic_Id = x.medic_Id,
                    masc_Id = x.masc_Nombre,
                    medic_Esterilizacion = x.medic_Esterilizacion,
                    medic_Personalidad = x.medic_Personalidad,
                    medic_SaludCuidado = x.medic_SaludCuidado,
                    medic_InformacionAdicional = x.medic_InformacionAdicional

                })
                .OrderBy(x => x.medic_Id)
            });
        }

        //[SessionManager("Listado Ficha Medica")]
        public IActionResult Index()
        {
           
            var model = new FichaMedicaViewModel();
            model.LlenarListas(_mascotasRepository.MascotasList());

            return View(model);
        }

        public IActionResult Eliminar(int id)
        {
            var result = _fichaMedicaRepository.Delete(id);
            return Json(result);
        }
        public IActionResult AgregarFichaMedica()
        {
            var model = new FichaMedicaViewModel();
            model.LlenarListas(_mascotasRepository.MascotasList());
            ViewBag.medic_Id = 0;
            return View(nameof(EditarFichaMedica), model);
        }


        //[SessionManager("Crear Ficha Medica")]
        [HttpPost("fichamedica/listado-ficha-medica", Name = "SaveFichaM")]

        public IActionResult EditarFichaMedica(FichaMedicaViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.medic_Id == 0)
                {

                    result = _fichaMedicaRepository.Insert(model.masc_Id, model.medic_Esterilizacion, model.medic_Personalidad, model.medic_SaludCuidado, model.medic_InformacionAdicional);
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
                    result = _fichaMedicaRepository.Update(model.medic_Id, model.masc_Id, model.medic_Esterilizacion, model.medic_Personalidad, model.medic_SaludCuidado, model.medic_InformacionAdicional);
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
                model.medic_Id = 0;
                ViewBag.masc_Id = 0;
                ViewBag.medic_Esterilizacion = 0;
                ViewBag.medic_Personalidad = 0;
                ViewBag.medic_SaludCuidado = 0;
                ViewBag.medic_InformacionAdicional = 0;


                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }

        //[SessionManager("Editar Ficha Medica")]
        public IActionResult EditarFichaMedica(int id)
        {
            var FichaMedica = _fichaMedicaRepository.Find(id);
            if (FichaMedica == null)
            {
                return View(nameof(Index));
            }

            var model = new FichaMedicaViewModel();
            ViewBag.medic_Id = id;
            model.medic_Id = id;
            model.masc_Id = FichaMedica.masc_Id;
            model.medic_Esterilizacion = FichaMedica.medic_Esterilizacion;
            model.medic_Personalidad = FichaMedica.medic_Personalidad;
            model.medic_SaludCuidado = FichaMedica.medic_SaludCuidado;
            model.medic_InformacionAdicional = FichaMedica.medic_InformacionAdicional;
            model.LlenarListas(_mascotasRepository.MascotasList());
            return View(model);
        }
    }
}
