using VYSA.Models;
using Microsoft.AspNetCore.Mvc;
using VYSA.Repositories;

namespace VYSA.Controllers
{
    public class FuncionarioController : Controller
    {
        private IFuncionarioRepository repository;
        private ICategoriaRepository categoriaRepository;

        public FuncionarioController(IFuncionarioRepository repository, ICategoriaRepository categoriaRepository)
        {
            this.repository = repository;
            this.categoriaRepository = categoriaRepository;
        }

        public ActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();

        }

        [HttpPost]
        public ActionResult Login(FuncionarioViewModel model)
        {

            var Funcionario = repository.Read(model.Senha, model.Login);
            
            try
            {
                if(model.Login == "admin@admin" && model.Senha == "admin123")
                {
                    Funcionario funcionario = new Funcionario();
                    funcionario.Nome = "Admin";
                    funcionario.Id = 0;

                    HttpContext.Session.SetInt32("Id", funcionario.Id);
                    HttpContext.Session.SetString("Nome", funcionario.Nome);

                    return RedirectToAction("Index", "Home");
                }

                else if(model.Login == Funcionario.Login && model.Senha == Funcionario.Senha)
                {
                    HttpContext.Session.SetInt32("Id", Funcionario.Id);
                    HttpContext.Session.SetString("Nome", Funcionario.Nome);

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    return RedirectToAction("Login", "Funcionario");     
                }
            }

            catch
            {
                ViewBag.Message = "Usuário e/ou senha incorretos";
                return View(model);
            }            
        }

        public ActionResult Index()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            List<Funcionario> funcionarios = repository.Read();
            return View(funcionarios);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categorias = categoriaRepository.Read();
            return View();
        } 
        
        [HttpPost]
        public ActionResult Create(Funcionario funcionario)
        {
            repository.Create(funcionario);
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
            var funcionario = repository.Read(id);
            return View(funcionario);
        }

        [HttpPost]
        public ActionResult Update(int id, Funcionario funcionario)
        {
            repository.Update(id, funcionario);
            return RedirectToAction("Index");
        } 
    }
}