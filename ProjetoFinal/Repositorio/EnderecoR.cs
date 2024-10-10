using ProjetoFinal.Model;
using ProjetoFinal.ORM;

namespace ProjetoFinal.Repositorio
{
    public class EnderecoR : IEndereco
    {
        private BdQuantumContext _context;

        public EnderecoR(BdQuantumContext context)
        {
            _context = context;
        }
        public void Add(Endereco endereco)

        {
            var tbEndereco = new TbEndereco()
            {
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                N = endereco.N,
                FkCliente = endereco.FkCliente,
               

                // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbEnderecos.Add(tbEndereco);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var tbendereco = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbendereco != null)
            {
                // Remove a entidade do contexto
                _context.TbEnderecos.Remove(tbendereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }

        public List<Endereco> GetAll()
        {
            List<Endereco> listFun = new List<Endereco>();

            var listTb = _context.TbEnderecos.ToList();

            foreach (var item in listTb)
            {
                var endereco = new Endereco
                {
                    Id = item.Id,
                   Logradouro = item.Logradouro,
                   Cidade = item.Cidade,
                   Estado = item.Estado,
                   Cep= item.Cep,
                   PontoReferencia= item.PontoReferencia,
                   N = item.N,
                   FkCliente= item.FkCliente,
                };

                listFun.Add(endereco);
            }

            return listFun;
        }

        public Endereco GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var endereco = new Endereco
            {
                Id = item.Id,
                Logradouro = item.Logradouro,
                Cidade = item.Cidade,
                Estado = item.Estado,
                Cep = item.Cep,
                PontoReferencia = item.PontoReferencia,
                N = item.N,
                FkCliente = item.FkCliente,
                // Mantém o campo Foto como byte[]
            };

            return endereco; // Retorna o funcionário encontrado
        }

        public void Update(Endereco endereco)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbendereco = _context.TbEnderecos.FirstOrDefault(f => f.Id == endereco.Id);

            // Verifica se a entidade foi encontrada
            if (tbendereco != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbendereco.Logradouro = tbendereco.Logradouro;
                tbendereco.Cidade = tbendereco.Cidade;
                tbendereco.Estado = tbendereco.Estado;
                tbendereco.Cep = tbendereco.Cep;
                tbendereco.PontoReferencia =tbendereco.PontoReferencia;
                tbendereco.N = tbendereco.N;
                tbendereco.FkCliente = tbendereco.FkCliente;
                


                // Atualiza as informações no contexto
                _context.TbEnderecos.Update(tbendereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }
    }
}
