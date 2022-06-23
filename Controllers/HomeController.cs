using VYSA.Repositories;
using VYSA.Models;
using Microsoft.AspNetCore.Mvc;

namespace VYSA.Controllers
{
    public class HomeController : Controller
    {

        private IProdutoRepository repository;
        private ICategoriaRepository categoriaRepository;
        private IPedidoRepository pedidoRepository;

        public HomeController(IProdutoRepository repository, ICategoriaRepository categoriaRepository, IPedidoRepository pedidoRepository)
        {
            this.repository = repository;
            this.categoriaRepository = categoriaRepository;
            this.pedidoRepository = pedidoRepository;
        }
            
        public ActionResult Index()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            
            int? id = HttpContext.Session.GetInt32("Id") as int?;
            if(id == null)
            {
                return RedirectToAction("Login", "Funcionario");
            }
            
            Console.WriteLine("Login");

            ViewBag.Pedidos = pedidoRepository.Read();
            List<Produto> produtos = repository.ReadByEstoque();
            return View("Index", produtos);

        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var produto = repository.Read(id);
            return View(produto);
        }

        [HttpPost]
        public ActionResult Update(int id, Produto produto)
        {
            repository.Update(id, produto);
            return RedirectToAction("Index");
        } 
        
    }
}