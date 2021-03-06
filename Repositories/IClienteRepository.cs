using VYSA.Models;

namespace VYSA.Repositories
{
    public interface IClienteRepository
    {
        void Create(Cliente cliente);
        List<Cliente> Read();
        Cliente Read(int id);
        void Update(int id, Cliente cliente);
        void Delete(int id);
    }
}