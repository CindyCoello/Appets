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
    public class PersonaController : BaseController
    {

        private readonly PersonaRepository _personaRepository;


        public PersonaController(PersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }
        [SessionManager("Listado Personas")]
        public IActionResult Index()
        {
            return View();
        }


        [SessionManager("Listado Personas")]
        public IActionResult Eliminar(int id)
        {
            var result = _personaRepository.Delete(id);
            return Json(result);
        }

        [SessionManager("Listado Personas")]
        public IActionResult AgregarPersona()
        {
            var model = new PersonaViewModel();
            //model.LlenarLista(_rolesRepository.RolesList());
            ViewBag.per_Id = 0;
            return View(nameof(EditarPersona), model);
        }

        public IActionResult PerList()
        {
            IEnumerable<tbPersonas> lista = _personaRepository.PersonaList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    per_Id = x.per_Id,
                    per_Identidad = x.per_Identidad,
                    per_Nombres = x.per_Nombres + " "+ x.per_Apellidos,
                    //per_Edad = x.per_Edad,
                    //per_FechaNacimiento = x.per_FechaNacimiento,
                    //per_Domicilio = x.per_Domicilio,
                    per_Telefono = x.per_Telefono,
                    per_Correo = x.per_Correo,
                    per_EsAdoptante = x.per_EsAdoptante

                })
                .OrderBy(x => x.per_Id)
            });
        }


        public IActionResult PerList1()
        {
            IEnumerable<UDP_tbPersonas_EsEmpleadoResult> lista = _personaRepository.PersonaListado();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    per_Id = x.per_Id,
                    per_Identidad = x.per_Identidad,
                    per_Nombres = x.per_Nombres + " " + x.per_Apellidos,
                    //per_Edad = x.per_Edad,
                    //per_FechaNacimiento = x.per_FechaNacimiento,
                    //per_Domicilio = x.per_Domicilio,
                    per_Telefono = x.per_Telefono,
                    per_Correo = x.per_Correo,
                    per_EsAdoptante = x.per_EsAdoptante

                })
                .OrderBy(x => x.per_Id)
            });
        }

        public IActionResult PerList2()
        {
            IEnumerable<UDP_tbPersonas_EsDonanteResult> lista = _personaRepository.PersonaListado2();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    per_Id = x.per_Id,
                    per_Identidad = x.per_Identidad,
                    per_Nombres = x.per_Nombres + " " + x.per_Apellidos,
                    //per_Edad = x.per_Edad,
                    //per_FechaNacimiento = x.per_FechaNacimiento,
                    //per_Domicilio = x.per_Domicilio,
                    per_Telefono = x.per_Telefono,
                    per_Correo = x.per_Correo,
                    per_EsAdoptante = x.per_EsAdoptante

                })
                .OrderBy(x => x.per_Id)
            });
        }

        public IActionResult PerList3()
        {
            IEnumerable<UDP_tbPersonas_EsVoluntarioResult> lista = _personaRepository.PersonaListado3();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    per_Id = x.per_Id,
                    per_Identidad = x.per_Identidad,
                    per_Nombres = x.per_Nombres + " " + x.per_Apellidos,
                    //per_Edad = x.per_Edad,
                    //per_FechaNacimiento = x.per_FechaNacimiento,
                    //per_Domicilio = x.per_Domicilio,
                    per_Telefono = x.per_Telefono,
                    per_Correo = x.per_Correo,
                    per_EsAdoptante = x.per_EsAdoptante

                })
                .OrderBy(x => x.per_Id)
            });
        }

        public IActionResult PerList4()
        {
            IEnumerable<UDP_tbPersonas_EsAdoptanteResult> lista = _personaRepository.PersonaListado4();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    per_Id = x.per_Id,
                    per_Identidad = x.per_Identidad,
                    per_Nombres = x.per_Nombres + " " + x.per_Apellidos,
                    //per_Edad = x.per_Edad,
                    //per_FechaNacimiento = x.per_FechaNacimiento,
                    //per_Domicilio = x.per_Domicilio,
                    per_Telefono = x.per_Telefono,
                    per_Correo = x.per_Correo,
                    per_EsAdoptante = x.per_EsAdoptante

                })
                .OrderBy(x => x.per_Id)
            });
        }


        public IActionResult PerList5()
        {
            IEnumerable<UDP_tbPersonas_EsEmpleadoResult> lista = _personaRepository.PersonaListado5();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    per_Id = x.per_Id,
                    per_Identidad = x.per_Identidad,
                    per_Nombres = x.per_Nombres + " " + x.per_Apellidos,
                    //per_Edad = x.per_Edad,
                    //per_FechaNacimiento = x.per_FechaNacimiento,
                    //per_Domicilio = x.per_Domicilio,
                    per_Telefono = x.per_Telefono,
                    per_Correo = x.per_Correo,
                    per_EsAdoptante = x.per_EsAdoptante

                })
                .OrderBy(x => x.per_Id)
            });
        }


        [SessionManager("Crear Personas")]

        [HttpPost("persona/listado-personas", Name = "SavePersona")]

        public IActionResult EditarPersona(PersonaViewModel model)
        {


            if (ModelState.IsValid)
            {
                int result = 0;

                if (model.per_Id == 0)
                {

                       result = _personaRepository.Insert(
                        model.per_Identidad,
                        model.per_Nombres,
                        model.per_Apellidos,
                        (int)model.per_Edad,
                        (DateTime)model.per_FechaNacimiento,
                        model.per_Domicilio, 
                        model.per_Telefono,
                        model.per_Correo,
                        model.per_EsAdoptante,
                        model.per_EsDonante,
                        model.per_EsEmpleado,
                        model.per_EsVoluntario);
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
                    result = _personaRepository.Update(
                        model.per_Id,
                        model.per_Identidad,
                        model.per_Nombres,
                        model.per_Apellidos,
                        (int)model.per_Edad,
                        (DateTime)model.per_FechaNacimiento,
                        model.per_Domicilio,
                        model.per_Telefono,
                        model.per_Correo,
                        model.per_EsAdoptante,
                        model.per_EsDonante,
                        model.per_EsEmpleado,
                        model.per_EsVoluntario);
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
                model.per_Id = 0;
                ViewBag.per_Identidad = 0;
                ViewBag.per_Nombres = 0;
                ViewBag.per_Apellidos = 0;
                ViewBag.per_Edad = 0;
                ViewBag.per_FechaNacimiento = 0;
                ViewBag.per_Domicilio = 0;
                ViewBag.per_Telefono = 0;
                ViewBag.per_Correo = 0;
                ViewBag.per_EsAdoptante = 0;
                ViewBag.per_EsDonante = 0;
                ViewBag.per_EsEmpleado = 0;
                ViewBag.per_EsVoluntario = 0;

                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            }
            return RedirectToAction("Index");


        }

        [SessionManager("Editar Personas")]
        public IActionResult EditarPersona(int id)
        {
            var persona = _personaRepository.Find(id);
            if (persona == null)
            {
                return View(nameof(Index));
            }

            var model = new PersonaViewModel();
            ViewBag.per_Id = id;
            model.per_Id = id;
            model.per_Identidad = persona.per_Identidad;
            model.per_Nombres = persona.per_Nombres;
            model.per_Apellidos = persona.per_Apellidos;
            model.per_Edad = persona.per_Edad;
            model.per_FechaNacimiento = persona.per_FechaNacimiento;
            model.per_Domicilio = persona.per_Domicilio;
            model.per_Telefono = persona.per_Telefono;
            model.per_Correo = persona.per_Correo;
            model.per_EsAdoptante = persona.per_EsAdoptante;

            model.per_EsDonante = persona.per_EsDonante;
            model.per_EsEmpleado = persona.per_EsEmpleado;
            model.per_EsVoluntario = persona.per_EsVoluntario;
            return View(model);
        }
    }
}
