using Dominio;
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

        public Worker(IEntidadeRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ///REFATORAR M�TODO MAIN EM M�TODOS MENORES

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";

            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("*** Gerenciador de [Entidades] *** ");
                Console.WriteLine("1 - Pesquisar [entidades]:");
                Console.WriteLine("2 - Adicionar [entidade]:");
                Console.WriteLine("3 - Sair:");
                Console.WriteLine("\nEscolha uma das op��es acima: ");

                opcao = Console.ReadLine();
                if (opcao == "1")
                {
                    Console.WriteLine("Informe [o campo string ou parte do campo string] [da entidade] que deseja pesquisar:");
                    var termoDePesquisa = Console.ReadLine();
                    var entidadesEncontradas = _repositorio.Pesquisar(termoDePesquisa);

                    if (entidadesEncontradas.Count > 0)
                    {
                        Console.WriteLine("Selecione uma das op��es abaixo para visualizar os dados [das entidades] encontrados:");
                        for (var index = 0; index < entidadesEncontradas.Count; index++)
                            Console.WriteLine($"{index} - {entidadesEncontradas[index].GetInformacao2()}");

                        if (!ushort.TryParse(Console.ReadLine(), out var indexAExibir) || indexAExibir >= entidadesEncontradas.Count)
                        {
                            Console.WriteLine($"Op��o inv�lida! {pressioneQualquerTecla}");
                            Console.ReadKey();
                            continue;
                        }

                        if (indexAExibir < entidadesEncontradas.Count)
                        {
                            var entidade = entidadesEncontradas[indexAExibir];

                            Console.WriteLine("Dados [da entidade]");
                            Console.WriteLine($"[campo string]: {entidade.GetInformacao2()}");
                            Console.WriteLine($"[campo DateTime]: {entidade.GetInformacao3():dd/MM/yyyy}");
                            ///IMPRIMIR NO CONSOLE DEMAIS CAMPOS
                            ///INVOCAR E EXIBIR RESULTADO DO M�TODO DA ENTIDADE QUE CONTEM REGRA DE NEG�CIO

                            Console.Write(pressioneQualquerTecla);
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"N�o foi encontrado nenhuma [entidade]! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                }
                else if (opcao == "2")
                {
                    ///SOLICITAR USU�RIO QUE INFORME OS DADOS DA NOVA ENTIDADE (DO SEU TEMA) - PELO MENOS CINCO INFORMA��ES
                    Console.WriteLine("Informe [campo string] da [entidade] que deseja adicionar:"); //ex: uma informa��o do tipo string: nome medico, nome carro, nome prefeito
                    var campoDoTipoStringDaEntidade = Console.ReadLine();

                    Console.WriteLine("Informe [campo DateTime da entidade] (formato dd/MM/yyyy):"); //uma informa��o do tipo DateTime: data de anivers�rio do m�dico, data de compra do carro, data de nomea��o do prefeito
                    if (!DateTime.TryParse(Console.ReadLine(), out var campoDoTipoDateTimeDaEntidade))
                    {
                        Console.WriteLine($"Data inv�lida! Dados descartados! {pressioneQualquerTecla}");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("Os dados est�o corretos?");
                    Console.WriteLine($"[Campo string da entidade]: {campoDoTipoStringDaEntidade}");
                    Console.WriteLine($"[Campo DateTime da entidade]: {campoDoTipoDateTimeDaEntidade:dd/MM/yyyy}");
                    Console.WriteLine("1 - Sim \n2 - N�o");

                    var opcaoParaAdicionar = Console.ReadLine();
                    if (opcaoParaAdicionar == "1")
                    {
                        ///ATRIBUIR INFORMA��ES OBTIDAS NO CONSOLE NA NOVA ENTIDADE
                        _repositorio.Adicionar(new Entidade());

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
                else if (opcao == "3")
                {
                    Console.Write("Saindo do programa... ");
                }
                else if (opcao != "3")
                {
                    Console.Write($"Opcao inv�lida! Escolha uma op��o v�lida. {pressioneQualquerTecla}");
                    Console.ReadKey();
                }

            } while (opcao != "3");
        }
    }
}
