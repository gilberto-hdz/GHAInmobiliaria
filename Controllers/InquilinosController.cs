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
    public class InquilinosController : Controller
    {

        private readonly IConfiguration config;
        private readonly RepositorioInquilino repositorioInquilino;

        public InquilinosController(IConfiguration config)
        {
            this.config = config;
            this.repositorioInquilino = new RepositorioInquilino(config);
        }

        // GET: InquilinosController
        public ActionResult Index()
        {
            try
            {
                List<Inquilino> lista = repositorioInquilino.ObtenerTodos();
                return View(lista);
            }
            catch (Exception)
            {
                throw;
            }

        }

        // GET: InquilinosController/Detalles/5
        public ActionResult Detalles(int id)
        {
            try
            {
                Inquilino inquilino = repositorioInquilino.ObtenerPorId(id);
                return View(inquilino);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: InquilinosController/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: InquilinosController/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Inquilino inquilino)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repositorioInquilino.Alta(inquilino);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(inquilino);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: InquilinosController/Editar/5
        public ActionResult Editar(int id)
        {
            try
            {
                Inquilino inquilino = repositorioInquilino.ObtenerPorId(id);
                return View(inquilino);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: InquilinosController/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Inquilino inquilino)
        {
            try
            {
                repositorioInquilino.Modificacion(inquilino);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: InquilinosController/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            Inquilino inquilino = repositorioInquilino.ObtenerPorId(id);
            return View(inquilino);
        }

        // POST: InquilinosController/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, Inquilino inquilino)
        {
            try
            {
                repositorioInquilino.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
