using Estudos.Redis.Api.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Estudos.Redis.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : Controller
    {

        private readonly IPessoaRepository _iPessoaRepository;

        public RedisController(IPessoaRepository iPessoaRepository)
        {
            _iPessoaRepository = iPessoaRepository;
        }

        [HttpGet("/[controller]/[action]/{nome}")]
        public async Task<IActionResult> Get(string nome)
        {
            return Ok(_iPessoaRepository.GetPessoa(nome));

        }
    }
}
