using Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

using System.Text;

namespace Infraestrutura
{
    [Serializable]
    public class EntidadeRepositorioArquivo : IEntidadeRepositorio
    {



        public IList<Contrato> Pesquisar(string nomeDaEmpresaContratada)
        {
            List<Contrato> listaComTodosContratos = new List<Contrato>();
            var diretorioRaiz = new DirectoryInfo(@"C:\Users\felip\source\repos\Tp4-Felipe-Lemes\Infraestrutura\data\");
            DataSerializer dataSerializer = new DataSerializer();

            foreach (FileInfo file in diretorioRaiz.GetFiles())
            {

                Contrato contratoDeserialized = null;
                contratoDeserialized = dataSerializer.XmlDeserialize(typeof(Contrato), $"{file.DirectoryName}\\{file.Name}") as Contrato;
                listaComTodosContratos.Add(contratoDeserialized);
            }
            var listaComResultados = listaComTodosContratos.Where(x => x.NomeDaEmpresaContratada.StartsWith(nomeDaEmpresaContratada.ToUpper()));
            return new List<Contrato>(listaComResultados);
        }

        public void Adicionar(Contrato contrato)
        {
            Contrato novoContrato = contrato;
            string filePath = $"C:\\Users\\felip\\source\\repos\\Tp4-Felipe-Lemes\\Infraestrutura\\data\\{contrato.NomeDaEmpresaContratada}-{contrato.NumeroDoContrato}.data";
            DataSerializer dataSerializer = new DataSerializer();


            dataSerializer.XmlSerialize(typeof(Contrato), novoContrato, filePath);

        }
        public void Remover(Contrato contrato)
        {
            File.Delete($"C:\\Users\\felip\\source\\repos\\Tp4-Felipe-Lemes\\Infraestrutura\\data\\{contrato.NomeDaEmpresaContratada}-{contrato.NumeroDoContrato}.data");

        }

        public void Editar(Contrato contratoEditado, Contrato contratoAntigo)
        {
            Adicionar(contratoEditado);
            Remover(contratoAntigo);
        }

    }
}
