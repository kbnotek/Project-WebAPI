using ProjetoFinal.Model;

namespace ProjetoFinal.Repositorio
{
    public interface IVenda
    {
        public void Add(Venda venda);
        public List<Venda> GetAll();
        public Venda GetById(int id);
        void Update(Venda venda);
        void Delete(int id);
    }
}
