using System.Threading.Tasks;

namespace Estudos.Redis.Api.Interface
{
    public interface IPessoaRepository
    {

        string GetPessoa(string nome);

    }
}
