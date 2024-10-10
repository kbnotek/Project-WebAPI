using ProjetoFinal.ORM;
using System.Text.Json.Serialization;

namespace ProjetoFinal.Model
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Telefone { get; set; } = null!;
        [JsonIgnore]
        public byte[]? RegistroGeral { get; set; }
        [JsonIgnore] // Ignora a serialização deste campo
        public string? FotoBase64 => RegistroGeral != null ? Convert.ToBase64String(RegistroGeral) : null;

        public string UrlRegistroGeral { get; set; } // Certifique-se de que esta propriedade esteja visível

    }
}
