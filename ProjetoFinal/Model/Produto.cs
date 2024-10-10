using ProjetoFinal.ORM;
using System.Text.Json.Serialization;

namespace ProjetoFinal.Model
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public int Preco { get; set; }

        public int Quant { get; set; }
        [JsonIgnore]
        public byte[]? NotaFiscal { get; set; }
        [JsonIgnore] // Ignora a serialização deste campo
        public string? FotoBase64 => NotaFiscal != null ? Convert.ToBase64String(NotaFiscal) : null;

        public string UrlNotaFiscal { get; set; } // Certifique-se de que esta propriedade esteja visível

    }
}
