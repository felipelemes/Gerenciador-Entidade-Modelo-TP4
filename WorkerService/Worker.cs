using Dominio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IEntidadeRepositorio _repositorio;
        private readonly string sourceFile;
        private const string pressioneQualquerTecla = " \n Pressione qualquer tecla para exibir o menu principal ...";
        public Worker(IEntidadeRepositorio repositorio, IConfiguration configuration)
        {
            _repositorio = repositorio;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            string opcao;
            do
            {
                ExibirMenuPrincipal();

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        PesquisarContratados();
                        break;
                    case "2":
                        AdicionarContrato();
                        break;
                    case "3":
                        RemoverContrato();
                        break;
                    case "4":
                        EditarContrato();
                        break;
                    case "5":
                        Console.Write("Saindo do programa... ");
                        break;
                    default:
                        Console.Write("Opcao inv�lida! Escolha uma op��o v�lida. ");
                        break;
                }

            }
            while (opcao != "5");
        }

        void ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("*** Gerenciador de Contratados *** ");
            Console.WriteLine("1 - Pesquisar Contratados:");
            Console.WriteLine("2 - Adicionar Contratados:");
            Console.WriteLine("3 - Remover Contratados:");
            Console.WriteLine("4 - Editar Contratados:");
            Console.WriteLine("5 - Sair:");
            Console.WriteLine("\nEscolha uma das op��es acima: ");

        }

        void PesquisarContratados()
        {
            Console.WriteLine("Informe o nome da Empresa Contratada que deseja pesquisar:");
            var termoDePesquisa = Console.ReadLine();
            var contratosEncontrados = _repositorio.Pesquisar(termoDePesquisa);

            if (contratosEncontrados.Count > 0)
            {
                Console.WriteLine("Selecione uma das op��es abaixo para visualizar os dados do(s) Contrato(s) encontrados:");
                for (var index = 0; index < contratosEncontrados.Count; index++)
                    Console.WriteLine($"{index} - {contratosEncontrados[index].NomeDaEmpresaContratada} - Contrato: {contratosEncontrados[index].NumeroDoContrato}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= contratosEncontrados.Count)
                {
                    Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                    Console.ReadKey();
                    return;
                }

                if (indexAExibir < contratosEncontrados.Count)
                {
                    var contrato = contratosEncontrados[indexAExibir];



                    Console.WriteLine(contrato);

                    Console.Write(pressioneQualquerTecla);
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"N�o foi encontrado nenhum Contrato! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
        }

        void AdicionarContrato()
        {

            Console.WriteLine("Informe o nome do usu�rio: ");
            string nomeDoUsuario = Console.ReadLine();

            Console.WriteLine("Informe o CPF do usu�rio (Somente n�meros):");
            if (!int.TryParse(Console.ReadLine(), out var cpf))
            {
                Console.WriteLine($"Digite apenas n�meros! Dados descartados! {pressioneQualquerTecla}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Informe o email do usu�rio: ");
            string email = Console.ReadLine();

            Console.WriteLine("Informe o Nome da Empresa Contratada: ");
            string nomeDaEmpresaContratada = Console.ReadLine();

            Console.WriteLine("Informe o CNPJ do usu�rio (Somente n�meros):");
            if (!int.TryParse(Console.ReadLine(), out var cnpj))
            {
                Console.WriteLine($"Digite apenas n�meros! Dados descartados! {pressioneQualquerTecla}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Informe o N�mero do Contrato(Somente n�meros):");
            if (!int.TryParse(Console.ReadLine(), out var numeroDoContrato))
            {
                Console.WriteLine($"Digite apenas n�meros! Dados descartados! {pressioneQualquerTecla}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Informe a Data de Validade Do Contrato (formato dd/MM/yyyy):");
            if (!DateTime.TryParse(Console.ReadLine(), out var validadeDoContrato))
            {
                Console.WriteLine($"Data inv�lida! Dados descartados! {pressioneQualquerTecla}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n Os dados est�o corretos?");
            Console.WriteLine($"Nome do Usu�rio: {nomeDoUsuario}");
            Console.WriteLine($"CPF do usu�rio: {cpf}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Nome da Empresa Contratada: {nomeDaEmpresaContratada}");
            Console.WriteLine($"CNPJ da Empresa Contratada: {cnpj}");
            Console.WriteLine($"N�mero do Contrato: {numeroDoContrato}");
            Console.WriteLine($"Validade do Contrato: {validadeDoContrato:dd/MM/yyyy}");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Confirma?");
            Console.WriteLine("1 - Sim \n2 - N�o");

            var opcaoParaAdicionar = Console.ReadLine();
            if (opcaoParaAdicionar == "1")
            {

                _repositorio.Adicionar(new Contrato(nomeDoUsuario, cpf, email, nomeDaEmpresaContratada, cnpj, numeroDoContrato, validadeDoContrato));

                Console.WriteLine($"Dados adicionados com sucesso! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
            else if (opcaoParaAdicionar == "2")
            {
                Console.WriteLine($"Dados descartados! {pressioneQualquerTecla}");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
        }

        void RemoverContrato()
        {
            Console.WriteLine("Informe o nome da Empresa Contratada que deseja remover:");
            var termoDePesquisa = Console.ReadLine();
            var contratosEncontrados = _repositorio.Pesquisar(termoDePesquisa);

            if (contratosEncontrados.Count > 0)
            {
                Console.WriteLine("Selecione uma das op��es abaixo para visualizar os dados do contrato a ser removido:");

                for (var index = 0; index < contratosEncontrados.Count; index++)
                    Console.WriteLine($"{index} - {contratosEncontrados[index].NomeDaEmpresaContratada} - Contrato: {contratosEncontrados[index].NumeroDoContrato}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= contratosEncontrados.Count)
                {
                    Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                    Console.ReadKey();
                    return;
                }

                if (indexAExibir < contratosEncontrados.Count)
                {
                    var contrato = contratosEncontrados[indexAExibir];

                    Console.WriteLine("--------------------");
                    Console.WriteLine(contrato);
                    Console.WriteLine("--------------------");
                    Console.WriteLine("Deseja realmente excluir o contrato acima? (S/N)");
                    string confirmaExclusao = Console.ReadLine();
                    string confirmaExlusaoTratado = confirmaExclusao.ToUpper();

                    if (confirmaExlusaoTratado == "S")
                    {
                        _repositorio.Remover(contrato);

                        Console.WriteLine($"Contrato Exclu�do com sucesso! {pressioneQualquerTecla}");
                        Console.ReadKey();
                        return;
                    }
                    else if (confirmaExlusaoTratado == "N")
                    {
                        Console.WriteLine($"O Contrato n�o foi exclu�do! {pressioneQualquerTecla}");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                        Console.ReadKey();
                        return;
                    }
                }
            }

        }

        void EditarContrato()
        {
            Console.WriteLine("Informe o nome da Empresa Contratada que deseja pesquisar para editar o contrato:");
            var termoDePesquisa = Console.ReadLine();
            var contratosEncontrados = _repositorio.Pesquisar(termoDePesquisa);

            if (contratosEncontrados.Count > 0)
            {
                Console.WriteLine("Selecione uma das op��es abaixo para visualizar os dados do(s) Contrato(s) encontrados:");

                for (var index = 0; index < contratosEncontrados.Count; index++)
                    Console.WriteLine($"{index} - {contratosEncontrados[index].NomeDaEmpresaContratada} - Contrato: {contratosEncontrados[index].NumeroDoContrato}");

                if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= contratosEncontrados.Count)
                {
                    Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                    Console.ReadKey();
                    return;
                }

                if (indexAExibir < contratosEncontrados.Count)
                {
                    var contratoAntigo = contratosEncontrados[indexAExibir];

                    Console.WriteLine(contratoAntigo);
                    Console.WriteLine("--------------------");
                    Console.WriteLine("Confirma que realizar� a edi��o deste contrato? (S/N)");
                    string confirmaEdicao = Console.ReadLine();
                    string confirmaEdicaoTratado = confirmaEdicao.ToUpper();

                    if (confirmaEdicaoTratado == "S")
                    {
                        Console.WriteLine("Informe o nome do usu�rio: ");
                        string nomeDoUsuario = Console.ReadLine();

                        Console.WriteLine("Informe o CPF do usu�rio (Somente n�meros):");

                        if (!int.TryParse(Console.ReadLine(), out var cpf))
                        {
                            Console.WriteLine($"Digite apenas n�meros! Dados descartados! {pressioneQualquerTecla}");
                            Console.ReadKey();
                            return;
                        }

                        Console.WriteLine("Informe o email do usu�rio: ");
                        string email = Console.ReadLine();

                        Console.WriteLine("Informe o Nome da Empresa Contratada: ");
                        string nomeDaEmpresaContratada = Console.ReadLine();

                        Console.WriteLine("Informe o CNPJ do usu�rio (Somente n�meros):");

                        if (!int.TryParse(Console.ReadLine(), out var cnpj))
                        {
                            Console.WriteLine($"Digite apenas n�meros! Dados descartados! {pressioneQualquerTecla}");
                            Console.ReadKey();
                            return;
                        }

                        Console.WriteLine("Informe o N�mero do Contrato(Somente n�meros):");

                        if (!int.TryParse(Console.ReadLine(), out var numeroDoContrato))
                        {
                            Console.WriteLine($"Digite apenas n�meros! Dados descartados! {pressioneQualquerTecla}");
                            Console.ReadKey();
                            return;
                        }

                        Console.WriteLine("Informe a Data de Validade Do Contrato (formato dd/MM/yyyy):");

                        if (!DateTime.TryParse(Console.ReadLine(), out var validadeDoContrato))
                        {
                            Console.WriteLine($"Data inv�lida! Dados descartados! {pressioneQualquerTecla}");
                            Console.ReadKey();
                            return;
                        }

                        Console.WriteLine("\n Os dados est�o corretos?");
                        Console.WriteLine($"Nome do Usu�rio: {nomeDoUsuario}");
                        Console.WriteLine($"CPF do usu�rio: {cpf}");
                        Console.WriteLine($"Email: {email}");
                        Console.WriteLine($"Nome da Empresa Contratada: {nomeDaEmpresaContratada}");
                        Console.WriteLine($"CNPJ da Empresa Contratada: {cnpj}");
                        Console.WriteLine($"N�mero do Contrato: {numeroDoContrato}");
                        Console.WriteLine($"Validade do Contrato: {validadeDoContrato:dd/MM/yyyy}");
                        Console.WriteLine("-----------------------------------------------------");
                        Console.WriteLine("Confirma?");
                        Console.WriteLine("1 - Sim \n2 - N�o");

                        var opcaoParaAdicionar = Console.ReadLine();

                        if (opcaoParaAdicionar == "1")
                        {
                            Contrato contratoEditado = new Contrato(nomeDoUsuario, cpf, email, nomeDaEmpresaContratada, cnpj, numeroDoContrato, validadeDoContrato);
                            _repositorio.Editar(contratoEditado, contratoAntigo);


                            Console.WriteLine($"Dados editados com sucesso! {pressioneQualquerTecla}");
                            Console.ReadKey();
                        }

                        else if (opcaoParaAdicionar == "2")
                        {
                            Console.WriteLine($"Dados descartados! {pressioneQualquerTecla}");
                            Console.ReadKey();
                        }

                        else
                        {
                            Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                            Console.ReadKey();
                        }
  
                    }
                }
                else
                {
                    Console.WriteLine($"N�o foi encontrado nenhum Contrato! {pressioneQualquerTecla}");
                    Console.ReadKey();
                }
            }
        }
    }
}


