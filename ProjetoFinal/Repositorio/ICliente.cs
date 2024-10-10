using ProjetoFinal.Model;

namespace ProjetoFinal.Repositorio
{
    public interface ICliente
    {
        public void Add(Cliente cliente, IFormFile ResgitroGeral);
        public List<Cliente> GetAll();
        public Cliente GetById(int id);
        void Update(Cliente cliente, IFormFile foto);
        void Delete(int id);
    }
}
