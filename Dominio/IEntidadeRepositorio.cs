using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public interface IEntidadeRepositorio
    {
        IList<Entidade> Pesquisar(string termoDePesquisa);
        void Adicionar(Entidade entidade);
    }
}
