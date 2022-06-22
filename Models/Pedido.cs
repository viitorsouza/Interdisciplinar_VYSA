namespace VYSA.Models
{
    public class Pedido
    {
        //propriedades
        public int IdPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int IdCliente {get; set;}            
        public int IdFuncionario {get; set;} 
        public int Tipo {get; set;} 

     }
} 