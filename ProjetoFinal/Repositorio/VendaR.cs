using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Model;
using ProjetoFinal.ORM;

namespace ProjetoFinal.Repositorio
{
    public class VendaR : IVenda
    {
        private BdQuantumContext _context;

        public VendaR(BdQuantumContext context)
        {
            _context = context;
        }

        public void Add(Venda venda)
        {
            var tbVenda = new TbVendum()
            {
                Valor = venda.Valor,
                NotaF = venda.NotaF,
                FkProduto = venda.FkProduto,
                FkCliente = venda.FkCliente
            };

            // Adiciona a entidade ao contexto
            _context.TbVenda.Add(tbVenda);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Venda> GetAll()
        {
            throw new NotImplementedException();
        }

        public Venda GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Venda venda)
        {
            throw new NotImplementedException();
        }
    }
}
