using VYSA.Models;

namespace VYSA.Repositories
{
    public interface IFuncionarioRepository
    {
        void Create(Funcionario funcionario);
        List<Funcionario> Read();
        Funcionario Read(int id);
        Funcionario Read(string senha, string login);
        void Update(int id, Funcionario funcionario);
        void Delete(int id);
    }
}