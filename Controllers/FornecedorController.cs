using VYSA.Models;
using Microsoft.AspNetCore.Mvc;
using VYSA.Repositories;

namespace VYSA.Controllers
{
    public class FornecedorController : Controller
    {
        private IFornecedorRepository repository;
        private ICategoriaRepository categoriaRepository;

        public FornecedorController(IFornecedorRepository repository, ICategoriaRepository categoriaRepository)
        {
            this.repository = repository;
            this.categoriaRepository = categoriaRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            List<Fornecedor> fornecedores = repository.Read();
            return View(fornecedores);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            return View();
        } 
        
        [HttpPost]
        public ActionResult Create(Fornecedor fornecedor)
        {
            repository.Create(fornecedor);
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
            var fornecedor = repository.Read(id);
            return View(fornecedor);
        }

        [HttpPost]
        public ActionResult Update(int id, Fornecedor fornecedor)
        {
            repository.Update(id, fornecedor);
            return RedirectToAction("Index");
        } 
    }
}