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
    public class PropietariosController : Controller
    {
        private readonly IConfiguration config;
        private readonly RepositorioPropietario repositorioPropietario;

        public PropietariosController(IConfiguration config)
        {
            this.config = config;
            this.repositorioPropietario = new RepositorioPropietario(config);
        }

        // GET: PropietariosController
        public ActionResult Index()
        {
            try
            {
                List<Propietario> lista = repositorioPropietario.ObtenerTodos();
                return View(lista);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // GET: PropietariosController/Detalles/5
        public ActionResult Detalles(int id)
        {
            try
            {
                Propietario propietario = repositorioPropietario.ObtenerPorId(id);
                return View(propietario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: PropietariosController/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: PropietariosController/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Propietario propietario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorioPropietario.Alta(propietario);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(propietario);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: PropietariosController/Editar/5
        public ActionResult Editar(int id)
        {
            try
            {
                Propietario propietario = repositorioPropietario.ObtenerPorId(id);
                return View(propietario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: PropietariosController/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Propietario propietario)
        {
            try
            {
                repositorioPropietario.Modificacion(propietario);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                throw;
            }
        }

        // GET: PropietariosController/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            Propietario propietario = repositorioPropietario.ObtenerPorId(id);
            return View(propietario);
        }

        // POST: PropietariosController/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, Propietario propietario)
        {
            try
            {
                repositorioPropietario.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
