using VYSA.Models;
using Microsoft.AspNetCore.Mvc;
using VYSA.Repositories;

namespace VYSA.Controllers
{
    public class ProdutoController : Controller
    {
        private IProdutoRepository repository;
        private ICategoriaRepository categoriaRepository;
        private IFornecedorRepository fornecedorRepository;

        public ProdutoController(IProdutoRepository repository, ICategoriaRepository categoriaRepository, IFornecedorRepository fornecedorRepository)
        {
            this.repository = repository;
            this.categoriaRepository = categoriaRepository;
            this.fornecedorRepository = fornecedorRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            List<Produto> produtos = repository.Read();
            return View(produtos);
        }

        public ActionResult Filter(int id)
        {
            ViewBag.Categorias = categoriaRepository.Read();
            List<Produto> produtos = repository.ReadByCategoria(id);
            return View("Index", produtos);
        } 
        public ActionResult Home()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            List<Produto> produtos = repository.ReadByEstoque();
            return View("Index", produtos);
        } 

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            ViewBag.Fornecedores = fornecedorRepository.Read();
            return View();
        } 

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            repository.Create(produto);
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
            ViewBag.Fornecedores = fornecedorRepository.Read();
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