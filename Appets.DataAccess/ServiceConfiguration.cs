using Appets.DataAccess.Repositories;
using Appets.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appets.DataAccess
{
    public static class ServiceConfiguration
    {

        public static void AddDataAcces(this IServiceCollection services, string connectionString)
        {
                
                services.AddScoped<UsuariosRepository>();
                services.AddScoped<ModuloRepository>();
                services.AddScoped<ModuloPantallaRepository>();
                services.AddScoped<ComponenteRepository>();
                services.AddScoped<RolesRepository>();
                services.AddScoped<EspeciesRepository>();
                services.AddScoped<RazasRepository>();
                services.AddScoped<ProcedenciaRepository>();
                services.AddScoped<AlbergueRepository>();
                services.AddScoped<PersonaRepository>();
                services.AddScoped<DonanteRepository>();
                services.AddScoped<VoluntarioRepository>();
                services.AddScoped<FichaMedicaRepository>();
                services.AddScoped<MascotasRepository>();
                services.AddScoped<FichaAdopcionRepository>();
                services.AddScoped<SolicitudRepository>();
                services.AddScoped<EmpleadoRepository>();
                services.AddScoped<OcupacionRepository>();

            

               services.AddScoped<HelpersServicie>();
              


            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddScoped<IUrlHelper>(x => x
                .GetRequiredService<IUrlHelperFactory>()
                .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));

            AppetsDbContext.BuildConnectionString(connectionString);
        }

    }
}



