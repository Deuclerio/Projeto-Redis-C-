using Estudos.Redis.Api.Entidade;
using Estudos.Redis.Api.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Estudos.Redis.Api.Repository
{
    public class PessoaRepository : IPessoaRepository
    {

        private readonly IRedisCacheService _redisCache;

        public PessoaRepository(IRedisCacheService redisCache)
        {
            _redisCache = redisCache;
        }

        public string GetPessoa(string nome)
        {
            var pessoa = new Pessoa
            {
                Nome = nome
            };

            var result = _redisCache.Get<List<Pessoa>>("key");

            var primeiro = result?.FirstOrDefault(x => x.Nome == nome);

            if (primeiro is not null)
                return primeiro.Nome;


            result = result ?? new List<Pessoa>();
            result.Add(pessoa);


            _redisCache.Set<List<Pessoa>>("key", result);

            return nome;

        }
    }
}
