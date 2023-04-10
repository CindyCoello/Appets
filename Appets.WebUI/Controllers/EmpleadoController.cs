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
    public class EmpleadoController : BaseController
    {
        private readonly EmpleadoRepository _empleadoRepository;
        private readonly OcupacionRepository _ocupacionRepository;
        private readonly PersonaRepository _personaRepository;

        public EmpleadoController(EmpleadoRepository empleadoRepository,
            OcupacionRepository ocupacionRepository,
             PersonaRepository personaRepository)
        {
            _empleadoRepository = empleadoRepository;
            _ocupacionRepository = ocupacionRepository;
            _personaRepository = personaRepository;
        }

        [SessionManager("Listado Empleado")]
        public IActionResult Index()
        {
            var model = new EmpleadoViewModel();
            model.LlenarListas(_personaRepository.PersonaListado());
            model.LlenarLista(_ocupacionRepository.OcupacionList());
            return View(model);
        }

        [SessionManager("Listado Empleado")]
        public IActionResult Eliminar(int id)
        {
            var result = _empleadoRepository.Delete(id);
            return Json(result);
        }


        public IActionResult EmpleadoList()
        {
        
            IEnumerable<UDP_tbEmpleados_SelectResult> lista = _empleadoRepository.EmpleadoList();
            return Json(new
            {

                data = lista.Select(x => new
                {
                    emp_Id = x.emp_Id,
                    per_Id = x.per_Nombres + ' '+ x.per_Apellidos,
                    ocup_Id = x.ocup_Descripcion

                })
                .OrderBy(x => x.emp_Id)
            });
        }

        [SessionManager("Crear Empleados")]

        [HttpPost("empleado/listado-empleados", Name = "SaveEmpleado")]

        public IActionResult EditarEspecie(EmpleadoViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.emp_Id == 0)
                {

                    result = _empleadoRepository.Insert(model.per_Id, model.ocup_Id);
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
                    result = _empleadoRepository.Update(model.emp_Id, model.per_Id, model.ocup_Id);
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
                model.emp_Id = 0;
                ViewBag.per_Id = 0;
                ViewBag.ocup_Id = 0;
                ShowAlert("Por favor ingrese lo solicitado", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }


        [SessionManager("Editar de Empleado")]
        public IActionResult GetEmpleado(int id)
        {
            var empleado = _empleadoRepository.Find(id);
            if (empleado == null)
            {
                return View(nameof(Index));
            }

            var model = new EmpleadoViewModel();
            ViewBag.emp_Id = id;
            model.emp_Id = id;
            model.per_Id = empleado.per_Id;
            model.ocup_Id = empleado.ocup_Id;
            return AjaxResult(empleado, true);
        }


    }
}
