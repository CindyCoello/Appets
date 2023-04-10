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
    public class SolicitudController : BaseController
    {
        private readonly SolicitudRepository _solicitudRepository;
        private readonly MascotasRepository _mascotasRepository;


        public SolicitudController(SolicitudRepository solicitudRepository,
             MascotasRepository mascotasRepository)
        {
            _solicitudRepository = solicitudRepository;
            _mascotasRepository = mascotasRepository;
        }

        //[SessionManager("Listado Solicitud Adopcion")]
        public IActionResult Index()
        {
            var model = new SolicitudViewModel();
            model.LlenarListas(_mascotasRepository.MascotasList());
            return View(model);
        }

       
        public IActionResult AgregarSolicitud()
        {
            var model = new SolicitudViewModel();
            model.LlenarListas(_mascotasRepository.MascotasList());
            ViewBag.solic_Id = 0;
            return View(nameof(EditarSolicitud), model);
        }

        public IActionResult SolicitudList()
        {
            
            IEnumerable<UDP_tbSolicitud_SelectResult> lista = _solicitudRepository.SolicitudList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    solic_Id = x.solic_Id,
                    solic_Correo = x.solic_Correo,
                    solic_NombreCompleto = x.solic_NombreCompleto,
                    solic_Fecha = x.solic_Fecha.ToString("yyyy-MM-dd"),
                    masc_Id = x.masc_Nombre

                })
                .OrderBy(x => x.solic_Id)
            });
        }

        //[SessionManager("Crear Solicitud Adopcion")]
        [HttpPost("solicitud/listado-solicitud", Name = "SaveSolicitud")]

        public IActionResult EditarSolicitud(SolicitudViewModel model)
        {
            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.solic_Id == 0)
                {

                    result = _solicitudRepository.Insert(model.solic_Correo,model.solic_NombreCompleto, model.solic_Fecha,model.masc_Id);
                    _mascotasRepository.Reservado(model.masc_Id, true);
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
                    result = _solicitudRepository.Update(model.solic_Id, model.solic_Correo, model.solic_NombreCompleto, model.solic_Fecha, model.masc_Id);
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
                model.solic_Id = 0;
                ViewBag.solic_Correo = 0;
                ViewBag.solic_NombreCompleto = 0;
                ViewBag.solic_Fecha = 0;
                ViewBag.masc_Id = 0;
                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");
        }

        //[SessionManager("Editar Solicitud Adopcion")]
        public IActionResult EditarSolicitud(int id)
        {
            var solicitud = _solicitudRepository.Find(id);
            if (solicitud == null)
            {
                return View(nameof(Index));
            }
            var model = new SolicitudViewModel();
            ViewBag.solic_Id = id;
            model.solic_Id = id;
            model.solic_Correo = solicitud.solic_Correo;
            model.solic_NombreCompleto = solicitud.solic_NombreCompleto;
            model.solic_Fecha = solicitud.solic_Fecha;
            model.masc_Id = solicitud.masc_Id;
            model.LlenarListas(_mascotasRepository.MascotasList());
            return View(model);
        }

    }
}
