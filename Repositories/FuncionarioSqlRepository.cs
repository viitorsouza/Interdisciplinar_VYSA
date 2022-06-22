using System.Data.SqlClient;
using VYSA.Models;

namespace VYSA.Repositories
{
    public class FuncionarioSqlRepository : DBContext, IFuncionarioRepository
    {
        public void Create(Funcionario funcionario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_add_funcionario";

                cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@cpf", funcionario.CPF);
                cmd.Parameters.AddWithValue("@endereco", funcionario.Endereco);
                cmd.Parameters.AddWithValue("@status", funcionario.Status);
                cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                cmd.Parameters.AddWithValue("@endEmail", funcionario.endEmail);
                cmd.Parameters.AddWithValue("@salario", funcionario.Salario);
                cmd.Parameters.AddWithValue("@login", funcionario.Login);
                cmd.Parameters.AddWithValue("@senha", funcionario.Senha);                
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
                cmd.CommandText = @"DELETE FROM Funcionarios WHERE pessoa_id = @id";

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

        public List<Funcionario> Read()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vFuncionarios";

                SqlDataReader reader = cmd.ExecuteReader();

                List<Funcionario> lista = new List<Funcionario>();

                while(reader.Read())
                {
                    lista.Add(
                        new Funcionario {
                            Id = (int)reader["IdPessoa"],
                            Nome = (string)reader["Nome"],
                            Salario = (decimal)reader["Salario"],
                            Login = (string)reader["Login"],
                            Status = (int)reader["Status"],
                            Telefone = (string)reader["Telefone"],
                            endEmail = (string)reader["endEmail"]
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

        public Funcionario Read(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vFuncionarios WHERE pessoa_id = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Funcionario {
                        Id = (int)reader["pessoa_id"],
                        Nome = (string)reader["Nome"],
                        CPF = (string)reader["CPF"],
                        Endereco = (string)reader["Endereco"],
                        Status = (int)reader["Status"],
                        Telefone = (string)reader["Telefone"],
                        endEmail = (string)reader["endEmail"],
                        Salario = (decimal)reader["Salario"],
                        Login = (string)reader["Login"],
                        Senha = (string)reader["Senha"]
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

        public void Update(int id, Funcionario funcionario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_update_funcionario";

                cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@cpf", funcionario.CPF);
                cmd.Parameters.AddWithValue("@endereco", funcionario.Endereco);
                cmd.Parameters.AddWithValue("@status", funcionario.Status);
                cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                cmd.Parameters.AddWithValue("@endEmail", funcionario.endEmail);
                cmd.Parameters.AddWithValue("@salario", funcionario.Salario);
                cmd.Parameters.AddWithValue("@login", funcionario.Login);
                cmd.Parameters.AddWithValue("@senha", funcionario.Senha);
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

        public Funcionario Read(string senha, string login)
        {
            try
            {                
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vFuncionarios WHERE Login = @Login and Senha = @Senha";

                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Senha", senha);

                SqlDataReader reader = cmd.ExecuteReader();


                if(reader.Read())
                {
                       return new Funcionario {
                            Id = (int)reader["IdPessoa"],
                            Nome = (string)reader["Nome"],
                            Salario = (decimal)reader["Salario"],
                            Login = (string)reader["Login"],
                            Senha = (string)reader["Senha"],
                            Status = (int)reader["Status"]
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
    }
}