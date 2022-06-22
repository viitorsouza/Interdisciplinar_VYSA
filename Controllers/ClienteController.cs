using VYSA.Models;
using Microsoft.AspNetCore.Mvc;
using VYSA.Repositories;

namespace VYSA.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteRepository repository;
        private ICategoriaRepository categoriaRepository;

        public ClienteController(IClienteRepository repository, ICategoriaRepository categoriaRepository)
        {
            this.repository = repository;
            this.categoriaRepository = categoriaRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            List<Cliente> clientes = repository.Read();
            return View(clientes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            return View();
        } 
        
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            repository.Create(cliente);
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
            var cliente = repository.Read(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Update(int id, Cliente cliente)
        {
            repository.Update(id, cliente);
            return RedirectToAction("Index");
        } 
    }
}