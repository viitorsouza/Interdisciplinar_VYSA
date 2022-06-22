using VYSA.Models;
using Microsoft.AspNetCore.Mvc;
using VYSA.Repositories;

namespace VYSA.Controllers
{
    public class CategoriaController : Controller
    {
        private ICategoriaRepository repository;
        private ICategoriaRepository categoriaRepository;

        public CategoriaController(ICategoriaRepository repository, ICategoriaRepository categoriaRepository)
        {
            this.repository = repository;
            this.categoriaRepository = categoriaRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            List<Categoria> categorias = repository.Read();
            return View(categorias);
        }
                
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Categoria categoria)
        {
            repository.Create(categoria);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewBag.Categorias = categoriaRepository.Read();
            var categoria = repository.Read(id);
            return View(categoria);
        }

        [HttpPost]
        public ActionResult Update(int id, Categoria categoria)
        {
            repository.Update(id, categoria);
            return RedirectToAction("Index");
        } 
    }
}