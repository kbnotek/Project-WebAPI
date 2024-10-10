using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Model;
using ProjetoFinal.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteR _clienteR;

        public ClienteController(ClienteR clienteR)
        {
            _clienteR = clienteR;
        }
        // GET: api/Funcionario/{id}/foto
        [HttpGet("{id}/foto")]
        public IActionResult GetFoto(int id)
        {
            // Busca o funcionário pelo ID
            var cliente = _clienteR.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (cliente == null || cliente.RegistroGeral == null)
            {
                return NotFound(new { Mensagem = "Foto não encontrada." });
            }


            // Retorna a foto como um arquivo de imagem
            return File(cliente.RegistroGeral, "image/jpeg "); // Ou "image/png" dependendo do formato

        }

        // GET: api/<ClienteController>
        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var cliente = _clienteR.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (cliente == null || !cliente.Any())
            {
                return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = cliente.Select(cliente => new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                
                UrlRegistroGeral = $"{Request.Scheme}://{Request.Host}/api/Funcionario/{cliente.Id}/foto" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var cliente = _clienteR.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (cliente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var clienteComUrl = new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                               
                UrlRegistroGeral = $"{Request.Scheme}://{Request.Host}/api/Funcionario/{cliente.Id}/foto" // Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(clienteComUrl);
        }

        // POST api/<ClienteController>
        [HttpPost]
        public ActionResult<object> Post([FromForm] ClienteDto novoCliente)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var cliente = new Cliente
            {
                Nome = novoCliente.Nome,
                Telefone = novoCliente.Telefone,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro

            _clienteR.Add(cliente,novoCliente.RegistroGeral);
            

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome =cliente.Nome,
                Telefone = novoCliente.Telefone,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ClienteDto cliente)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteR.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            clienteExistente.Nome = cliente.Nome;
            clienteExistente.Telefone = cliente.Telefone;
          

            // Chama o método de atualização do repositório, passando a nova foto
            _clienteR.Update(clienteExistente, cliente.RegistroGeral);

            // Cria a URL da foto
            var urlFoto = $"{Request.Scheme}://{Request.Host}/api/Funcionario/{clienteExistente.Id}/foto";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone,
                UrlFoto = urlFoto // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public ActionResult Delet(int id)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteR.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _clienteR.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
