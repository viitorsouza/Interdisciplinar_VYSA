using VYSA.Repositories;

namespace VYSA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IProdutoRepository, ProdutoSqlRepository>();
            builder.Services.AddTransient<ICategoriaRepository, CategoriaSqlRepository>();
            builder.Services.AddTransient<IFuncionarioRepository, FuncionarioSqlRepository>();
            builder.Services.AddTransient<IFornecedorRepository, FornecedorSqlRepository>();
            builder.Services.AddTransient<IClienteRepository, ClienteSqlRepository>();
            builder.Services.AddTransient<IPedidoRepository, PedidoSqlRepository>();

            var app = builder.Build();

            app.UseSession();
            app.MapDefaultControllerRoute();
            app.Run();
        }
    }
}