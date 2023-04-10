using Appets.DataAccess.Entities;
using Appets.DataAccess.Repositories;
using Appets.DataAccess.Services;
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
    public class MascotasController : BaseController
    {

        private readonly MascotasRepository _mascotasRepository;
        private readonly EspeciesRepository _especiesRepository;
        private readonly RazasRepository _razasRepository;
        private readonly ProcedenciaRepository _procedenciaRepository;
        private readonly PersonaRepository _personaRepository;
        private readonly HelpersServicie _helpersServicie;


        public MascotasController(
            MascotasRepository mascotasRepository,
            EspeciesRepository especiesRepository, 
            RazasRepository razasRepository,
            ProcedenciaRepository procedenciaRepository,
            PersonaRepository personaRepository,
            HelpersServicie helpersServicie)
        {
            _mascotasRepository = mascotasRepository;
            _especiesRepository = especiesRepository;
            _razasRepository = razasRepository;
            _procedenciaRepository = procedenciaRepository;
            _personaRepository = personaRepository;
            _helpersServicie = helpersServicie;
        }

        //[SessionManager("Listado Mascotas")]
        public IActionResult Index()
        {
            var model = new MascotasViewModel();
            model.AccionMascota.LlenarListas(_especiesRepository.EspeciesList());
            model.AccionMascota.LlenarListas(_razasRepository.RazasList());
            model.AccionMascota.LlenarListas(_personaRepository.PersonaListado5());
            model.AccionMascota.LlenarListas(_procedenciaRepository.ProcedenciaList());

            return View(model);

        }


      
        public IActionResult Eliminar(int id)
        {
            var result = _mascotasRepository.Delete(id);
            return Json(result);
        }


        public IActionResult RazasList(int id) 
        {
            var resultado = _razasRepository.RazasByEspecie(id);
            return Json(resultado);
        }


        public IActionResult CambiarImagen(CambiarImagen model)
        {
            if (ModelState.IsValid)
            {
                var result = _helpersServicie.updateMascotasImagen(model.ImageFile, model.masc_Id);
                ShowAlert("Imagen Guardada correctamente",AlertMessageType.Success );
               
            }
            else
            {
                ShowAlert("Ha ocurrido un error", AlertMessageType.Error);
            }


            return RedirectToAction("Index");
        }

        //[SessionManager("Listado Mascotas")]
        public IActionResult AgregarMascotas()
        {
           
           
            var model = new MascotasViewModel();
            model.AccionMascota.LlenarListas(_especiesRepository.EspeciesList());
            model.AccionMascota.LlenarListas(_razasRepository.RazasList());
            model.AccionMascota.LlenarListas(_personaRepository.PersonaListado5());
            model.AccionMascota.LlenarListas(_procedenciaRepository.ProcedenciaList());
            ViewBag.masc_Id = 0;
            return View(nameof(MascotaAccion), model);
            
        }

        public ActionResult ListarMascotas()
        {
           
            IEnumerable<UDP_tbMascotas_SelectResult> lista = _mascotasRepository.MascotasList();

            return Json(new
            {

                data = lista.Select(x => new
                {
                    masc_Id = x.masc_Id,
                    //masc_Imagen = x.masc_Imagen,
                    masc_Nombre = x.masc_Nombre,
                    espc_Id = x.espc_Descripcion,
                    raza_Id = x.raza_Descripcion,
                    //masc_Edad = x.masc_Edad,
                    masc_Sexo = x.masc_Sexo,
                    masc_Peso = x.masc_Peso,
                    //masc_Talla = x.masc_Talla,
                    masc_Color = x.masc_Color,
                    //emp_Id = x.emp_Id,
                    masc_EsAdoptado = x.masc_EsAdoptado,
                    masc_EsReservado =  x.masc_EsReservado,
                    //masc_HistorialDescripcion = x.masc_HistorialDescripcion,
                    proc_Id = x.proc_Descripcion

                })
                .OrderBy(x => x.masc_Id)
            });
        }


        public IActionResult updateMascotasImagen(AccionMascota model)
        {

            if (ModelState.IsValid)
            {
                var result = new tbMascotas();

                ViewBag.masc_Id = model.masc_Id;
                ViewBag.masc_Imagen = model.masc_Imagen;
                //result = _mascotasRepository.updateMascotasImagen1(model.masc_Nombre, ViewBag.masc_Id);
                if (result == null)
                {
                    ShowAlert("Imagen Actualizada Correctamente", AlertMessageType.Success);
                    return RedirectToAction("Index");

                }

                else
                {

                    goto error;
                }

            error:
                model.masc_Id = 0;
                ViewBag.masc_Id = 0;

                ShowAlert("Error.", AlertMessageType.Error);
                return View(model);

            }

            return RedirectToAction("Index");
        }


        //[SessionManager("Crear Mascotas")]
      
        //[HttpPost("mascotas/listado-mascotas", Name = "SaveMascota")]

        public IActionResult MascotaAccion(AccionMascota model)
        {


            //if (ModelState.IsValid)
            //{
                int result = 0;
                ViewBag.masc_Id = model.masc_Id;
                if (model.masc_Id == 0)
                {

                    result = _mascotasRepository.Insert(
                        model.masc_Imagen,
                        model.masc_Nombre,
                        model.espc_Id,
                        model.raza_Id,
                        model.masc_Edad, 
                        model.masc_Sexo, 
                        model.masc_Peso,
                        model.masc_Talla,
                        model.masc_Color,
                        model.emp_Id,
                        //model.masc_EsAdoptado,
                        //model.masc_EsReservado,
                        model.masc_HistorialDescripcion,
                        model.proc_Id);
                    if (result != -1)
                    {
                        ShowAlert("Registro Ingresado Correctamente", AlertMessageType.Success);
                        return RedirectToAction(nameof(MascotaFind),new {id = model.masc_Id});
                        
                    }

                    else
                    {

                        goto error;
                    }

                }
                else
                {
                    result = _mascotasRepository.update(
                        ViewBag.masc_Id,
                        model.masc_Nombre,
                        model.espc_Id,
                        model.raza_Id,
                        model.masc_Edad,
                        model.masc_Sexo,
                        model.masc_Peso,
                        model.masc_Talla,
                        model.masc_Color,
                        model.emp_Id,
                        //model.masc_EsAdoptado,
                        //model.masc_EsReservado,
                        model.masc_HistorialDescripcion,
                        model.proc_Id);
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
                model.masc_Id = 0;
                ViewBag.masc_Id = 0;
               

                ShowAlert("Ingrese un Registro", AlertMessageType.Info);
                return View(model);

            //}
            //return RedirectToAction("Index");


        }

        //public string SplitVirgulilla(string param)
        //{
        //     param.Replace("~", string.Empty);

        //}
        //[SessionManager("Editar Mascotas")]
        public IActionResult MascotaFind(int id)
        {
            var mascotas = _mascotasRepository.Find(id);
            if (mascotas == null)
            {
                return View(nameof(Index));
            }

            var model = new MascotasViewModel();
            ViewBag.masc_Id = id;
            model.CambiarImagen.masc_Id = id;
            model.CambiarImagen.MascotaPriview = mascotas.masc_Imagen;
            model.AccionMascota.masc_Imagen = mascotas.masc_Imagen;
            model.AccionMascota.masc_Nombre = mascotas.masc_Nombre;
            model.AccionMascota.espc_Id = mascotas.espc_Id;
            model.AccionMascota.raza_Id = mascotas.raza_Id;
            model.AccionMascota.masc_Edad = mascotas.masc_Edad;
            model.AccionMascota.masc_Sexo = mascotas.masc_Sexo;
            model.AccionMascota.masc_Talla = mascotas.masc_Talla;
            model.AccionMascota.masc_Color = mascotas.masc_Color;
            model.AccionMascota.emp_Id = mascotas.emp_Id;
            model.AccionMascota.masc_Peso = mascotas.masc_Peso;
            model.CambiarImagen.nombreMascota = mascotas.masc_Nombre;
            //model.AccionMascota.masc_EsAdoptado = mascotas.masc_EsAdoptado;
            //model.AccionMascota.masc_EsReservado = mascotas.masc_EsReservado;
            model.AccionMascota.masc_HistorialDescripcion = mascotas.masc_HistorialDescripcion;
            model.AccionMascota.proc_Id = mascotas.proc_Id;
           
            model.AccionMascota.LlenarListas(_especiesRepository.EspeciesList());
            model.AccionMascota.LlenarListas(_razasRepository.RazasList());
            model.AccionMascota.LlenarListas(_personaRepository.PersonaListado5());
            model.AccionMascota.LlenarListas(_procedenciaRepository.ProcedenciaList());

            return View(nameof(MascotaAccion), model);
        }

    }
}

