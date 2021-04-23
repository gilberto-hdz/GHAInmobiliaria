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
    public class ContratosController : Controller
    {
        private readonly IConfiguration config;
        private RepositorioContrato repositorioContrato;
        private RepositorioInquilino repositorioInquilino;
        private RepositorioInmueble repositorioInmueble;

        public ContratosController(IConfiguration config)
        {
            this.config = config;
            repositorioContrato = new RepositorioContrato(config);
            repositorioInquilino = new RepositorioInquilino(config);
            repositorioInmueble = new RepositorioInmueble(config);
        }

        // GET: ContratosController
        public ActionResult Index()
        {
            try
            {
                List<Contrato> lista = repositorioContrato.ObtenerTodos();
                return View(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: ContratosController/Detalles/5
        public ActionResult Detalles(int id)
        {
            try
            {
                Contrato contrato = repositorioContrato.ObtenerPorId(id);
                return View(contrato);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: ContratosController/Crear
        public ActionResult Crear()
        {
            ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
            ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
            return View();
        }

        // POST: ContratosController/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Contrato contrato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorioContrato.Alta(contrato);
                    return RedirectToAction(nameof(Index));
                } else
                {
                    ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
                    ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
                    return View(contrato);
                }
                
            }
            catch(Exception)
            {
                throw;
            }
        }

        // GET: ContratosController/Editar/5
        public ActionResult Editar(int id)
        {
            try
            {
                Contrato contrato = repositorioContrato.ObtenerPorId(id);
                ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
                ViewBag.Inmuebles = repositorioInmueble.ObtenerTodos();
                return View(contrato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: ContratosController/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Contrato contrato)
        {
            try
            {
                repositorioContrato.Modificacion(contrato);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: ContratosController/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            try
            {
                Contrato contrato = repositorioContrato.ObtenerPorId(id);
                return View(contrato);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: ContratosController/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, Contrato contrato)
        {
            try
            {
                repositorioContrato.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
