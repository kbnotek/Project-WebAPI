namespace ProjetoFinal.Model
{
    public class ClienteDto
    {
        public string Nome { get; set; } = null!;

        public string Telefone { get; set; } = null!;
          public IFormFile RegistroGeral { get; set; } //Campo pra receber a Foto !

    }
}
