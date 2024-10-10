using ProjetoFinal.ORM;

namespace ProjetoFinal.Model
{
    public class Venda
    {
        public int Id { get; set; }

        public int Valor { get; set; }

        public string NotaF { get; set; } = null!;

        public int FkProduto { get; set; }

        public int FkCliente { get; set; }

    }
}
