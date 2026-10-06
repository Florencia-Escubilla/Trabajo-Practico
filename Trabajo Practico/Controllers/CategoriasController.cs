using Microsoft.AspNetCore.Mvc;
using Trabajo_Practico.Models;
using Trabajo_Practico.Repositories;

namespace Trabajo_Practico.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _repository;

        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            List<Categoria> categorias = _repository.ObtenerTodas();
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            categoria.Activa = true;
            _repository.Agregar(categoria);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Categoria? categoria = _repository.ObtenerPorId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            _repository.Actualizar(categoria);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _repository.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reactivar(int id)
        {
            _repository.Reactivar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}