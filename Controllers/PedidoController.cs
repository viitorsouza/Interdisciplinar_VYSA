using VYSA.Repositories;
using VYSA.Models;
using Microsoft.AspNetCore.Mvc;

namespace VYSA.Controllers
{
    public class PedidoController : Controller
    {
        private IPedidoRepository repository;
        private ICategoriaRepository categoriaRepository;
        private IProdutoRepository produtoRepository;
        private IFuncionarioRepository funcionarioRepository;
        private IClienteRepository clienteRepository;

        public PedidoController(IPedidoRepository repository, ICategoriaRepository categoriaRepository, IProdutoRepository produtoRepository,
        IFuncionarioRepository funcionarioRepository, IClienteRepository clienteRepository)
        {
            this.repository = repository;
            this.categoriaRepository = categoriaRepository;
            this.produtoRepository = produtoRepository;
            this.funcionarioRepository = funcionarioRepository;
            this.clienteRepository = clienteRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            List<Pedido> pedidos = repository.Read();
            return View(pedidos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Produtos = produtoRepository.Read();
            ViewBag.Categorias = categoriaRepository.Read();
            ViewBag.Funcionarios = funcionarioRepository.Read();
            ViewBag.Clientes = clienteRepository.Read();
            return View();
        } 
        
        [HttpPost]
        public ActionResult Create(Pedido pedido)
        {
            repository.Create(pedido);
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
            var pedido = repository.Read(id);
            return View(pedido);
        }

        [HttpPost]
        public ActionResult Update(int id, Pedido pedido)
        {
            repository.Update(id, pedido);
            return RedirectToAction("Index");
        } 
    }
}