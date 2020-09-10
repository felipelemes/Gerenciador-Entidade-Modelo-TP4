using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura
{
    public class EntidadeRepositorioComOutroTipoDeColecao : IEntidadeRepositorio
    {
        ///1 - CRIAR COLEÇÃO EM MEMÓRIA


        public IList<Entidade> Pesquisar(string termoDePesquisa)
        {
            ///2 - REALIZAR PESQUISA NA COLEÇÃO EM MEMÓRIA E RETORNAR RESULTADO
            return new List<Entidade>();
        }

        public void Adicionar(Entidade entidade)
        {
            ///3 - ADICIONAR A ENTIDADE (DO SEU TEMA) NA COLEÇÃO EM MEMÓRIA
        }
    }
}
