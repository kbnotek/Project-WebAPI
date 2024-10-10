using ProjetoFinal.Model;

namespace ProjetoFinal.Repositorio
{
    public interface IProduto
    {

        public void Add(Produto produto ,IFormFile notaFiscal);
        public List<Produto> GetAll();
        public Produto GetById(int id);
        void Update(Produto produto, IFormFile notaFiscal);
        void Delete(int id);
    }
}
