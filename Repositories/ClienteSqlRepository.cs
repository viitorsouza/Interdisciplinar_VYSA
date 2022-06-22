using System.Data.SqlClient;
using VYSA.Models;

namespace VYSA.Repositories
{
    public class ClienteSqlRepository : DBContext, IClienteRepository
    {
        public void Create(Cliente cliente)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_add_cliente";

                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf", cliente.CPF);
                cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@status", cliente.Status);
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@endEmail", cliente.endEmail);
                cmd.Parameters.AddWithValue("@idade", cliente.Idade);              
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);              
            }
            finally
            {
                Dispose();
            }
        }

        public void Delete(int id)
        {
            try{
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"DELETE FROM Clientes WHERE pessoa_id = @id";

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);              
            }
            finally
            {
                Dispose();
            }
        }

        public List<Cliente> Read()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vClientes";

                SqlDataReader reader = cmd.ExecuteReader();

                List<Cliente> lista = new List<Cliente>();

                while(reader.Read())
                {
                    lista.Add(
                        new Cliente {
                            Id = (int)reader["pessoa_id"],
                            Nome = (string)reader["Nome"],
                            CPF = (string)reader["CPF"],
                            Endereco = (string)reader["Endereco"],
                            Status = (int)reader["Status"],
                            Telefone = (string)reader["Telefone"],
                            endEmail = (string)reader["endEmail"],
                            Idade = (int)reader["Idade"]
                        }
                    );
                }

                return lista;
            }
            catch(Exception ex) 
            {
                return null;
            }
            finally
            {
                Dispose();
            }
        }

        public Cliente Read(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vClientes WHERE pessoa_id = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Cliente {
                        Id = (int)reader["pessoa_id"],
                        Nome = (string)reader["Nome"],
                        CPF = (string)reader["CPF"],
                        Endereco = (string)reader["Endereco"],
                        Status = (int)reader["Status"],
                        Telefone = (string)reader["Telefone"],
                        endEmail = (string)reader["endEmail"],
                        Idade = (int)reader["Idade"]
                    };
                }

                return null;
            }
            catch(Exception ex) 
            {
                return null;
            }
            finally
            {
                Dispose();
            }            
        }

        public void Update(int id, Cliente cliente)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_update_cliente";

                cmd.Parameters.AddWithValue("@pessoa_id", id);
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf", cliente.CPF);
                cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@status", cliente.Status);
                cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@endEmail", cliente.endEmail);
                cmd.Parameters.AddWithValue("@idade", cliente.Idade);


                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }
    }
}