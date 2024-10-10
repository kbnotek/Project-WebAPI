using ProjetoFinal.ORM;

namespace ProjetoFinal.Model
{
    public class Endereco
    {
        public int Id { get; set; }

        public string Logradouro { get; set; } = null!;

        public string Cidade { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public int Cep { get; set; }

        public string PontoReferencia { get; set; } = null!;

        public int N { get; set; }

        public int FkCliente { get; set; }
        
    }
}
