using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Model;
using ProjetoFinal.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjetoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
        
    {
        private readonly EnderecoR _enderecoR;
        public EnderecoController(EnderecoR enderecoR)
        {
            _enderecoR = enderecoR;
        }

        // GET: api/<EnderecoController>
        [HttpGet]
        public ActionResult<List<Endereco>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var endereco = _enderecoR.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (endereco == null || !endereco.Any())
            {
                return NotFound(new { Mensagem = "Nenhum Endereço encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listasemUrl = endereco.Select(endereco => new Endereco
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                N = endereco.N,
                FkCliente = endereco.FkCliente,

            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listasemUrl);
        }

        // GET api/<EnderecoController>/5
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var endereco = _enderecoR.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (endereco == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var enderecoSemUrl = new Endereco
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                N = endereco.N,
                FkCliente = endereco.FkCliente,
            };

            // Retorna o funcionário com status 200 OK
            return Ok(enderecoSemUrl);
        }

        // POST api/<EnderecoController>
        [HttpPost]
        public ActionResult<object> Post([FromForm]Endereco novoEndereco)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var endereco = new Endereco
            {
                Logradouro = novoEndereco.Logradouro,
                Cidade = novoEndereco.Cidade,
                Estado= novoEndereco.Estado,
                Cep = novoEndereco.Cep,
                PontoReferencia= novoEndereco.PontoReferencia,
                N = novoEndereco.N,
                FkCliente= novoEndereco.FkCliente
               
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _enderecoR.Add(endereco);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço cadastrado com sucesso!",
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado= endereco.Estado,
                Cep = endereco.Cep, 
                PontoReferencia = endereco.PontoReferencia,
                N = endereco.N,
                FkCliente = endereco.FkCliente

                
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<EnderecoController>/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] Endereco enderecoAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoR.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
           
            enderecoExistente.Logradouro = enderecoAtualizado.Logradouro;
            enderecoExistente.Cidade = enderecoAtualizado.Cidade;
            enderecoExistente.Cep = enderecoAtualizado.Cep;
            enderecoExistente.PontoReferencia=enderecoAtualizado.PontoReferencia;
            enderecoExistente.N = enderecoAtualizado.N;
            enderecoExistente.FkCliente = enderecoAtualizado.FkCliente;
           

            // Chama o método de atualização do repositório, passando a nova foto
            _enderecoR.Update(enderecoExistente);

           
            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço atualizado com sucesso!",
                Logradouro = enderecoExistente.Logradouro,
                Cidade = enderecoExistente.Cidade,
                Estados = enderecoExistente.Estado,
                Cep = enderecoExistente.Cep,
                PontoReferencia = enderecoExistente.PontoReferencia,
                N = enderecoExistente.N,
                FkCliente = enderecoExistente.FkCliente,
               
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<EnderecoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoR.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _enderecoR.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço excluído com sucesso!",
                Logradouro = enderecoExistente.Logradouro,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                Cep = enderecoExistente.Cep,
                PontoReferencia= enderecoExistente.PontoReferencia,
                N = enderecoExistente.N,
                PkCliente = enderecoExistente.FkCliente               
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
