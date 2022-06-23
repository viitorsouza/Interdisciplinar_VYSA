using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using VYSA.Models;


namespace VYSA.Repositories
{
    public class PedidoSqlRepository : DBContext, IPedidoRepository
    {

        public void Create(Pedido pedido)
        {            
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"INSERT INTO Pedidos 
                    VALUES (GETDATE(), @valor, @tipo, @cliente_id, @funcionario_id)";

                cmd.Parameters.AddWithValue("@valor", pedido.Valor);
                cmd.Parameters.AddWithValue("@tipo", pedido.Tipo);
                cmd.Parameters.AddWithValue("@cliente_id", pedido.IdCliente);
                cmd.Parameters.AddWithValue("@funcionario_id", pedido.IdFuncionario);


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

        public List<Pedido> Read()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM vPedidos";

                SqlDataReader reader = cmd.ExecuteReader();

                List<Pedido> lista = new List<Pedido>();

                while(reader.Read())
                {
                    lista.Add(
                        new Pedido {
                            IdPedido = (int)reader["IdPedido"],
                            Data = (DateTime)reader["Data"],
                            Valor = (decimal)reader["Valor"],
                            Tipo = (int)reader["Tipo"],
                            IdCliente = (int)reader["cliente_id"],
                            IdFuncionario = (int)reader["funcionario_id"]
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

        public Pedido Read(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM Pedido WHERE IdPedido = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Pedido {
                        IdPedido = (int)reader["IdPedido"],
                        Data = (DateTime)reader["Data"],
                        Valor = (decimal)reader["Valor"],
                        IdCliente = (int)reader["IdCliente"],
                        IdFuncionario = (int)reader["IdFuncionario"]
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

        public void Update(int id, Pedido pedido)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"UPDATE Pedidos 
                    SET Valor = @valor, IdCliente = @IdCliente, IdFuncionario = @IdFuncionario
                    WHERE IdPedido = @id";

                cmd.Parameters.AddWithValue("@valor", pedido.Valor);
                cmd.Parameters.AddWithValue("@idCliente", pedido.IdCliente);
                cmd.Parameters.AddWithValue("@idFuncionario", pedido.IdFuncionario);

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
                cmd.CommandText = @"DELETE FROM Pedido WHERE IdPedido = @id";

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