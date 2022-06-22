using VYSA.Models;

namespace VYSA.Repositories
{
    public interface IProdutoRepository
    {
        void Create(Produto produto);
        List<Produto> Read();
        List<Produto> ReadByCategoria(int id);
        List<Produto> ReadByEstoque();
        Produto Read(int id);
        void Update(int id, Produto produto);
        void Delete(int id);
    }
}