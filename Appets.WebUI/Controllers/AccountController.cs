using Appets.DataAccess.Repositories;
using Appets.DataAccess.Services;
using Appets.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;



namespace Appets.WebUI.Controllers
{
    public class AccountController : Controller
    {
        //inyeccion de dependencias
        private readonly UsuariosRepository _usuariosRepository;
        private readonly HelpersServicie _helpersServicie;
        private readonly IConfiguration _configuration;

        public AccountController(UsuariosRepository usuariosRepository,
            HelpersServicie helpersServicie,
            IConfiguration  configuration)
        {
            _usuariosRepository = usuariosRepository;
            _helpersServicie = helpersServicie;
            _configuration = configuration;
           
        }

        public IActionResult Login()
        {
            HttpContext.Session.Remove("pantallas");
            return View();
        }

       
        public IActionResult SinAcceso()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
                if (model.usu_Identidad.Length != 13 || model.usu_Identidad.Contains("-")){
                    ModelState.AddModelError("usu_Identidad", "La longitud de la identidad debe ser 13 sin guiones");
                    return View(model);
                }
                 

                var usuario = _usuariosRepository.Login(model.usu_Contraseña , model.usu_Identidad);
                if (usuario != null)
                {
                    string pantallas = string.Join(",", _helpersServicie.ListModuloPantallasByRol(usuario.rol_Id));
                    HttpContext.Session.SetString("usu_Identidad", usuario.usu_Identidad);
                    HttpContext.Session.SetString("pantallas", pantallas);
                  
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("", "El usuario o contraseña ingresados son incorrectos");
                   
                }
               return View(model);

        }
    }
}

