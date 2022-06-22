using VYSA.Models;
using System.Data.SqlClient;

namespace VYSA.Repositories
{
    public class CategoriaSqlRepository: DBContext, ICategoriaRepository
    {
        public void Create(Categoria categoria)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"INSERT INTO Categorias 
                    VALUES (@nome)";

                cmd.Parameters.AddWithValue("@nome", categoria.Nome);

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
        public List<Categoria> Read()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Categorias";

            SqlDataReader reader = cmd.ExecuteReader();

            List<Categoria> lista = new List<Categoria>();

            while(reader.Read())
            {
                lista.Add(new Categoria {
                    IdCategoria = (int)reader["IdCategoria"],
                    Nome = (string)reader["Nome"]
                });
            }

            return lista;
        }
        
        public Categoria Read(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM Categorias WHERE IdCategoria = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Categoria {
                        IdCategoria = (int)reader["IdCategoria"],
                        Nome = (string)reader["Nome"]
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

        public void Update(int id, Categoria categoria)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"UPDATE Categorias 
                    SET Nome = @nome
                    WHERE IdCategoria = @id";

                cmd.Parameters.AddWithValue("@nome", categoria.Nome);
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
                cmd.CommandText = @"DELETE FROM Categorias WHERE IdCategoria = @id";

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