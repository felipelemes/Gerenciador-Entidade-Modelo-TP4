using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public interface IEntidadeRepositorio
    {
        IList<Contrato> Pesquisar(string termoDePesquisa);
        void Adicionar(Contrato entidade);

        void Remover(Contrato entidade);

        void Editar(Contrato entidadeEditada, Contrato entidadeAntiga);
    }
}
