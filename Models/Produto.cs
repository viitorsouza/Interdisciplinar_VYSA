namespace VYSA.Models
{    public class Produto
    {
        // propriedades
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public decimal Preco { get; set; }  
        public int Cod_barras { get; set; }       
        public int IdCategoria { get; set; }
        public int Status {get; set;}
        public int IdFornecedor { get; set; }
    }
}