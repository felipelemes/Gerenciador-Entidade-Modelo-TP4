using Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Infraestrutura
{
    public class EntidadeRepositorioMemoria : IEntidadeRepositorio
    {
        List<Contrato> contratos = new List<Contrato>();

                public IList<Contrato> Pesquisar(string termoDePesquisa)
        {
            var listaComResultados = contratos.Where(x => x.NomeDaEmpresaContratada.StartsWith(termoDePesquisa.ToUpper()));
            return new List<Contrato>(listaComResultados);
        }

        public void Adicionar(Contrato contrato)
        {
            contratos.Add(contrato);
        }

        public void Remover(Contrato contrato)
        {
            contratos.Remove(contrato);
        }

        public void Editar(Contrato contratoEditado, Contrato contratoAntigo)
        {
            Adicionar(contratoEditado);
            Remover(contratoAntigo);
        }

        
    }
}
