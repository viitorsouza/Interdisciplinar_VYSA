using System.Data.SqlClient;
using VYSA.Models;

namespace VYSA.Repositories
{
    public class ProdutoSqlRepository : DBContext, IProdutoRepository
    {
        public void Create(Produto produto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"INSERT INTO Produtos 
                    VALUES (@nome, @descricao, @estoque, @preco, @cod_barras, @categoria_id, @fornecedor_id, @status)";

                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@estoque", produto.Estoque);
                cmd.Parameters.AddWithValue("@preco", produto.Preco);
                cmd.Parameters.AddWithValue("@cod_barras", produto.Cod_barras);
                cmd.Parameters.AddWithValue("@categoria_id", produto.IdCategoria);
                cmd.Parameters.AddWithValue("@fornecedor_id", produto.IdFornecedor);
                cmd.Parameters.AddWithValue("@status", produto.Status);

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

        public List<Produto> Read()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vProdutos";

                SqlDataReader reader = cmd.ExecuteReader();

                List<Produto> lista = new List<Produto>();

                while(reader.Read())
                {
                    lista.Add(
                        new Produto {
                            IdProduto = (int)reader["IdProduto"],
                            Nome = (string)reader["nome"],
                            Descricao = (string)reader["descricao"],
                            Estoque = (int)reader["estoque"],
                            Preco = (decimal)reader["preco"],
                            Cod_barras = (int)reader["cod_barras"],
                            IdCategoria = (int)reader["categoria_id"],
                            Status = (int)reader["status"],
                            IdFornecedor = (int)reader["fornecedor_id"]
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

        public List<Produto> ReadByEstoque()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vEstoqueBaixo";

                SqlDataReader reader = cmd.ExecuteReader();

                List<Produto> lista = new List<Produto>();

                while(reader.Read())
                {
                    lista.Add(
                        new Produto {
                            IdProduto = (int)reader["IdProduto"],
                            Nome = (string)reader["nome"],
                            Descricao = (string)reader["descricao"],
                            Estoque = (int)reader["estoque"],
                            Preco = (decimal)reader["preco"],
                            Cod_barras = (int)reader["cod_barras"],
                            IdCategoria = (int)reader["categoria_id"],
                            IdFornecedor = (int)reader["fornecedor_id"],
                            Status = (int)reader["status"]

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

        public List<Produto> ReadByCategoria(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vProdutos WHERE categoria_id = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                List<Produto> lista = new List<Produto>();

                while(reader.Read())
                {
                    lista.Add(
                        new Produto {
                            IdProduto = (int)reader["IdProduto"],
                            Nome = (string)reader["nome"],
                            Descricao = (string)reader["descricao"],
                            Estoque = (int)reader["estoque"],
                            Preco = (decimal)reader["preco"],
                            Cod_barras = (int)reader["cod_barras"],
                            IdCategoria = (int)reader["categoria_id"],
                            Status = (int)reader["status"],
                            IdFornecedor = (int)reader["fornecedor_id"]
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
           
        public Produto Read(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM Produtos WHERE IdProduto = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Produto {
                        IdProduto = (int)reader["IdProduto"],
                        Nome = (string)reader["nome"],
                        Descricao = (string)reader["descricao"],
                        Estoque = (int)reader["estoque"],
                        Preco = (decimal)reader["preco"],
                        Cod_barras = (int)reader["cod_barras"],
                        IdCategoria = (int)reader["categoria_id"],
                        Status = (int)reader["status"],
                        IdFornecedor = (int)reader["fornecedor_id"]
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

        public void Update(int id, Produto produto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"UPDATE Produtos 
                    SET Nome = @nome, Descricao = @descricao, Estoque = @estoque, Preco = @preco, Cod_barras = @cod_barras, categoria_id = @categoria_id, fornecedor_id = @fornecedor_id, Status = @status
                    WHERE IdProduto = @id";

                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("@estoque", produto.Estoque);
                cmd.Parameters.AddWithValue("@preco", produto.Preco);
                cmd.Parameters.AddWithValue("@cod_barras", produto.Cod_barras);
                cmd.Parameters.AddWithValue("@categoria_id", produto.IdCategoria);
                cmd.Parameters.AddWithValue("@fornecedor_id", produto.IdFornecedor);
                cmd.Parameters.AddWithValue("@status", produto.Status);
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

        public void Delete(int id)
        {
            try{
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"DELETE FROM Produtos WHERE IdProduto = @id";

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
    }
}