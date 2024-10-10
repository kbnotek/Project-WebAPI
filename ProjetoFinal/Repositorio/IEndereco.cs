using ProjetoFinal.Model;

namespace ProjetoFinal.Repositorio
{
    public interface IEndereco
    {
        public void Add(Endereco endereco);
        public List<Endereco> GetAll();
        public Endereco GetById(int id);
        void Update(Endereco endereco);
        void Delete(int id);
    }
}
