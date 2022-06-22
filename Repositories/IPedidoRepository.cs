using VYSA.Models;

namespace VYSA.Repositories
{
    public interface IPedidoRepository
    {
        void Create(Pedido pedido);
        List<Pedido> Read();
        Pedido Read(int id);
        void Update(int id, Pedido pedido);
        void Delete(int id);
    }
}