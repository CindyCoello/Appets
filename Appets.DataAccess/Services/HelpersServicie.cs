using Appets.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp.Formats.Gif;

namespace Appets.DataAccess.Services
{
    public class HelpersServicie
    {
        private readonly ModuloPantallaRepository _moduloPantallaRepository;
        private readonly MascotasRepository _mascotasRepository;
        private readonly IHostEnvironment _HostEnvironment;


        public HelpersServicie(ModuloPantallaRepository moduloPantallaRepository,
            MascotasRepository mascotasRepository,
            IHostEnvironment HostEnvironment)

        {
            _moduloPantallaRepository = moduloPantallaRepository;
            _mascotasRepository = mascotasRepository;
            _HostEnvironment = HostEnvironment;
        }

        public List<string> ListModuloPantallasByRol(int rol_Id)
        {

            var listado = _moduloPantallaRepository.ListModuloPantallasByRol(rol_Id);
            var listadoString = new List<string>();

            try
            {
                foreach (var item in listado)
                {
                    listadoString.Add(item.modpan_Nombre);
                }
                return listadoString;
            }
            catch (Exception)
            {

                return listadoString;
            }
        }




        public string updateMascotasImagen(IFormFile file, int id)
        {
            string oldFile = "";
            string newExtension = "";


            try
            {
                if (file.ContentType != "image/jpeg" &&
                    file.ContentType != "image/jpg" &&
                    file.ContentType != "image/png" &&
                    file.ContentType != "image/gif")

                    return "Seleccione un archivo valido (.jpg, .png, .gif)";

                var mascota = _mascotasRepository.Find(id);

                string root = _HostEnvironment.ContentRootPath;
                string masc_Imagen = $"{root}\\wwwroot\\images\\mascotas-images";
                string imageName = $"{mascota.masc_Nombre.ToLower().Trim().Replace(" ", "-")}";

                if (!string.IsNullOrEmpty(mascota.masc_Imagen)) 
                oldFile = $"{root}{mascota.masc_Imagen}";

                if (File.Exists(oldFile))
                {
                    File.Delete(oldFile);
                }


                using (var imagenScreen = file.OpenReadStream())
                {
                    using (var img = Image.Load(imagenScreen))
                    {
                        img.Mutate(x => x.Resize(new ResizeOptions
                        {

                            Mode = ResizeMode.Min,
                            Size = new SixLabors.ImageSharp.Size(width: 500, height: 500)

                        }));


                        newExtension = Path.GetExtension(file.FileName);
                        if (newExtension == ".png")
                        {
                            img.Save($"{masc_Imagen}\\{imageName}{newExtension}", new PngEncoder());
                        }
                        else if(newExtension == ".jpeg")
                        {
                            img.Save($"{masc_Imagen}\\{imageName}{newExtension}", new JpegEncoder());
                        }
                        else
                        {
                            img.Save($"{masc_Imagen}\\{imageName}{newExtension}", new GifEncoder());
                        }

                        mascota.masc_Imagen = $"~/images/mascotas-images/{imageName}{newExtension}";
                     
                    }
                }

              _mascotasRepository.updateMascotasImagen1(mascota.masc_Imagen, id);
                return "La Imagen de la mascota ha sido Actualizada.";
            }
            catch (Exception e)
            {
                var hola = e;

                return "Error";
            }
        }


    }
}
