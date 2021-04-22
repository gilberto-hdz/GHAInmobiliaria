using GHAInmobiliaria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHAInmobiliaria.Controllers
{
    public class InmueblesController : Controller
    {
        private readonly IConfiguration config;
        private readonly RepositorioPropietario repositorioPropietario;
        private readonly RepositorioInmueble repositorioInmueble;

        public InmueblesController(IConfiguration config)
        {
            this.config = config;
            this.repositorioPropietario = new RepositorioPropietario(config);
            this.repositorioInmueble = new RepositorioInmueble(config);
        }

        // GET: InmueblesController
        public ActionResult Index()
        {
            try
            {
                List<Inmueble> lista = repositorioInmueble.ObtenerTodos();
                return View(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: InmueblesController/Detalles/5
        public ActionResult Detalles(int id)
        {
            try
            {
                Inmueble inmueble = repositorioInmueble.ObtenerPorId(id);
                return View(inmueble);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: InmueblesController/Crear
        public ActionResult Crear()
        {
            ViewData["Propietarios"] = repositorioPropietario.ObtenerTodos();
            return View();
        }

        // POST: InmueblesController/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Inmueble inmueble)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorioInmueble.Alta(inmueble);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["Propietarios"] = repositorioPropietario.ObtenerTodos();
                    return View(inmueble);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        // GET: InmueblesController/Editar/5
        public ActionResult Editar(int id)
        {
            try
            {
                Inmueble inmueble = repositorioInmueble.ObtenerPorId(id);
                ViewData["Propietarios"] = repositorioPropietario.ObtenerTodos();
                return View(inmueble);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: InmueblesController/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Inmueble inmueble)
        {
            try
            {
                repositorioInmueble.Modificacion(inmueble);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                throw;
                return View();
            }
        }

        // GET: InmueblesController/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            try
            {
                Inmueble inmueble = repositorioInmueble.ObtenerPorId(id);
                return View(inmueble);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: InmueblesController/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, Inmueble inmueble)
        {
            try
            {
                repositorioInmueble.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                throw;
                return View();
            }
        }
    }
}
