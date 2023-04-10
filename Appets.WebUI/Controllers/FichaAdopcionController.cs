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
    public class FichaAdopcionController : BaseController
    {

        private readonly FichaAdopcionRepository _fichaAdopcionRepository;
        private readonly PersonaRepository _personaRepository;
        private readonly MascotasRepository _mascotasRepository;


        public FichaAdopcionController(FichaAdopcionRepository fichaAdopcionRepository,
            PersonaRepository personaRepository,
            MascotasRepository mascotasRepository)
        {
            _fichaAdopcionRepository = fichaAdopcionRepository;
            _personaRepository = personaRepository;
            _mascotasRepository = mascotasRepository;
        }
        //[SessionManager("Listado Ficha Adopcion")]
        public IActionResult Index()
        {
            var model = new FichaAdopcionViewModel();
            model.LlenarListas(_mascotasRepository.MascotasList());
            model.LlenarLista(_personaRepository.PersonaListado4());
            return View(model);
        }

        //[SessionManager("Listado Ficha Adopcion")]
        public IActionResult Eliminar(int id)
        {
            var result = _fichaAdopcionRepository.Delete(id);
            return Json(result);
        }

        public IActionResult FichaList()
        {
            IEnumerable<UDP_tbFichaAdopcion_SelectResult> lista = _fichaAdopcionRepository.FichaAdopcionList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    ficha_Id = x.ficha_Id,
                    masc_Id = x.masc_Nombre,
                    per_Id = x.per_Nombres + ' '+ x.per_Apellidos,
                    ficha_FechaRegistro = x.ficha_FechaRegistro.ToString("yyyy-MM-dd")
                    
                })
                .OrderBy(x => x.ficha_Id)
            });
        }

        //[SessionManager("Crear Ficha Adopcion")]

        [HttpPost("fichaAdopcion/listado-adopciones", Name = "SaveAdopcion")]

        public IActionResult EditarAdopcion(FichaAdopcionViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.ficha_Id == 0)
                {

                    result = _fichaAdopcionRepository.Insert(model.masc_Id, model.per_Id,model.ficha_FechaRegistro);
                    _mascotasRepository.Adoptado(model.masc_Id, true);
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
                    result = _fichaAdopcionRepository.Update(model.ficha_Id, model.masc_Id, model.per_Id, model.ficha_FechaRegistro);
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
                model.ficha_Id = 0;
                ViewBag.masc_Id = 0;
                ViewBag.per_Id = 0;
                ViewBag.ficha_FechaRegistro = 0;
                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }


        //[SessionManager("Editar Ficha Adopcion")]
        public IActionResult GetAdopcion(int id)
        {
            var adopcion = _fichaAdopcionRepository.Find(id);
            if (adopcion == null)
            {
                return View(nameof(Index));
            }

            var model = new FichaAdopcionViewModel();
            ViewBag.ficha_Id = id;
            model.ficha_Id = id;
            model.masc_Id = adopcion.masc_Id;
            model.per_Id = adopcion.per_Id;
            model.ficha_FechaRegistro = adopcion.ficha_FechaRegistro;
            return AjaxResult(adopcion, true);
        }
    }
}
