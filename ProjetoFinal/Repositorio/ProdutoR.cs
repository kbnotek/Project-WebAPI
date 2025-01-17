﻿
using ProjetoFinal.Model;
using ProjetoFinal.ORM;

namespace ProjetoFinal.Repositorio
{
    public class ProdutoR : IProduto
    {
        private BdQuantumContext _context;

        public ProdutoR(BdQuantumContext context)
        {
            _context = context;
        }

        public void Add(Produto produto, IFormFile notaFiscal)
        {
            // Verifica se uma foto foi enviada
            byte[] notaFiscalBytes = null;
            if (notaFiscal != null && notaFiscal.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    notaFiscal.CopyTo(memoryStream);
                    notaFiscalBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbproduto = new TbProduto()
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quant = produto.Quant,
                NotaFiscal = notaFiscalBytes
                
            };

            // Adiciona a entidade ao contexto
            _context.TbProdutos.Add(tbproduto);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbproduto = _context.TbProdutos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbproduto != null)
            {
                // Remove a entidade do contexto
                _context.TbProdutos.Remove(tbproduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }

        public List<Produto> GetAll()
        {
            List<Produto> listFun = new List<Produto>();

            var listTb = _context.TbProdutos.ToList();

            foreach (var item in listTb)
            {
                var produto = new Produto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Preco = item.Preco,
                    Quant = item.Quant,
                    NotaFiscal = item.NotaFiscal
                    
                };

                listFun.Add(produto);
            }

            return listFun;
        }

        public Produto GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbProdutos.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var produto = new Produto
            {
                Id = item.Id,
                Nome = item.Nome,
                Preco = item.Preco,
                Quant = item.Quant,
                NotaFiscal = item.NotaFiscal
                
                                
                // Mantém o campo Foto como byte[]
            };

            return produto; // Retorna o funcionário encontrado
        }

        public void Update(Produto produto, IFormFile notaFiscal)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbProduto = _context.TbProdutos.FirstOrDefault(f => f.Id == produto.Id);

            // Verifica se a entidade foi encontrada
            if (tbProduto != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbProduto.Nome = produto.Nome; 
                tbProduto.Preco = produto.Preco;
                tbProduto.Quant = produto.Quant;
                tbProduto.NotaFiscal = produto.NotaFiscal;               

                // Verifica se uma nova foto foi enviada
                if (notaFiscal != null && notaFiscal.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        notaFiscal.CopyTo(memoryStream);
                        tbProduto.NotaFiscal = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbProdutos.Update(tbProduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }
    }
}
