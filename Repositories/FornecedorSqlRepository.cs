using System.Data.SqlClient;
using VYSA.Models;

namespace VYSA.Repositories
{
    public class FornecedorSqlRepository : DBContext, IFornecedorRepository
    {
        public void Create(Fornecedor fornecedor)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_add_fornecedor";

                cmd.Parameters.AddWithValue("@nome", fornecedor.Nome);
                cmd.Parameters.AddWithValue("@cpf", fornecedor.CPF);
                cmd.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
                cmd.Parameters.AddWithValue("@status", fornecedor.Status);
                cmd.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                cmd.Parameters.AddWithValue("@endEmail", fornecedor.endEmail);
                cmd.Parameters.AddWithValue("@empresa", fornecedor.Empresa);

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
                cmd.CommandText = @"DELETE FROM Fornecedores WHERE pessoa_id = @id";

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

        public List<Fornecedor> Read()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vFornecedores";

                SqlDataReader reader = cmd.ExecuteReader();

                List<Fornecedor> lista = new List<Fornecedor>();

                while(reader.Read())
                {
                    lista.Add(
                        new Fornecedor {
                            Id = (int)reader["IdPessoa"],
                            Nome = (string)reader["Nome"],
                            Status = (int)reader["Status"],
                            Telefone = (string)reader["Telefone"],
                            endEmail = (string)reader["endEmail"],
                            Empresa = (string)reader["Empresa"]
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

        public Fornecedor Read(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vFornecedores WHERE pessoa_id = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Fornecedor {
                        Id = (int)reader["pessoa_id"],
                        Nome = (string)reader["Nome"],
                        CPF = (string)reader["CPF"],
                        Endereco = (string)reader["Endereco"],
                        Status = (int)reader["Status"],
                        Telefone = (string)reader["Telefone"],
                        endEmail = (string)reader["endEmail"],
                        Empresa = (string)reader["Empresa"]
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

        public void Update(int id, Fornecedor fornecedor)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_update_fornecedor";

                cmd.Parameters.AddWithValue("@nome", fornecedor.Nome);
                cmd.Parameters.AddWithValue("@cpf", fornecedor.CPF);
                cmd.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
                cmd.Parameters.AddWithValue("@status", fornecedor.Status);
                cmd.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                cmd.Parameters.AddWithValue("@endEmail", fornecedor.endEmail);
                cmd.Parameters.AddWithValue("@empresa", fornecedor.Empresa);
                cmd.Parameters.AddWithValue("@pessoa_id", id);


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