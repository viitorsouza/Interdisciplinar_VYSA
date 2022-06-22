using VYSA.Models;

namespace VYSA.Repositories
{
    public interface ICategoriaRepository
    {
        void Create(Categoria categoria);
        List<Categoria> Read();
        Categoria Read(int id);
        void Update(int id, Categoria categoria);
        void Delete(int id);
    }
}