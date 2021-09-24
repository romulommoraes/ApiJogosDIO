using ApiJogosDIO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiJogosDIO.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            { Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Jogo{Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Nome = "Super Mario Bros 1", Produtora = "Nintendo", Preco = 50 } },
            { Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa3"), new Jogo{Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa3"), Nome = "Super Mario Bros 2", Produtora = "Nintendo", Preco = 50 } },
            { Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2"), new Jogo{Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2"), Nome = "Super Mario Bros 3", Produtora = "Nintendo", Preco = 55 } }
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade) //Linq pra paginação
        {
            return Task.FromResult(jogos.Values.Skip((pagina -1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
          if(!jogos.ContainsKey(id))
            {
                return null;
            }
            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());//LinQ
        }

        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach (var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                {
                    retorno.Add(jogo);
                }
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //fechar conexão com o banco
        }


    }
}
