using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Model;
using ProjetoFinal.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoR _produtoR;

        public ProdutoController(ProdutoR produtoR)
        {
            _produtoR = produtoR;
        }
        // GET: api/Funcionario/{id}/foto
        [HttpGet("{id}/foto")]
        public IActionResult GetFoto(int id)
        {
            // Busca o funcionário pelo ID
            var produto = _produtoR.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produto == null || produto.NotaFiscal == null)
            {
                return NotFound(new { Mensagem = "Nota Fiscal não encontrada." });
            }


            // Retorna a foto como um arquivo de imagem
            return File(produto.NotaFiscal, "image/jpeg "); // Ou "image/png" dependendo do formato

        }
        // GET: api/<ProdutoController>
        [HttpGet]
        public ActionResult<List<Produto>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var produto = _produtoR.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (produto == null || !produto.Any())
            {
                return NotFound(new { Mensagem = "Nenhuma Nota Fiscal encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = produto.Select(produto => new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quant = produto.Quant,
               

               UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Funcionario/{produto.Id}/foto" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET api/<ProdutoController>/5
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var produto = _produtoR.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (produto == null)
            {
                return NotFound(new { Mensagem = "Nota Fiscal não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var produtoComUrl = new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quant= produto.Quant,

                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Funcionario/{produto.Id}/foto" // Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(produtoComUrl);
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] ProdutoDto novoProduto)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var produto = new Produto
            {
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco,
                Quant = novoProduto.Quant               
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro

           _produtoR.Add(produto, novoProduto.NotaFiscal);


            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto cadastrado com sucesso!",
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco,
                Quant = novoProduto.Quant, 
                
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ProdutoController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ProdutoDto produto)
        {
            // Busca o funcionário existente pelo Id
            var produtoExistente = _produtoR.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            produtoExistente.Nome = produto.Nome;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Quant = produto.Quant;          

            // Chama o método de atualização do repositório, passando a nova foto
           _produtoR.Update(produtoExistente, produto.NotaFiscal);

            // Cria a URL da foto
            var urlFoto = $"{Request.Scheme}://{Request.Host}/api/Funcionario/{produtoExistente.Id}/foto";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto atualizado com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco,
                Quant = produtoExistente.Quant,
                UrlFoto = urlFoto // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<ProdutoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delet(int id)
        {
            // Busca o funcionário existente pelo Id
            var produtoExistente = _produtoR.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Chama o método de exclusão do repositório
           _produtoR.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto excluído com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco,
                Quant = produtoExistente.Quant,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
