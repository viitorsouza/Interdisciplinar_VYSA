using System.Data.SqlClient;

namespace VYSA.Repositories
{
    public abstract class DBContext
    {
        private readonly string strConn = @"Data Source=VITOR\SQLEXPRESS;
        Initial Catalog=VYSA;
        Integrated Security=True;";

         protected SqlConnection connection;

         public DBContext()
         {
             connection = new SqlConnection(strConn);
             connection.Open();
         }

         public void Dispose()
         {
             connection.Close();
         }
    }
}