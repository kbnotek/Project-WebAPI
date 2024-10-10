namespace ProjetoFinal.Model
{
    public class ProdutoDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public int Preco { get; set; }

        public int Quant { get; set; }
        public IFormFile NotaFiscal { get; set; } //Campo pra receber a Foto !

    }
}
